using System;
using UnityEngine;

public class Gold
{
    private static bool _instantiated = false;
    private int _currentGold;
    private int _goldPerClick = 1;
    private int _goldBonusMultiplier = 1;

    public Gold()
    {
        Debug.Assert(!_instantiated);
        _instantiated = true;
    }

    public int CurrentGold
    {
        get => _currentGold;
        private set
        {
            _currentGold = value;
            OnGoldChangeEvent?.Invoke(_currentGold);
        }
    }
    public bool IsEnoughGold(int amount) => _currentGold >= amount;
    public Action<int> OnGoldChangeEvent;
    
    public void AddGold()
    {
        CurrentGold += _goldPerClick * _goldBonusMultiplier;
    }
    public void SpendGold(int amount)
    {
        Debug.Assert(IsEnoughGold(amount),"Not enough gold ");
        CurrentGold -= amount;
    }

    public void UpgradeGoldPerClick(int amount)
    {
        _goldPerClick += amount;
    }

    public void UpgradeGoldBonusMultiplier(int amount)
    {
        _goldBonusMultiplier += amount;
    }
}

