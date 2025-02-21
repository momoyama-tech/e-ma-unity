using UnityEngine;
using WebSocketSharp;
using System;

public class ActionCableClient : MonoBehaviour
{
    private WebSocket ws;
    private string roomId  = "room_channel"; // 外部から設定するRoom ID
    private string channelName = "RoomChannel"; // ActionCableのチャンネル名
    private string wsUrl;

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
            Debug.Log("WebSocket Data: " + e.Data);
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
                return "ws://localhost:3000/cable";  // 開発環境
        #else
                return "wss://your-production-url.com/cable";  // 本番環境
        #endif
    }
}
