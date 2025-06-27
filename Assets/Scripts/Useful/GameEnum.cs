using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内部で使用する列挙定数クラス
/// </summary>
public static class GameEnum
{
    /// <summary>
    /// ゲームフェーズ管理用列挙定数
    /// </summary>
    public enum GamePhase {
        Invalid = -1,
        opening,
        middle,
        ending,
        PhaseEnd,
    }
    /// <summary>
    /// 連鎖管理用列挙定数
    /// </summary>
    public enum ComboMode {
        Invalid = -1,
        Easy,
        Normal,
        Hard,
        Imposible,

        max,
    }
    /// <summary>
    /// SE管理用列挙定数
    /// </summary>
    public enum SEIndex{
        Invalid = -1,
        FootSound,
        InsectSound,
        FruitSound,

        Max
    }
}
