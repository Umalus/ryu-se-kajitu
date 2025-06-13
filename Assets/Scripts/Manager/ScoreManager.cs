using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModul;

public class ScoreManager {
    public static int Score = 0;

    public static void CountScore(int _combo) {
        if (Score <= 0)
            Score = 0;
        if (InRange(_combo, 5, 15)) {
            Score += 100;
        }
        else if (InRange(_combo, 15, 20)) {
            Score += 200;
        }
        else if (InRange(_combo, 20, 1024)) {
            Score += 300;
        }
    }
}
