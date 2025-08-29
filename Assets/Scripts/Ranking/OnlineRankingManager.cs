using PlayFab;
using PlayFab.ClientModels;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnlineRankingManager : MonoBehaviour
{
    [Serializable]
    private class RankingList {
        public List<RankingData> rankingList;
    }

    public static OnlineRankingManager instance = null;

    private bool createAccount;

    private string customID;

    static readonly string CUSTOM_ID_SAVE_KEY = "TEST_RANKING_SAVE_KEY";

    static readonly string ID_CHARACTER = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private List<RankingData> rankingDatas;
    private void Awake() {
        instance = this;
    }

    private void Start() {
        LoginRanking();
    }
    public void AddRankingData(string _name,int _score) {
        SetUserName(_name);
        SubmitScore(_score);

        RankingData addDate = new RankingData(_name, _score,DateTime.Now);

        rankingDatas.Add(addDate);

        SaveRankingData();
    }

    private void SaveRankingData() {
        rankingDatas.Sort((x, y) => y.score.CompareTo(x.score));
        RankingList rankingList = new RankingList { rankingList = rankingDatas };
        string json = JsonUtility.ToJson(rankingList);
        PlayerPrefs.SetString("TestRanking", json);
        PlayerPrefs.Save();
    }

    public List<RankingData> GetOnlineRankingData() {
        return rankingDatas;
    }

    private void SetUserName(string _name) {
        // ���[�U�[����ݒ肷�郊�N�G�X�g�����
        var request = new UpdateUserTitleDisplayNameRequest {
            // ���[�U�[���̐ݒ�
            DisplayName = _name
        };
        
        // ���N�G�X�g��PlayFab�ɑ��M����
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSetUserNameSuccess, OnSetUserNameFailure);

        // ���M�������̏���
        void OnSetUserNameSuccess(UpdateUserTitleDisplayNameResult result) {
            Debug.Log("�v���C���[���ύX����");
        }

        // ���M���s���̏���
        void OnSetUserNameFailure(PlayFabError error) {
            Debug.Log("�v���C���[���ύX���s");
        }
    }

    private void LoginRanking() {
        customID = LoadCustomID();
        
        var request = new LoginWithCustomIDRequest { CustomId = "testID", CreateAccount = true };
        //�����L���O�f�[�^�̏�����
        string json = PlayerPrefs.GetString("TestRanking", "");
        if (!string.IsNullOrEmpty(json)) {
            RankingList rankingList = JsonUtility.FromJson<RankingList>(json);

            rankingDatas = rankingList.rankingList;
        }
        else {
            rankingDatas = new List<RankingData>();
        }

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult _request) {
        //ID���Ԃ����ꍇ
        if(createAccount && !_request.NewlyCreated) {
            //�ēx���O�C��
            LoginRanking();

            return;
        }

        if (_request.NewlyCreated) {
            SaveCustomID();
        }
        
        Debug.Log("���O�C������");
        


    }
    private void OnLoginFailure(PlayFabError _error) {
        Debug.Log("���O�C�����s");
        Debug.LogError(_error.GenerateErrorReport());

    }

    private void SaveCustomID() {
        PlayerPrefs.SetString(CUSTOM_ID_SAVE_KEY, customID);
    }

    private string LoadCustomID() {
        string id = PlayerPrefs.GetString(CUSTOM_ID_SAVE_KEY);

        createAccount = string.IsNullOrEmpty(id);

        return createAccount ? GenerateCustomID() : id;
    }

    private string GenerateCustomID() {
        int idLength = 32;

        StringBuilder stringBuilder = new StringBuilder(idLength);

        var random = new System.Random();

        for(int i = 0; i < idLength; i++) {
            stringBuilder.Append(ID_CHARACTER[random.Next(ID_CHARACTER.Length)]);
        }

        return stringBuilder.ToString();
    }

    private void SubmitScore(int _score) {
        
        var statisticUpdate = new StatisticUpdate {
            StatisticName = "TestRanking",

            Value = _score,
        };
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                statisticUpdate
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSubmitScoreSuccess, OnSubmitScoreFailure);
    }
    private void OnSubmitScoreSuccess(UpdatePlayerStatisticsResult _result) {
        Debug.Log("�X�R�A���M����");
    }
    private void OnSubmitScoreFailure(PlayFabError _error) {
        Debug.Log("�X�R�A���M���s");
    }

    public void GetRanking() {
        var request = new GetLeaderboardRequest {
            StatisticName = "TestRanking",

            StartPosition = 0,

            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, OnGetRankingSuccess, OnGetRankingFailure);
    }

    void OnGetRankingSuccess(GetLeaderboardResult _leaderboardResult) {
        foreach (var item in _leaderboardResult.Leaderboard) {
            Debug.Log($"{item.Position + 1}�ʁ@�v���C���[��:{item.DisplayName}�@�X�R�A:{item.StatValue}");

            
        }

        for(int i = 0,max = 10;i < max; i++) {
            var item = _leaderboardResult.Leaderboard;
            rankingDatas[i].score = item[i].StatValue;
            rankingDatas[i].name = item[i].DisplayName;
        }
    }

    void OnGetRankingFailure(PlayFabError _error) {
        Debug.Log("�����L���O�擾���s");
    }
}
