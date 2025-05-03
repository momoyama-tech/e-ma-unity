using UnityEngine;
using WebSocketSharp;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

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
        };

        ws.OnMessage += (sender, e) =>
        {
            JObject wsMessageJson = JObject.Parse(e.Data);
            if ((String)wsMessageJson["type"] == "ping")
            {
                // Debug.Log("this message is ping, so  through");
            }
            else if ((String)wsMessageJson["type"] == "confirm_subscription")
            {
                // Debug.Log("this message is confirm_subscription, so  through");
            }
            else
            {
                // Debug.Log(wsMessageJson["message"]);                 // 投稿通知のjson
                // Debug.Log(wsMessageJson["message"]["message"]);      // 新しいイラストが投稿されました！
                // Debug.Log(wsMessageJson["message"]["data"]["urls"]["illustration"]);  // 画像の url
                flowerUrl = (string)wsMessageJson["message"]["data"]["urls"]["illustration"];
                nameUrl = (string)wsMessageJson["message"]["data"]["urls"]["name"];
                wishUrl = (string)wsMessageJson["message"]["data"]["urls"]["wish"];
                _isFlowerInfoUpdated = true;
                Debug.Log("flowerUrl: " + flowerUrl);
                Debug.Log("nameUrl: " + nameUrl);
                Debug.Log("wishUrl: " + wishUrl);
                Debug.Log("上の情報から花を作る");
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

    async Task Update()
    {
        if (_isFlowerInfoUpdated)
        {
            if (_emaCreator != null)
            {
                _emaCreator.GetComponent<EmaCreator>().CreateEma(flowerUrl, nameUrl, wishUrl);
                Debug.Log("花情報をセットしました！");
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
