using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MessageTest : MonoBehaviour
{
    public UnityEvent<string> OnReceivedMessage = new UnityEvent<string>();
    
    public void ReceiveMessage(string msg)
    {
        Debug.Log($"[Debug][MessageTest] Received Message:{msg}");
        OnReceivedMessage?.Invoke(msg);
    }
}
