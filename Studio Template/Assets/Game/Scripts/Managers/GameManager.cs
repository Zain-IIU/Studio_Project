using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static int CurrentLevel
    {

        get => PlayerPrefs.GetInt("CurrentLevel", 0);
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
            PlayerPrefs.Save();
        }
    }
       
 
    [SerializeField] GameObject[] levels;   
    void Start()
    {
        Application.targetFrameRate = 60;
        InitLevel();

    }
    void InitLevel()
    {        
        var old = GameObject.FindGameObjectsWithTag("Destroyable");
        foreach (var item in old)
        {
            Destroy(item);
        }

        var levelIndex = GetLevelIndex();
        var lvl = Instantiate(levels[levelIndex], 
            levels[levelIndex].transform.position, levels[levelIndex].transform.rotation);
        lvl.tag = "Destroyable";
        lvl.SetActive(true);      

        OnLevelStart();      
    }


    
    /// <summary>
    /// After 5 Levels repeat Level 3 to 5
    /// </summary>
    /// <returns> Current Level Index </returns>
    int GetLevelIndex()
    {
        if (CurrentLevel < 5)        
            return CurrentLevel;      
        else
        {
            int mod = ((CurrentLevel+1) % 6);
            return ((mod <= 2) ? mod + 2 : mod - 1);        
        }      
    }


    
    /// <summary>
    /// Trigger when Level Start
    /// </summary>
    void OnLevelStart()
    {
        print("Level Start");

        
    }



    /// <summary>
    ///  Trigger when Level Clear
    /// </summary>
    public void OnLevelClear()
    {
        print("Level Clear");      


        MAliGMethods.Wait(() => {

            UiManager.instance.ShowHideLevelCompleteUi(true);

        }, 1.5f, this);

    }

   

    /// <summary>
    /// Trigger when Level Failed
    /// </summary>
    public void OnLevelFailed()
    {
        print("Level Failed");

     

        MAliGMethods.Wait(() => {

            UiManager.instance.ShowHideLevelFailedUi(true);

        }, .5f, this);

    }


    public void StartNewLevel()
    {
        CurrentLevel++;
        InitLevel();
    }

    public void RestartLevel()
    {
        InitLevel();
    }

   

}