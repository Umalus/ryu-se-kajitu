using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[�������Ŏg�p����񋓒萔�N���X
/// </summary>
public static class GameEnum
{
    /// <summary>
    /// �Q�[���t�F�[�Y�Ǘ��p�񋓒萔
    /// </summary>
    public enum GamePhase {
        Invalid = -1,
        opening,
        middle,
        ending,
        PhaseEnd,
    }
    /// <summary>
    /// �A���Ǘ��p�񋓒萔
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
    /// �A�C�e���J�e�S���[�Ǘ��񋓒萔
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
    /// �~���Ă���X�R�A�I�u�W�F�N�g�Ǘ��p�񋓒萔
    /// </summary>
    public enum FallObjectType {
        Invalid = -1,
        Fruit,
        Insect,
    }

    /// <summary>
    /// SE�Ǘ��p�񋓒萔
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
