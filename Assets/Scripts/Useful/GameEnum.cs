using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEnum
{
    public enum GamePhase {
        Invalid = -1,
        opening,
        middle,
        ending,
        PhaseEnd,
    }

    public enum ComboMode {
        Invalid = -1,
        Easy,
        Normal,
        Hard,
        Imposible,

        max,
    }
}
