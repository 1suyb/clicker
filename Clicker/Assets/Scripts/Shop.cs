using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Singleton<Shop>
{
    [SerializeField] private UIShop _uiShop;
    private Gold _playerGold;
    private BigIntegerUnit _goldIncrease;
    private BigIntegerUnit _goldIncreaseCost;
    private BigIntegerUnit _goldMultiplier;
    private BigIntegerUnit _goldMultiCost;
    
    public Action<string,string> OnGoldIncrease;
    public Action<string,string> OnGoldMultiplier;

    
    private void Start()
    {
        _playerGold = GameManager.Instance.User.Gold;
        _goldIncrease = new BigIntegerUnit(1);
        _goldIncreaseCost = new BigIntegerUnit(1);
        _goldMultiplier = new BigIntegerUnit(10);
        _goldMultiCost = new BigIntegerUnit(1);

    }
    public void BuyGoldIncrease()
    {
        if (_playerGold.IsEnoughGold(_goldIncreaseCost))
        {
            _playerGold.SpendGold(_goldIncreaseCost);
            _playerGold.UpgradeGoldPerClick(_goldIncrease);
            _goldIncrease*=5;
            _goldIncreaseCost *= 5;
            OnGoldIncrease?.Invoke(_goldIncrease.ToDexString(),_goldIncreaseCost.ToDexString());
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
            OnGoldMultiplier?.Invoke(_goldMultiplier.ToDexString(),_goldMultiCost.ToDexString());
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    public void OpenShop()
    {
        _uiShop.gameObject.SetActive(true);
        _uiShop.Init(BuyGoldIncrease,BuyGoldMultiplier);
        OnGoldIncrease?.Invoke(_goldIncrease.ToDexString(),_goldIncreaseCost.ToDexString());
        OnGoldMultiplier?.Invoke(_goldMultiplier.ToDexString(),_goldMultiCost.ToDexString());
    }
    
}
