using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModul;
using static GameConst;

public class ScoreManager {
    public static int AllScore = 0;
    public static void AddScore(BaseScoreObject _scoreObject,int _combo) {
        //�Ǘ����Ă��鑍�X�R�A�ɉ��Z
        AllScore += _scoreObject.score;
        CountScore(_combo);



        //�������X�R�A��0�����������0�ɂ���
        if (AllScore < 0)
            AllScore = 0;
    }
    /// <summary>
    /// �X�R�A�J�E���g�֐�
    /// </summary>
    /// <param name="_combo"></param>
    public static void CountScore(int _combo) {
        //�e�R���{�͈̔͂Ȃ����ǂ����𔻒�
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
