using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : UI_Base
{
    public TextMeshProUGUI timer;
    public TextMeshProUGUI killCount;
    public Slider gemSlider;
    public TextMeshProUGUI level;
    public TextMeshProUGUI gold;

    private void OnEnable()
    {
        //타이머를 0으로 초기화
        timer.text = "0:0";

    }

    public void SetTimer(float time)
    {
        int minute = (int)time / 60;
        int second = (int)time % 60;

        timer.text = $"{minute:D2}:{second:D2}";
    }

    private void Update()
    {
        SetTimer(Time.timeSinceLevelLoad);
    }

    public void SetGemCountRatio(float ratio)
    {
        gemSlider.value = ratio;
    }

    public void SetKillCount(int count)
    {
        killCount.text = $"{count}";
    }
    public void SetLevel(int level)
    {
        this.level.text = $"{level}";
    }
    public void SetGold(int gold)
    {
     this.gold.text = $"{gold}";
    }

}
