using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �֗��֐��N���X
/// </summary>
public static class CommonModul
{
    /// <summary>
    /// ���̒l���w��͈͓��ɓ����Ă��邩�ǂ���
    /// </summary>
    /// <param name="_value"></param>
    /// <param name="_min"></param>
    /// <param name="_max"></param>
    /// <returns></returns>
    public static bool InRange(int _value,int _min,int _max) {
        return _min <= _value && _value < _max;
    }
}
