using UnityEngine;
using System.Collections.Generic;

public class EmaQueue : MonoBehaviour
{
    private Queue<int> _rightEmaQueue;
    private Queue<int> _leftEmaQueue;

    public void Initialize()
    {
        _rightEmaQueue = new Queue<int>();
        _leftEmaQueue = new Queue<int>();
    }

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

    private void EnqueueRight(int id)
    {
        _rightEmaQueue.Enqueue(id);
    }
    private void EnqueueLeft(int id)
    {
        _leftEmaQueue.Enqueue(id);
    }

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