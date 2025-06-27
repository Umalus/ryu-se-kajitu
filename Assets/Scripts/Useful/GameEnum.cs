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
    /// SE�Ǘ��p�񋓒萔
    /// </summary>
    public enum SEIndex{
        Invalid = -1,
        FootSound,
        InsectSound,
        FruitSound,

        Max
    }
}
