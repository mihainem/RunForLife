using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler //, IPointerClickHandler,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static event Action OnTap;
    public static event Action OnSwipeUp;
    public static event Action OnSwipeDown;
    public static event Action OnSwipeLeft;
    public static event Action OnSwipeRight;
    private Vector2 startDragPos;
    private float minDistanceForSwipe = 20f;

    public void OnPointerDown(PointerEventData eventData)
    {
        startDragPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - startDragPos;
        if (Math.Abs(direction.x) < minDistanceForSwipe && Math.Abs(direction.y) < minDistanceForSwipe)
        {
            OnTap?.Invoke();
            return;
        }

        if (Math.Abs(direction.x) >= Math.Abs(direction.y))
        {
            if (startDragPos.x < eventData.position.x)
            {
                OnSwipeRight?.Invoke();
            }
            else
            {
                OnSwipeLeft?.Invoke();
            }
        }
        else
        {
            if (startDragPos.y < eventData.position.y)
            {
                OnSwipeUp?.Invoke();
            }
            else
            {
                OnSwipeDown?.Invoke();
            }
        }
        startDragPos = Vector2.zero;
    }
}
