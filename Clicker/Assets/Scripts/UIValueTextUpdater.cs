
using System;
using TMPro;
using UnityEngine;

public class UIValueTextUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;

    private void Start()
    {
        GameManager.Instance.User.Gold.OnGoldChangeEvent += UpdateValueText;
    }

    public void UpdateValueText(int value)
    {
        valueText.text = value.ToString();
    }
}
