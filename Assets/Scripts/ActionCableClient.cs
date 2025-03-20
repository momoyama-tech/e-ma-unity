using UnityEngine;
using WebSocketSharp;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;

public class ActionCableClient : MonoBehaviour
{
    private WebSocket ws;
    private string roomId = "room_channel"; // 外部から設定するRoom ID
    private string channelName = "RoomChannel"; // ActionCableのチャンネル名
    private string wsUrl;
    private Sprite flowerSprite;
    [SerializeField] private GameObject flowerObject;

    [Serializable]
    public class Test
    {
        [SerializeField] public string type;
        [SerializeField] public Identifier identifier;

        // public override string ToString() {
        //     return $"type:{type}, identifier:{identifier}";
        // }
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
            else {
                Debug.Log(wsMessageJson["message"]);                 // 投稿通知のjson
                Debug.Log(wsMessageJson["message"]["message"]);      // 新しいイラストが投稿されました！
                Debug.Log(wsMessageJson["message"]["data"]["url"]);  // 画像の url

            }
            //     try
            //     {
            //         Debug.Log("-----------------------1-----------------------");
            // Debug.Log("WebSocket: " + e.Data);

            //         if (!e.Data.Contains("url"))
            //         {
            //             Debug.Log("No url attribute");
            //             Debug.Log("-----------------------2-----------------------");
            //             return;
            //         }

            //         string[] parts = e.Data.Split(',');
            //         if (parts.Length > 5)
            //         {
            //             string attributeAndValue = parts[5];
            //             Debug.Log("Attribute and Value: " + attributeAndValue);

            //             string[] urlParts = attributeAndValue.Split('"');
            //             if (urlParts.Length > 3)
            //             {
            //                 string url = urlParts[3].Replace("\"", "");
            //                 Debug.Log("URL: " + url);
            //                 Debug.Log("-----------------------3-----------------------");

            //                 StartCoroutine(GetTexture(url));
            //                 Debug.Log("-----------------------5-----------------------");
            //             }
            //             else
            //             {
            //                 Debug.LogError("Unexpected attribute format: " + attributeAndValue);
            //             }
            //         }
            //         else
            //         {
            //             Debug.LogError("Unexpected data format: " + e.Data);
            //         }
            //     }
            //     catch (Exception ex)
            //     {
            //         Debug.LogError("Exception in OnMessage: " + ex.Message);
            //         Debug.LogError("Stack Trace: " + ex.StackTrace);
            //     }
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

    void Update()
    {
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

    private IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        Debug.Log("Connecting to: " + url);
        Debug.Log("-----------------------4-----------------------");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ImageLoadERROR:" + www.error);
        }
        else
        {
            // urlから取得した画像をSpriteに変換
            flowerSprite = Sprite.Create(((DownloadHandlerTexture)www.downloadHandler).texture,
             new Rect(0, 0, ((DownloadHandlerTexture)www.downloadHandler).texture.width,
              ((DownloadHandlerTexture)www.downloadHandler).texture.height),
               Vector2.zero);

            flowerObject.GetComponent<SpriteRenderer>().sprite = flowerSprite;

            Debug.Log("Success");
        }
    }
}
