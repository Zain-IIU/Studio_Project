using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField] GameObject gameTitle;
    [SerializeField] GameObject[] panelLevelComplete;
    [SerializeField] GameObject[] panelFailed;
   

    public override void Init()
    {
        TapText(false);
      
    }


    public void ShowHideLevelFailedUi(bool value)
    {
        panelFailed.SetActiveAll(value);
    }
    public void ShowHideLevelCompleteUi(bool value)
    {
        panelLevelComplete.SetActiveAll(value);
    }
    public void TapText(bool isShow)
    {
       
    }

          // Ui Buttons
    public void PlayButton(GameObject playBtn)
    {       
        playBtn.SetActive(false);
        gameTitle.SetActive(false);
        GameManager.instance.RestartLevel();
    }

    public void BtnNextLevel()
    {
        ShowHideLevelCompleteUi(false);
        Init();
        GameManager.instance.StartNewLevel();
    }

    public void BtnRetryLevel()
    {
        ShowHideLevelFailedUi(false);
        Init();
        GameManager.instance.RestartLevel();
    }


    



}
