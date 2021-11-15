using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//TMP
using TMPro;

public class DropdowValueSave : MonoBehaviour
{
    const string PrefName = "optionvalue";
    public static TMP_Dropdown dropdown;

    public MainMenuButtons mainMenuButtons;

    void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(PrefName, dropdown.value);
            PlayerPrefs.Save();
        }));
    }

    void Start()
    {
        dropdown.value = PlayerPrefs.GetInt(PrefName, 0);
        mainMenuButtons.DropdownValueChanged(dropdown.value);
    }
}

//Credit: - Youtube: https://www.youtube.com/watch?v=3NzNIsJO2Hs
//        - GitHub: https://gist.github.com/codehoose/daa8ea19f142d4453e9a3bd6bd4b77b1