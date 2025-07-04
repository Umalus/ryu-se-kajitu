using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 便利関数クラス
/// </summary>
public static class CommonModul
{
    /// <summary>
    /// その値が指定範囲内に入っているかどうか
    /// </summary>
    /// <param name="_value"></param>
    /// <param name="_min"></param>
    /// <param name="_max"></param>
    /// <returns></returns>
    public static bool InRange(int _value,int _min,int _max) {
        return _min <= _value && _value < _max;
    }

    public static bool IsEmpty<T>(List<T> _list) {
        //短絡評価なので大丈夫
        return _list == null || _list.Count <= 0;
    }
    public static bool IsEmpty<T>(T[] _array) {
        //短絡評価なので大丈夫
        return _array == null || _array.Length <= 0;
    }
}
