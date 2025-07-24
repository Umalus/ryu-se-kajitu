using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class OffLineRanking : MonoBehaviour
{
    public static OffLineRanking instance = null;

    private List<RankingData> rankingDates;
    // Start is called before the first frame update
    void Start() {
        instance = this;
        //�����L���O������
        RankingLoad("testRanking");
    }

    //�����L���O�X�V����
    public void RankingUpdate(string _rankingKey, int _newScore) {

        //�����L���O�f�[�^���
        string rankingData = PlayerPrefs.GetString(_rankingKey, "500,400,300,200,100");

        //split�֐��Ńf�[�^��z��ɐ؂蕪����
        string[] rankingDataArray = rankingData.Split(",");

        //�����L���O�̐l�����L���b�V������
        int topNScore = rankingDataArray.Length;

        //�����L���O���1�傫�������L���O�z���V���ɍ쐬
        int[] rankingIntArray = new int[topNScore + 1];

        //�쐬�����z���int�ɕϊ������X�R�A����
        for (int i = 0; i < topNScore; i++) {
            rankingIntArray[i] = int.Parse(rankingDataArray[i]);
        }

        //rankingIntArra�̈�Ԍ��ɍŐV�̃X�R�A����
        rankingIntArray[topNScore] = _newScore;
        //�����L���O�������Ƀ\�[�g
        Array.Sort(rankingIntArray);
        //�\�[�g���������L���O�𔽓]������
        Array.Reverse(rankingIntArray);

        //�ۑ��p�̃����L���O�e�L�X�g�̏�����
        string upRoadRankingText = "";

        for (int i = 0; i < topNScore; i++) {
            if (i < topNScore - 1) {
                upRoadRankingText += rankingIntArray[i].ToString() + ",";
            }
            else {
                upRoadRankingText += rankingIntArray[i].ToString();
            }
        }
        //�����L���O�̕ۑ�
        PlayerPrefs.SetString(_rankingKey, upRoadRankingText);

        return;
    }

    public void RankingLoad(string _rankingKey) {
        //�����L���O�f�[�^�̏�����
        string rankingData = PlayerPrefs.GetString(_rankingKey, "500,400,300,200,100");

        string[] rankingDataArray = rankingData.Split(",");

        int topNScore = rankingDataArray.Length;

        for (int i = 0; i < topNScore; i++) {
            Debug.Log($"{i + 1}��:{rankingDataArray[i]}");
        }
        return;
    }
}
