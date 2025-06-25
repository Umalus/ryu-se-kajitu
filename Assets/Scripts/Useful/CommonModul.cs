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
}
