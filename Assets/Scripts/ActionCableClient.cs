using UnityEngine;
using WebSocketSharp;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ActionCableClient : MonoBehaviour
{
    private WebSocket ws;
    private string roomId = "room_channel"; // 外部から設定するRoom ID
    private string channelName = "RoomChannel"; // ActionCableのチャンネル名
    private string wsUrl;
    private string flowerUrl = null;
    private string nameUrl = null;
    private string wishUrl = null;
    [SerializeField] private GameObject _emaCreator;
    private bool _isFlowerInfoUpdated = false;
    private bool _isNew = false;


    [Serializable]
    public class Test
    {
        [SerializeField] public string type;
        [SerializeField] public Identifier identifier;
    }

    [Serializable]
    public class Identifier
    {
        [SerializeField] public string channel;
        [SerializeField] public string room_id;
        public override string ToString()
        {
            return $"channel:{channel}, room_id:{room_id}";
        }
    }


    void Start()
    {
        wsUrl = GetWebSocketUrl();
        ws = new WebSocket(wsUrl);

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
            SubscribeToChannel();
            SetupEmas();
        };

        ws.OnMessage += (sender, e) =>
        {
            try
            {
                JObject wsMessageJson = JObject.Parse(e.Data);
                if ((string)wsMessageJson["type"] == "ping")
                {
                    // pingメッセージは無視
                }
                else if ((string)wsMessageJson["type"] == "confirm_subscription")
                {
                    // サブスクリプション確認メッセージは無視
                }
                else if ((string)wsMessageJson["type"] == "welcome")
                {
                    // welcomeメッセージは無視
                }
                else
                {
                    Debug.Log(wsMessageJson["message"]);                 // 投稿通知のjson
                    Debug.Log(wsMessageJson["message"]["message"]);      // 新しいイラストが投稿されました！
                    Debug.Log(wsMessageJson["message"]["data"]["urls"]["illustration"]);  // 画像の url

                    flowerUrl = (string)wsMessageJson["message"]["data"]["urls"]["illustration"];
                    nameUrl = (string)wsMessageJson["message"]["data"]["urls"]["name"];
                    wishUrl = (string)wsMessageJson["message"]["data"]["urls"]["wish"];
                    _isFlowerInfoUpdated = true;

                    string msgType = (string)wsMessageJson["message"]["message"];
                    if (msgType == "new")
                    {
                        Debug.Log("新しい画像が送られてきたので_isNew = true");
                        _isNew = true;
                    }
                    else if (msgType == "old")
                    {
                        Debug.Log("古い画像が送られてきたので_isNew = false");
                    }
                    else
                    {
                        Debug.Log("そもそも区別できていない");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("WebSocket メッセージ処理中に例外が発生: " + ex.Message);
                Debug.LogError("例外発生時の生データ: " + e.Data);
            }
        };

        ws.OnError += (sender, e) =>
        {
            Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket Close");
        };

        ws.Connect();
    }

    async UniTask Update()
    {
        if (_isFlowerInfoUpdated)
        {
            if (_emaCreator != null)
            {
                if (_isNew)
                {
                    Debug.Log("新しい画像をセット");
                    _emaCreator.GetComponent<EmaCreator>().CreateEma(flowerUrl, nameUrl, wishUrl);
                    Debug.Log("lkjhg--------");
                }
            }
            _isFlowerInfoUpdated = false; // 処理終わったのでリセット
        }

        if (Input.GetKeyUp("s"))
        {
            SendMessageToChannel("Hello from Unity!");
        }
    }

    void OnDestroy()
    {
        UnsubscribeFromChannel();
        ws.Close();
        ws = null;
    }

    private void SubscribeToChannel()
    {
        string identifier = $"{{\"channel\":\"{channelName}\",\"room_id\":\"{roomId}\"}}";
        string subscriptionMessage = $"{{\"command\":\"subscribe\",\"identifier\":\"{EscapeJson(identifier)}\"}}";
        ws.Send(subscriptionMessage);
        Debug.Log("Subscribed to " + channelName + " with Room ID: " + roomId);
    }

    private async void SetupEmas()
    {
        Debug.Log("SetupEmas");

        UnityWebRequest www = UnityWebRequest.Get("https://e-ma-rails-staging-986464278422.asia-northeast1.run.app/emas");
        www.downloadHandler = new DownloadHandlerBuffer(); // 明示的に追加（なくても動くが推奨）
        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("絵馬の直近一覧取得に失敗しました: " + www.error);
            return;
        }

        try
        {
            JArray emasJsonArray = JArray.Parse(www.downloadHandler.text);

            foreach (JObject ema in emasJsonArray)
            {
                int id = ema.Value<int?>("id") ?? -1;
                string illustrationUrl = ema.Value<string>("illustration");
                string nameUrl = ema.Value<string>("name");
                string wishUrl = ema.Value<string>("wish");
                if (illustrationUrl == "") {
                    continue;
                }
                Debug.Log($"絵馬ID: {id}, illustration: {illustrationUrl}, name: {nameUrl}, wish: {wishUrl}");
                _emaCreator.GetComponent<EmaCreator>().RebornEma(illustrationUrl, nameUrl, wishUrl);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("JSONパース中にエラーが発生しました: " + e.Message);
        }

        Debug.Log("SetupEmas end");
    }


    private void UnsubscribeFromChannel()
    {
        string identifier = $"{{\"channel\":\"{channelName}\",\"room_id\":\"{roomId}\"}}";
        string unsubscribeMessage = $"{{\"command\":\"unsubscribe\",\"identifier\":\"{EscapeJson(identifier)}\"}}";
        ws.Send(unsubscribeMessage);
        Debug.Log("Unsubscribed from " + channelName + " with Room ID: " + roomId);
    }

    private void SendMessageToChannel(string message)
    {
        string identifier = $"{{\"channel\":\"{channelName}\",\"room_id\":\"{roomId}\"}}";
        string data = $"{{\"action\":\"speak\",\"message\":\"{message}\"}}";
        string dataMessage = $"{{\"command\":\"message\",\"identifier\":\"{EscapeJson(identifier)}\",\"data\":\"{EscapeJson(data)}\"}}";
        ws.Send(dataMessage);
        Debug.Log("Sent: " + message);
    }

    private string EscapeJson(string json)
    {
        return json.Replace("\"", "\\\""); // JSON内のダブルクォートをエスケープ
    }

    private string GetWebSocketUrl()
    {
#if UNITY_EDITOR
        return "wss://e-ma-rails-staging-986464278422.asia-northeast1.run.app/cable";  // 開発環境
#else
        return "wss://your-production-url.com/cable";  // 本番環境
#endif
    }

    public string GetFlowerUrl()
    {
        return flowerUrl;
    }
    public string GetNameUrl()
    {
        return nameUrl;
    }
    public string GetWishUrl()
    {
        return wishUrl;
    }
}
