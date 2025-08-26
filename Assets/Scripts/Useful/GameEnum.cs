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
    /// アイテムカテゴリー管理列挙定数
    /// </summary>
    public enum eItemCategory {
        Invalid = -1,
        FallSpeed,
        OnlyFruit,

        Max,
    }

    public enum eEffectCategory {
        Invalid = -1,
        Good,
        Bad,

        effectCategoryMax
    }

    /// <summary>
    /// 降ってくるスコアオブジェクト管理用列挙定数
    /// </summary>
    public enum FallObjectType {
        Invalid = -1,
        Fruit,
        Insect,
    }

    /// <summary>
    /// SE管理用列挙定数
    /// </summary>
    public enum SEIndex{
        Invalid = -1,
        FootSound,
        InsectSound,
        FruitSound,
        ItemSound,

        Max
    }

    public enum eCanvasType {
        Invalid = -1,
        InGameCanvas,
        OutGameCanvas,
        Ranking,

        Max
    }
}
