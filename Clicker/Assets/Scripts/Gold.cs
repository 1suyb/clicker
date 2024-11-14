using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class Gold
{
    private static bool _instantiated = false;
    private BigIntegerUnit _currentGold;
    private BigIntegerUnit _goldPerClick;
    private BigIntegerUnit _goldBonusMultiplier;
    private string _pattern = @"\d+[a-zA-Z]";
    public Gold()
    {
        Debug.Assert(!_instantiated);
        _instantiated = true;
        
        _currentGold = new BigIntegerUnit(0);
        _goldPerClick = new BigIntegerUnit(1);
        _goldBonusMultiplier = new BigIntegerUnit(1);

    }

    public BigIntegerUnit CurrentGold
    {
        get => _currentGold;
        private set
        {
            _currentGold = value;
            OnGoldChangeEvent?.Invoke(_currentGold.ToDexString());
        } 
    }
    public bool IsEnoughGold(BigIntegerUnit amount) => _currentGold >= amount;
    public Action<string> OnGoldChangeEvent;

    
    
    public void AddGold()
    {
        CurrentGold+=(_goldPerClick*_goldBonusMultiplier);
    }
    public void SpendGold(BigIntegerUnit amount)
    {
        Debug.Assert(IsEnoughGold(amount),"Not enough gold ");
        CurrentGold-=(amount);
    }

    public void UpgradeGoldPerClick(BigIntegerUnit amount)
    {
        _goldPerClick += amount;
    }

    public void UpgradeGoldBonusMultiplier(BigIntegerUnit amount)
    {
        _goldBonusMultiplier += amount;
    }
}

