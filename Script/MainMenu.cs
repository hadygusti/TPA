using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    Resolution[] resolutions;
    public GameObject settingMenu, mainMenu;
    public TMP_Dropdown resolutionDrop, qualityDrop;
    bool isSetting;

    void Start()
    {
        resolutionDrop.ClearOptions();
        qualityDrop.ClearOptions();
        isSetting = false;

        resolutions = Screen.resolutions;
        List<string> resolutionList = new List<string>();

        foreach(Resolution currRes in resolutions)
        {
            string res = currRes.width + "x" + currRes.height;
            resolutionList.Add(res);
        }

        resolutionDrop.AddOptions(resolutionList);

        List<string> qualityList = new List<string>();
        string[] qualities = QualitySettings.names;

        foreach (string quality in qualities)
        {
            qualityList.Add(quality);
        }
        qualityDrop.AddOptions(qualityList);
    }

    void Update()
    {
        if (isSetting)
        {
            settingMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        else
        {
            settingMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void openSettings()
    {
        isSetting = true;
    }

    public void exitSettings()
    {
        isSetting = false;
    }

    public void quit()
    {
        Application.Quit();
    }

    public void fullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void PlayGame(){
        SceneManager.LoadScene("Game");
    }

    public void Resolution(int idx)
    {
        try
        {
            Screen.SetResolution(resolutions[idx].width, resolutions[idx].height, Screen.fullScreen);
        }
        catch(System.NullReferenceException e)
        {

        }
    }

    public void Quality(int idx)
    {
        QualitySettings.SetQualityLevel(idx, true);
    }
}
