using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager {
    public static int Score = 0;

    public static void CountScore() {
        if (Score <= 0)
            Score = 0;

    }
}
