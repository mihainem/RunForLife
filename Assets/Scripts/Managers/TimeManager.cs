using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TimeManager is singleton which manages timedObjects, meaning that all existent timedObjects
/// can be managed in this one single update
/// </summary>
public class TimeManager : MonoBehaviour, ITimersContainer
{

    private static TimeManager instance;
    public static TimeManager Instance
    {
        get
        {
            if (instance == null && !Application.isPlaying)
                instance = FindObjectOfType<TimeManager>();

            return instance;
        }
    }

    public List<ITimedItem> containedItems { get; set; }

    private void Awake()
    {
        instance = this;
        containedItems = new List<ITimedItem>();
    }


    private void Update()
    {
        for (int i = 0; i < containedItems.Count; i++)
        {
            containedItems[i]?.ProcessTimePassing();
        }
    }

    public Timer DelayedCall(float delayTime, Action callback)
    {
        Timer delayedCall = new Timer(this, delayTime,null, callback);
        containedItems.Add(delayedCall);
        return delayedCall;
    }


    public void Remove(ITimedItem item)
    {
        containedItems.Remove(item);
        item = null;
    }
}
