using PlayFab;
using PlayFab.ClientModels;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class OnlineRankingManager : MonoBehaviour
{
    public static OnlineRankingManager instance = null;

    private bool createAccount;

    private string customID;

    static readonly string CUSTOM_ID_SAVE_KEY = "TEST_RANKING_SAVE_KEY";

    static readonly string ID_CHARACTER = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private void Awake() {
        instance = this;
    }

    private void Start() {
        LoginRanking();
    }
    public void AddRankingData(string _name,int _score) {
        SetUserName(_name);
        SubmitScore(_score);
    }

    private void SetUserName(string _name) {
        // ユーザー名を設定するリクエストを作る
        var request = new UpdateUserTitleDisplayNameRequest {
            // ユーザー名の設定
            DisplayName = _name
        };

        // リクエストをPlayFabに送信する
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSetUserNameSuccess, OnSetUserNameFailure);

        // 送信成功時の処理
        void OnSetUserNameSuccess(UpdateUserTitleDisplayNameResult result) {
            Debug.Log("プレイヤー名変更成功");
        }

        // 送信失敗時の処理
        void OnSetUserNameFailure(PlayFabError error) {
            Debug.Log("プレイヤー名変更失敗");
        }
    }

    private void LoginRanking() {
        customID = LoadCustomID();
        
        var request = new LoginWithCustomIDRequest { CustomId = "testID", CreateAccount = true };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult _request) {
        //IDかぶった場合
        if(createAccount && !_request.NewlyCreated) {
            //再度ログイン
            LoginRanking();

            return;
        }

        if (_request.NewlyCreated) {
            SaveCustomID();
        }
        
        Debug.Log("ログイン成功");
        


    }
    private void OnLoginFailure(PlayFabError _error) {
        Debug.Log("ログイン失敗");
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
        Debug.Log("スコア送信成功");
    }
    private void OnSubmitScoreFailure(PlayFabError _error) {
        Debug.Log("スコア送信失敗");
    }

    private void GetRanking() {
        var request = new GetLeaderboardRequest {
            StatisticName = "TestRanking",

            StartPosition = 0,

            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, OnGetRankingSuccess, OnGetRankingFailure);
    }

    void OnGetRankingSuccess(GetLeaderboardResult _leaderboardResult) {
        foreach (var item in _leaderboardResult.Leaderboard) {
            Debug.Log($"{item.Position + 1}位　プレイヤー名:{item.DisplayName}　スコア:{int.MaxValue - item.StatValue}");
        }
    }

    void OnGetRankingFailure(PlayFabError _error) {
        Debug.Log("ランキング取得失敗");
    }
}
