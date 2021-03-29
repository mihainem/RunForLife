using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlayPanel : Window
{

    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;

    public float maxTimer = 5f;
    private float elapsedTime = 0f;
    private bool startTimer = false;

    public void StartTimer() 
    {
        startTimer = true;
    }    

    private void Update()
    {
        if (!startTimer)
            return;

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= maxTimer) 
        {
            HideWindow();
            elapsedTime = 0f;
            GameManager.Instance.StartPlay();
            startTimer = false;
        }
        timerImage.fillAmount = elapsedTime / maxTimer;
        int remainingTime = (int)(maxTimer - elapsedTime);
        if (remainingTime < 1f) 
        {
            timerText.text = "...Run";
        }
        else
            timerText.text = remainingTime.ToString("00");

    }

}
