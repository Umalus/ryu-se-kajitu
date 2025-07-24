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
        //ランキング初期化
        RankingLoad("testRanking");
    }

    //ランキング更新処理
    public void RankingUpdate(string _rankingKey, int _newScore) {

        //ランキングデータ代入
        string rankingData = PlayerPrefs.GetString(_rankingKey, "500,400,300,200,100");

        //split関数でデータを配列に切り分ける
        string[] rankingDataArray = rankingData.Split(",");

        //ランキングの人数をキャッシュする
        int topNScore = rankingDataArray.Length;

        //ランキングより1大きいランキング配列を新たに作成
        int[] rankingIntArray = new int[topNScore + 1];

        //作成した配列にintに変換したスコアを代入
        for (int i = 0; i < topNScore; i++) {
            rankingIntArray[i] = int.Parse(rankingDataArray[i]);
        }

        //rankingIntArraの一番後ろに最新のスコアを代入
        rankingIntArray[topNScore] = _newScore;
        //ランキングを昇順にソート
        Array.Sort(rankingIntArray);
        //ソートしたランキングを反転させる
        Array.Reverse(rankingIntArray);

        //保存用のランキングテキストの初期化
        string upRoadRankingText = "";

        for (int i = 0; i < topNScore; i++) {
            if (i < topNScore - 1) {
                upRoadRankingText += rankingIntArray[i].ToString() + ",";
            }
            else {
                upRoadRankingText += rankingIntArray[i].ToString();
            }
        }
        //ランキングの保存
        PlayerPrefs.SetString(_rankingKey, upRoadRankingText);

        return;
    }

    public void RankingLoad(string _rankingKey) {
        //ランキングデータの初期化
        string rankingData = PlayerPrefs.GetString(_rankingKey, "500,400,300,200,100");

        string[] rankingDataArray = rankingData.Split(",");

        int topNScore = rankingDataArray.Length;

        for (int i = 0; i < topNScore; i++) {
            Debug.Log($"{i + 1}位:{rankingDataArray[i]}");
        }
        return;
    }
}
