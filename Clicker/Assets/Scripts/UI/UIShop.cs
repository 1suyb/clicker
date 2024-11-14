using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Button _backGround;
    [SerializeField] private Button _addGoldIncreseButton;
    [SerializeField] private Button _multipleGoldIncreseButton;

    public void Init(Action addGoldIncreseButton, Action multipleGoldIncreseButton)
    {
        _addGoldIncreseButton.onClick.AddListener(() => { addGoldIncreseButton();});
        _multipleGoldIncreseButton.onClick.AddListener(() => { multipleGoldIncreseButton();});
        _backGround.onClick.AddListener(() => { this.gameObject.SetActive(false); });
    }
}
