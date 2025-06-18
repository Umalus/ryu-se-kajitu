using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModul;
using static GameConst;

public class ScoreManager {
    public static int Score = 0;

    public static void UpdateScore() {
        if (Score < 0)
            Score = 0;
    }
    public static void AddScore(BaseScoreObject _scoreObject,int _combo) {
        Score += _scoreObject.score;
        CountScore(_combo);
    }

    public static void CountScore(int _combo) {
        if (InRange(_combo, FRUIT_FIRST_MIN, FRUIT_FIRST_MAX)) {
            Score += 100;
        }
        else if (InRange(_combo, FRUIT_FIRST_MAX, FRUIT_SECOND_MAX)) {
            Score += 200;
        }
        else if (InRange(_combo, FRUIT_SECOND_MAX, FRUIT_THIRD_MAX)) {
            Score += 300;
        }
    }
}
