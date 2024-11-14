using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIShopButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _incresevalueText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int index;
    private void OnEnable()
    {
        if (index == 0)
        {
            Shop.Instance.OnGoldIncrease += UpdateText;
        }
        else
        {
            Shop.Instance.OnGoldMultiplier += UpdateText;
        }
    }

    public void UpdateText(string increaseValue, string priceValue)
    {
        _incresevalueText.text = increaseValue;
        _priceText.text = priceValue;
    }
}
