using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEnum {
    #region フルーツ獲得個数判断用定数
    public const int FRUIT_FIRST_MIN = 5;
    public const int FRUIT_FIRST_MAX = 15;
    public const int FRUIT_SECOND_MAX = 20;
    public const int FRUIT_THIRD_MAX = 4096;
    #endregion
    public const int MINUTE = 60;
    public enum GamePhase {
        Invalid = -1,
        opening,
        middle,
        ending,
    }
}
