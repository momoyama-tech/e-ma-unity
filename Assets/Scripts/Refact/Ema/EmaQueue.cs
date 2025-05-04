using UnityEngine;
using System.Collections.Generic;

public class EmaQueue : MonoBehaviour
{
    private Queue<int> _rightEmaQueue;// 右側のEmaのIDを格納するキュー
    private Queue<int> _leftEmaQueue;// 左側のEmaのIDを格納するキュー

    public void Initialize()
    {
        _rightEmaQueue = new Queue<int>();
        _leftEmaQueue = new Queue<int>();
    }

    /// <summary>
    /// EmaのIDをキューに追加するメソッド
    /// isRightがtrueなら右側のキューに追加、falseなら左側のキューに追加
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isRight"></param>
    public void EnQueue(int id, bool isRight)
    {
        if (isRight)
        {
            EnqueueRight(id);
        }
        else
        {
            EnqueueLeft(id);
        }
    }

    /// <summary>
    /// 右側のEmaのIDをキューに追加するメソッド
    /// </summary>
    /// <param name="id"></param>
    private void EnqueueRight(int id)
    {
        _rightEmaQueue.Enqueue(id);
    }

    /// <summary>
    /// 左側のEmaのIDをキューに追加するメソッド
    /// </summary>
    /// <param name="id"></param>
    private void EnqueueLeft(int id)
    {
        _leftEmaQueue.Enqueue(id);
    }

    /// <summary>
    /// EmaのIDをキューから取り出すメソッド
    /// isRightがtrueなら右側のキューから取り出し、falseなら左側のキューから取り出す
    /// </summary>
    /// <param name="isRight"></param>
    /// <returns></returns>
    public int DeQueue(bool isRight)
    {
        if (isRight)
        {
            return DequeueRight();
        }
        else
        {
            return DequeueLeft();
        }
    }

    /// <summary>
    /// 右側のEmaのIDをキューから取り出すメソッド
    /// </summary>
    /// <returns></returns>
    private int DequeueRight()
    {
        if (_rightEmaQueue.Count > 0)
        {
            return _rightEmaQueue.Dequeue();
        }
        else
        {
            Debug.LogError("Error: Right Ema Queue is empty.");
            return -1; // or some other error value
        }
    }

    /// <summary>
    /// 左側のEmaのIDをキューから取り出すメソッド
    /// </summary>
    /// <returns></returns>
    private int DequeueLeft()
    {
        if (_leftEmaQueue.Count > 0)
        {
            return _leftEmaQueue.Dequeue();
        }
        else
        {
            Debug.LogError("Error: Left Ema Queue is empty.");
            return -1; // or some other error value
        }
    }
}