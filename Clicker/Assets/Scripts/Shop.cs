using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private Gold _playerGold;
    private int _goldIncrease = 1;
    private int _goldIncreaseCost = 1;
    private int _goldMultiplier = 2;
    private int _goldMultiCost = 10;
    [SerializeField] private Item[] _items;

    private void Start()
    {
        _playerGold = GameManager.Instance.User.Gold;
    }
    public void BuyGoldIncrease()
    {
        if (_playerGold.IsEnoughGold(_goldIncreaseCost))
        {
            _playerGold.SpendGold(_goldIncreaseCost);
            _playerGold.UpgradeGoldPerClick(_goldIncrease);
            _goldIncrease++;
            _goldIncreaseCost *= 5;
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    public void BuyGoldMultiplier()
    {
        if (_playerGold.IsEnoughGold(_goldMultiCost))
        {
            _playerGold.SpendGold(_goldMultiCost);
            _playerGold.UpgradeGoldBonusMultiplier(_goldMultiplier);
            _goldMultiCost *= 100;
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(20, 70, 300, 200), "GoldIncrease"))
        {
            BuyGoldIncrease();
        }

        if (GUI.Button(new Rect(20, 270, 300, 200), "GoldMultiplier"))
        {
            BuyGoldMultiplier();
        }
    }
}
