using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Window : MonoBehaviour, IPointerClickHandler
{
    public bool closeOnClick = true;
    public bool closeOnButtonClick = false;
    public bool closeOnClickOutside = false;
    private Animator animator;
    

    void Awake() 
    {
        animator = GetComponent<Animator>();        
    }

    void Start() 
    {
        if (closeOnButtonClick) 
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++) 
            {
                buttons[i].onClick.AddListener(HideWindow);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (closeOnClick && Input.GetMouseButtonDown(0) && gameObject.activeSelf)
            StartCoroutine(HideWindowDelayed());
	}

    private IEnumerator HideWindowDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        HideWindow();
    }

    public void ShowWindow()
    {
        gameObject.SetActive(true);

        if (animator != null)
            animator.Play("intro",0,0);
        
            
    }
    public void HideWindow()
    {
        if (animator != null)
            animator.Play("outro");
        else
            gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    { if (closeOnClickOutside && gameObject.activeSelf)
            HideWindow();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetAsActive()
    {
        gameObject.SetActive(true);
    }

    public void SetAsInactive()
    {
        gameObject.SetActive(false);
    }
}
