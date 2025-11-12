using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static GameConst;
using static CommonModul;
using UnityEngine.InputSystem;
using UnityEditor;
using Cysharp.Threading.Tasks;
/// <summary>
/// ゲーム内諸々管理クラス
/// </summary>
public class GameManager : MonoBehaviour {
    /// <summary>
    /// インスタンス
    /// </summary>
    public static GameManager instance = null;
    //自身のInputSystem
    private Bozu inputAction = null;

    private bool isAddRanking = false;
    /// <summary>
    /// 開始時間
    /// </summary>
    [SerializeField]
    private int playTime = 90;

    public float totalTime;

    public bool IsPlay = false;
    /// <summary>
    /// 現在の時間
    /// </summary>
    public float second { get; private set; } = 0;

    public int minute { get; private set; } = 0;
    public float prevTime { get; private set; } = 0;

    

    [SerializeField]
    private Player player = null;
    // Start is called before the first frame update
    async void Start() {
        await Initialize();
        //BGM再生
        AudioManager.instance.PlayBGM(0);
        
    }

    // Update is called once per frame
    void Update() {
        //プレイできる状態でないなら処理しない
        if (!IsPlay) return;


        //タイマー処理
        Timer();
    }
    /// <summary>
    /// 時間管理
    /// </summary>
    private void Timer() {
        //タイマーが0以下なら処理しない
        if (second <= 0) {
            second = 0;
            return;
        }

        //時間を減らす
        totalTime = minute * MINUTE + second;
        totalTime -= Time.deltaTime;
        //時間の再設定
        minute = (int)totalTime / MINUTE;
        second = totalTime - minute * MINUTE;

        prevTime = second;
    }

    private async UniTask Initialize() {
        //外部から取得しやすいように自身のインスタンスを設定
        instance = this;
        //フレームレートを固定
        Application.targetFrameRate = 60;
        //時間設定
        second = playTime;
        totalTime = minute * MINUTE + second;

        //inputSystemの初期化
        inputAction = InputSystemManager.instance.InputSystem;

        inputAction.GameManager.Start.performed += OnStartPreformed;
        inputAction.GameManager.End.performed += OnEndPreformed;

        await FadeManager.instance.FadeIn();
    }

    public async void OnButtonReturnTitle() {
        AudioManager.instance.PlaySE((int)SEIndex.ClickButton);
        await FadeManager.instance.FadeOut();
        UIManager.instance.ResetUI();
        CameraManager.instance.ResetCamera();
        player.Reset();

        await FadeManager.instance.FadeIn(1);

        ResetGame();
    }

    private void ResetGame() {
        
        second = playTime;
        Player.SetCombo(0);
        
        inputAction.GameManager.Start.performed += OnStartPreformed;
        inputAction.GameManager.End.performed += OnEndPreformed;
    }
    public void OnButtonShowOfflineRanking() {
        OnlineRankingManager.instance.GetRanking();
        UIManager.instance.ShowOnlineRanking();
        UIManager.instance.HideCanvas((int)eCanvasType.OutGameCanvas);
        AudioManager.instance.PlaySE((int)SEIndex.ClickButton);
    }

    public void ExitRanking() {
        UIManager.instance.HideCanvas((int)eCanvasType.OfflineRanking);
        UIManager.instance.ShowCanvas((int)eCanvasType.OutGameCanvas);
        AudioManager.instance.PlaySE((int)SEIndex.ClickButton);
    }

    public void AddSocreData() {
        if (isAddRanking) return;
        OnlineRankingManager.instance.AddRankingData(UIManager.instance.GetInputName(),ScoreManager.AllScore);
        isAddRanking = true;
    }
    /// <summary>
    /// ゲーム開始
    /// </summary>
    /// <param name="_context"></param>
    private void OnStartPreformed(InputAction.CallbackContext _context) {
        IsPlay = true;
        ScoreManager.AllScore = 0;
        inputAction.GameManager.Start.performed -= OnStartPreformed;
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    /// <param name="_context"></param>
    private void OnEndPreformed(InputAction.CallbackContext _context) {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;

#else
         Application.Quit();
#endif
    }

    public async void GoTutorial() {
        IsPlay = false;
        await FadeManager.instance.FadeOut();
    }

    public void NotTutorial() {

    }

    public async void AddSecond(float _addTime) {
        second += _addTime;
        await UIManager.instance.ShowTimeAddUI(0.0f);
    }

    public float GetTotalTime() {  return totalTime; } 
}
