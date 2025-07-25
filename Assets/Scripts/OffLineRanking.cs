using System;
using System.Collections.Generic;
using UnityEngine;

public class OffLineRanking : MonoBehaviour
{
    [Serializable]
    private class RankingList {
        public List<RankingData> rankingList;
    }

    public static OffLineRanking instance = null;

    private List<RankingData> rankingDates;
    // Start is called before the first frame update
    void Start() {
        instance = this;
        //ランキング初期化
        RankingLoad("testRanking");
    }

    //ランキング更新処理
    public void AddRankingData(string _name, int _newScore) {
        RankingData newData = new RankingData(_name, _newScore,DateTime.Now);

        rankingDates.Add(newData);

        SaveRankingData();
        
    }

    public List<RankingData> GetRankingDatas() {
        rankingDates.Sort((x, y) => y.score.CompareTo(x.score));
        return rankingDates;
    }

    private void RankingLoad(string _rankingKey) {
        //ランキングデータの初期化
        string json = PlayerPrefs.GetString("RankingData","");

        if (!string.IsNullOrEmpty(json)) {
            RankingList rankingList = JsonUtility.FromJson<RankingList>(json);

            rankingDates = rankingList.rankingList;
        }
        else {
            rankingDates = new List<RankingData>();
        }
    }

    private void SaveRankingData() {
        rankingDates.Sort((x, y) => y.score.CompareTo(x.score));
        RankingList rankingList = new RankingList { rankingList = rankingDates };
        string json = JsonUtility.ToJson(rankingList);
        PlayerPrefs.SetString("RankingData", json);
        PlayerPrefs.Save();
    }
}
