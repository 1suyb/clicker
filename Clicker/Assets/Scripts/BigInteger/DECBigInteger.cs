using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DecBigInteger
{
    public struct BigIntUnit
{
    private uint _value;

    public uint Value
    {
        get => _value;
        set
        {
            _value = value > MAX ? MAX : value;
        }
    }
    public const uint MAX = 999_999_999;
    public const uint UNITSIZE = 1_000_000_000;
    public const int ZEROCOUNT = 9;
    public const uint STRINGPARSESIZE = 1_000;
    public const uint STRINGPARSEZEROCOUNT = 3;

    
    public BigIntUnit(uint value)
    {
        _value = value > MAX ? MAX : value;;
    }
}

public class BigInteger
{
    private BigIntUnit[] _array;
    public BigIntUnit[] List => _array;
    public int Length => _array.Length;

    private char[] _chars = new char[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
    };
    
    private int GetArraySize(string value)
    {
        int size = value.Length / BigIntUnit.ZEROCOUNT;
        size += value.Length % BigIntUnit.ZEROCOUNT;
        return size;
    }
    private int GetArraySize(ulong value)
    {
        int size = 0;
        while (value > 0)
        {
            value /=BigIntUnit.UNITSIZE;
            size++;
        }

        return size;
    }

    private int GetArraySize(uint value)
    {
        return 1;
    }
    
    private void Allocate(int size)
    {
        _array = new BigIntUnit[size];
    }
    public BigInteger(uint value)
    {
        Allocate(GetArraySize(value));
        int i = 0;
        while (value > 0)
        {
            uint portion = value/BigIntUnit.UNITSIZE;
            uint remain = value%BigIntUnit.UNITSIZE;
            _array[i] = new BigIntUnit(remain);
            i++;
            value = portion;
        }
    }
    public BigInteger(ulong value)
    {
        Allocate(GetArraySize(value));
        int i = 0;
        while (value > 0)
        {
            ulong portion = value/BigIntUnit.UNITSIZE;
            ulong remain = value%BigIntUnit.UNITSIZE;
            _array[i] = new BigIntUnit((uint)remain);
            i++;
            value = portion;
        }
    }
    public BigInteger(string value)
    {
        // 012 345 678 91011 121314 151617 181920 21
        // "123,456,789,123,456,789,123"
        // 91011 121314 151617 
        // "456,789,123" "456,789,132" "123"
        Allocate(GetArraySize(value));
        value = value.Replace(",", "");
        value = value.Replace(" ", "");
        int index = value.Length;
        int i = 0;
        while (index - BigIntUnit.ZEROCOUNT > 0)
        { 
            string temp = value.Substring(index-BigIntUnit.ZEROCOUNT, BigIntUnit.ZEROCOUNT);
            _array[i] = new BigIntUnit(uint.Parse(temp));
            i++;
            index -= BigIntUnit.ZEROCOUNT;
        }
        string t = value.Substring(0, index);
        _array[i] = new BigIntUnit(uint.Parse(t));
        i++;
    }

    public BigInteger(uint[] array)
    {
        BigIntUnit[] units = new BigIntUnit[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            units[i] = new BigIntUnit(array[i]);
        }
        _array = units;
    }

    public BigInteger(List<uint> array)
    {
        List<BigIntUnit> units = new List<BigIntUnit>();
        for (int i = 0; i < array.Count; i++)
        {
            units.Add(new BigIntUnit(array[i]));
        }
        _array = units.ToArray();
    }
    
    public new string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = _array.Length-1; i >=0; i--)
        {
            sb.Append(_array[i].Value.ToString());
        }
        return sb.ToString();
    }

    public string ToFormatString()
    {
        StringBuilder sb = new StringBuilder();
        List<string> list = new List<string>();
        int index = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            uint portion = _array[i].Value;
            while (portion > 0)
            {
                StringBuilder sec = new StringBuilder();
                uint remain = portion % BigIntUnit.STRINGPARSESIZE;
                portion /= BigIntUnit.STRINGPARSESIZE;
                sec.Append(remain.ToString());
                int charIndex = index;
                List<char> chars = new List<char>();
                while (charIndex  > 0)
                {
                    chars.Add(_chars[(charIndex-1) % _chars.Length]);
                    charIndex = (charIndex-1)/_chars.Length;
                }
                chars.Reverse();
                for (int j = 0; j < chars.Count; j++)
                {
                    sec.Append(chars[j].ToString());
                }

                list.Add(sec.ToString());
                index++;
            }
        }
        list.Reverse();
        for (int i = 0; i < list.Count; i++)
        {
            sb.Append(list[i]);
        }
        return sb.ToString();
    }

    public string ToShortString()
    {
        int index = (_array.Length - 1)*3;
        uint portion = _array[_array.Length-1].Value;

        while (portion/ BigIntUnit.STRINGPARSESIZE > 0)
        {
            portion /= BigIntUnit.STRINGPARSESIZE;
            index++;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(portion);
        List<char> chars = new List<char>();
        while (index  > 0)
        {
            chars.Add(_chars[(index-1) % _chars.Length]);
            index = (index-1)/_chars.Length;
        }
        chars.Reverse();
        for (int j = 0; j < chars.Count; j++)
        {
            sb.Append(chars[j].ToString());
        }
        return sb.ToString();
        
        
    }


    private static uint AddValueWithCarry(ref uint value, List<uint> values)
    {
        if (value > BigIntUnit.UNITSIZE)
        {
            values.Add(value - BigIntUnit.UNITSIZE);
            value = (value - (value - BigIntUnit.UNITSIZE)) / BigIntUnit.UNITSIZE;
        }
        else
        {
            values.Add(value);
            value = 0;
        }
        return value;
    }

    public static BigInteger operator +(BigInteger a, BigInteger b)
    {
        List<uint> values = new List<uint>();
        int minLength = Mathf.Min(a.Length, b.Length);
        int maxLength = Mathf.Max(a.Length, b.Length);
        uint value = 0;
        for (int i = 0; i < minLength; i++)
        {
            value += (a.List[i].Value + b.List[i].Value);
            AddValueWithCarry(ref value, values);
        }
        for (int i = 0; i < maxLength - minLength; i++)
        {
            if (a.Length > b.Length)
            {
                value += a.List[a.Length-1].Value;
            }
            else
            {
                value += b.List[a.Length-1].Value;
            }
            AddValueWithCarry(ref value, values);
        }
        if (value > 0)
        {
            while (value > 0)
            {
                AddValueWithCarry(ref value, values);
            }
        }
        return new BigInteger(values);
    }
    public static BigInteger operator -(BigInteger a, BigInteger b)
    {
        if (a < b)
        {
            return new BigInteger(0);
        }
        List<uint> values = new List<uint>();
        for (int i = 0; i < a.Length; i++)
        {
            values.Add(a.List[i].Value);
        }
        uint value = 0;
        int minIndex = Mathf.Min(a.Length,b.Length);
        for (int i = 0; i < minIndex; i++)
        {
            if (values[i] < b.List[i].Value)
            {
                values[i + 1] -= 1;
                value += values[i] + BigIntUnit.UNITSIZE - b.List[i].Value;
                values[i] = value;
                value = 0;
            }
            else
            {
                value += values[i] - b.List[i].Value;
                values[i] = value;
                value = 0;
            }
        }
        return new BigInteger(values);
    }

    public static BigInteger operator *(BigInteger a, BigInteger b)
    {
        List<uint> values = new List<uint>();
        ulong value = 0;
        int minIndex = Mathf.Min(a.Length, b.Length);
        int maxIndex = Mathf.Max(a.Length, b.Length);
        if (object.ReferenceEquals(a, null))
        {
            throw new ArgumentNullException("a");
        }
        if (object.ReferenceEquals(a, null))
        {
            throw new ArgumentNullException("b");
        }

        for (int i = 0; i < minIndex; i++)
        {
            value += (a.List[i].Value * b.List[i].Value);
            values.Add((uint)value%BigIntUnit.UNITSIZE);
            value = (value - (value%BigIntUnit.UNITSIZE))/BigIntUnit.UNITSIZE;
        }

        for (int i = minIndex; i < maxIndex; i++)
        {
            if (a.Length > b.Length)
            {
                value += a.List[a.Length-1].Value;
                values.Add((uint)value%BigIntUnit.UNITSIZE);
                value = (value - (value%BigIntUnit.UNITSIZE))/BigIntUnit.UNITSIZE;
            }
            else
            {
                value += b.List[a.Length-1].Value;
                values.Add((uint)value%BigIntUnit.UNITSIZE);
                value = (value - (value%BigIntUnit.UNITSIZE))/BigIntUnit.UNITSIZE;
            }
        }

        while (value > 0)
        {
            values.Add((uint)value%BigIntUnit.UNITSIZE);
            value = (value - (value%BigIntUnit.UNITSIZE))/BigIntUnit.UNITSIZE;
        }
        return new BigInteger(values);
    }
    
    public static bool operator >(BigInteger a, BigInteger b)
    {
        if (a.Length > b.Length)
        {
            return true;
        }
        if (a.Length < b.Length)
        {
            return false;
        }
        else
        {
            for (int i = a.Length-1; i >=0 ; i--)
            {
                if (a.List[i].Value > b.List[i].Value)
                {
                    return true;
                }
                else if (a.List[i].Value < b.List[i].Value)
                {
                    return false;
                }
            }
        }
        return false;
    }
    public static bool operator <(BigInteger a, BigInteger b)
    {
        if (a.Length > b.Length)
        {
            return false;
        }
        if (a.Length < b.Length)
        {
            return true;
        }
        else
        {
            for (int i = a.Length-1; i >=0 ; i--)
            {
                if (a.List[i].Value < b.List[i].Value)
                {
                    return true;
                }
                else if (a.List[i].Value > b.List[i].Value)
                {
                    return false;
                }
            }
        }
        return false;
    }
    public static bool operator >=(BigInteger a, BigInteger b)
    {
        return !(a < b);
    }
    public static bool operator <=(BigInteger a, BigInteger b)
    {
       return !(a > b);
    }

    public static bool operator ==(BigInteger a, BigInteger b)
    {
        if (object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
        {
            return true;
        }
        else if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
        {
            return false;
        }
        else
        {
            if (a.Length != b.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a.List[i].Value != b.List[i].Value)
                    {
                        return false;
                    }
                }
            }
        }
        
        return true;
    }
    public static bool operator !=(BigInteger a, BigInteger b)
    {
        return !(a == b);
    }
    
}

}
