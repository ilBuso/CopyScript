using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    //Sensitivity
    public Slider sliderSensitivity;
    public TMP_Text sesitivityText;

    //Graphic
    public TMP_Dropdown dropdownGraphic;
    
    //Resolution
    Resolution[] resolutions;
    public TMP_Dropdown dropdownResolution;

    //Volume
    public AudioMixer audioMixer;
    public Slider sliderVolume;

    //Save Variables
    const string PrefNameSensitivity = "optionvaluesensitivity";
    const string PrefNameGraphic = "optionvaluegraphic";
    const string PrefNameResolution = "optionvalueresolution";
    const string PrefNameVolume = "optionvaluevolume";

    void Start()
    {
        //SetValues
        sliderSensitivity.value = PlayerPrefs.GetFloat("optionvaluesensitivity");
        dropdownGraphic.value = PlayerPrefs.GetInt("optionvaluegraphic");
        sliderVolume.value = PlayerPrefs.GetFloat("optionvaluevolume");

        //Resolution
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        for (int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }

        dropdownResolution.AddOptions(options);

        dropdownResolution.value = PlayerPrefs.GetInt("optionvalueresolution");

        PlayerPrefs.Save();
    }

    //Sensitivity
    public void SetSensitivity(float value)
    {
        //UI
        int a = (int)value;
        sesitivityText.text = a.ToString();

        //Save
        PlayerPrefs.SetFloat(PrefNameSensitivity, value);
        PlayerPrefs.Save();
    }

    //Graphic
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        //Save
        PlayerPrefs.SetInt(PrefNameGraphic, qualityIndex);
        PlayerPrefs.Save();
    }


    //Resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        //Save
        PlayerPrefs.SetInt(PrefNameResolution, resolutionIndex);
        PlayerPrefs.Save();
    }

    //Volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        //Save
        PlayerPrefs.SetFloat(PrefNameVolume, volume);
        PlayerPrefs.Save();
    }
}
