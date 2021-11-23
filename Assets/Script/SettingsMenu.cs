using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    //Volume
    public AudioMixer audioMixer;

    //Sensitivity
    public Slider sensitivitySlider;

    //Resolution
    Resolution[] resolutions;
    public TMP_Dropdown dropdown;

    //Save Variables
    const string PrefNameSensitivity = "optionvaluesensitivity";
    const string PrefNameGraphic = "optionvaluegraphic";
    const string PrefNameResolution = "optionvalueresolution";
    const string PrefNameVolume = "optionvalueVolume";

    void OnEnable()
    {
        //Set Values
        //SetSensitivity(PlayerPrefs.GetInt(PrefNameSensitivity, 100));
        SetQuality(PlayerPrefs.GetInt(PrefNameGraphic, 0));
        SetResolution(PlayerPrefs.GetInt(PrefNameResolution, 0));
        SetVolume(PlayerPrefs.GetFloat(PrefNameVolume, 0));
    }

    void Start()
    {
        resolutions = Screen.resolutions;

        dropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        dropdown.AddOptions(options);
        dropdown.value = currentResolution;
        dropdown.RefreshShownValue();
    }


    public float SetSensitivity()
    {
        float a = sensitivitySlider.value;

        //Save
        PlayerPrefs.SetFloat(PrefNameSensitivity, a);
        PlayerPrefs.Save();

        return a;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        //Save
        PlayerPrefs.SetInt(PrefNameGraphic, qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        //Save
        PlayerPrefs.SetInt(PrefNameResolution, resolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        //Save
        PlayerPrefs.SetFloat(PrefNameVolume, volume);
        PlayerPrefs.Save();
    }
}
