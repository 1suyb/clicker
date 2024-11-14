using System;
using UnityEngine;

public class BigInteger
{
    private BigIntegerUnit _value;

    public BigIntegerUnit Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            OnChangeValueEvent?.Invoke(_value.ToSimbolString());
            Debug.Log((_value.ToSimbolString()));
        }
    }

    private void ChangeTopByte()
    {
        int maxCount = Mathf.Min(_topByte.Length, _value.Value.Length);
        for (int i = 0; i < maxCount; i++)
        {
            _topByte[i] = _value.Value[_value.Count - 1 - i];
        }
    }

    private bool CheckTopByte()
    {
        int maxCount = Mathf.Min(_topByte.Length, _value.Value.Length);
        for (int i = 0; i < maxCount; i++)
        {
            if (_topByte[i] != _value.Value[_value.Count - 1 - i])
            {
                return true;
            }
        }
        return false;
    }
    private byte[] _topByte = new byte[TopByteLength];
    private const int TopByteLength = 2;
    public event Action<string> OnChangeValueEvent;

    public override string ToString()
    {
        return _value.ToSimbolString();
    }

    public BigInteger(int value)
    {
        Value = new BigIntegerUnit(value);
    }
    public BigInteger(uint value)
    {
        Value = new BigIntegerUnit(value);
    }
    public BigInteger(long value)
    {
        Value = new BigIntegerUnit(value);
    }
    public BigInteger(ulong value)
    {
        Value = new BigIntegerUnit(value);
    }
    public BigInteger(string value)
    {
        Value = new BigIntegerUnit(value);
    }

    private BigInteger(BigIntegerUnit rightValue)
    {
        Value = rightValue;
    }

    #region 연산자 오버로드

    public static BigInteger operator +(BigInteger left, BigInteger right)
    {
        return new BigInteger(left.Value + right.Value);
    }
    public static BigInteger operator +(BigInteger left, int right)
    {
        return new BigInteger(left.Value + right);
    }

    public static BigInteger operator -(BigInteger left, BigInteger right)
    {
        return new BigInteger(left.Value - right.Value);
    }
    public static BigInteger operator -(BigInteger left, int right)
    {
        return new BigInteger(left.Value - right);
    }

    public static BigInteger operator *(BigInteger left, BigInteger right)
    {
        return new BigInteger(left.Value * right.Value);
    }
    public static BigInteger operator *(BigInteger left, int right)
    {
        return new BigInteger(left.Value * right);
    }
    public static BigInteger operator *(int left, BigInteger right)
    {
        return new BigInteger(left * right.Value);
    }

    public static BigInteger operator /(BigInteger left, BigInteger right)
    {
        return new BigInteger(left.Value / right.Value);
    }
    public static BigInteger operator %(BigInteger left, BigInteger right)
    {
        return new BigInteger(left.Value % right.Value);
    }
    public static BigInteger operator %(BigInteger left, int right)
    {
        return new BigInteger(left.Value % right);
    }

    public static BigInteger operator ++(BigInteger left)
    {
        return new BigInteger(left.Value + 1);
    }

    public static BigInteger operator --(BigInteger left)
    {
        return new BigInteger(left.Value - 1);
    }

    public static bool operator <(BigInteger left, BigInteger right)
    {
        return (left.Value < right.Value);
    }

    public static bool operator >(BigInteger left, BigInteger right)
    {
        return (left.Value > right.Value);
    }

    public static bool operator <=(BigInteger left, BigInteger right)
    {
        return (left.Value <= right.Value);
    }

    public static bool operator >=(BigInteger left, BigInteger right)
    {
        return (left.Value >= right.Value);
    }
    
    public static bool operator <(BigInteger left, int right)
    {
        return (left.Value < new BigIntegerUnit(right));
    }

    public static bool operator >(BigInteger left, int right)
    {
        return (left.Value >  new BigIntegerUnit(right));
    }

    public static bool operator <=(BigInteger left, int right)
    {
        return (left.Value <=  new BigIntegerUnit(right));
    }

    public static bool operator >=(BigInteger left, int right)
    {
        return (left.Value >=  new BigIntegerUnit(right));
    }

    
    public static bool operator ==(BigInteger left, BigInteger right)
    {
        if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
        {
            return true;
        }
        else if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
        {
            return false;
        }
        return (left.Value == right.Value);
    }

    public static bool operator !=(BigInteger left, BigInteger right)
    {
        if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
        {
            return true;
        }
        else if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
        {
            return false;
        }
        return (left.Value != right.Value);
    }

    public override bool Equals(object obj)
    {
        if (object.ReferenceEquals(this, obj))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    #endregion

    public void Add(BigInteger value)
    {
        Value += value.Value;
    }

    public void Sub(BigInteger value)
    {
        Value -= value.Value;
    }
}