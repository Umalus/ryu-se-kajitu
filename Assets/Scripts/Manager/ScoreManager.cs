using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModul;
using static GameConst;

public class ScoreManager {
    public static int AllScore = 0;
    public static void AddScore(BaseScoreObject _scoreObject,int _combo) {
        //管理している総スコアに加算
        AllScore += _scoreObject.score;
        CountScore(_combo);



        //もし総スコアが0を下回ったら0にする
        if (AllScore < 0)
            AllScore = 0;
    }
    /// <summary>
    /// スコアカウント関数
    /// </summary>
    /// <param name="_combo"></param>
    public static void CountScore(int _combo) {
        //各コンボの範囲ないかどうかを判定
        if (InRange(_combo, FRUIT_FIRST_MIN, FRUIT_FIRST_MAX)) {
            AllScore += 100;
        }
        else if (InRange(_combo, FRUIT_FIRST_MAX, FRUIT_SECOND_MAX)) {
            AllScore += 200;
        }
        else if (InRange(_combo, FRUIT_SECOND_MAX, FRUIT_THIRD_MAX)) {
            AllScore += 300;
        }
    }
}
