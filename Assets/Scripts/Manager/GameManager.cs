using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static GameConst;
using static CommonModul;
using UnityEngine.InputSystem;
using UnityEditor;
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

    public GamePhase phase { get; private set; } = 0;
    // Start is called before the first frame update
    void Start() {
        //外部から取得しやすいように自身のインスタンスを設定
        instance = this;
        //フレームレートを固定
        Application.targetFrameRate = 60;
        //時間設定
        second = playTime;
        totalTime = minute * MINUTE + second;
        //BGM再生
        AudioManager.instance.PlayBGM(0);
        //inputSystemの初期化
        inputAction = InputSystemManager.instance.InputSystem;

        inputAction.GameManager.Start.performed += OnStartPreformed;
        inputAction.GameManager.End.performed += OnEndPreformed;
    }

    // Update is called once per frame
    void Update() {
        //プレイできる状態でないなら処理しない
        if (!IsPlay) return;
        //タイマー処理
        Timer();
        //フェーズ処理
        Phase();
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

    /// <summary>
    /// フェーズ管理
    /// </summary>
    private void Phase() {
        //総時間が指定の値の範囲内ならフェーズを切り替える
        if (InRange((int)totalTime,1,30))
            phase = GamePhase.ending;
        else if (InRange((int)totalTime, 30, 60))
            phase = GamePhase.middle;
        else if (InRange((int)totalTime, 60, 90))
            phase = GamePhase.opening;
        else {
            phase = GamePhase.PhaseEnd;
            IsPlay = false;
        }
           
    }
    /// <summary>
    /// ゲーム開始
    /// </summary>
    /// <param name="_context"></param>
    private void OnStartPreformed(InputAction.CallbackContext _context) {
        IsPlay = true;
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
}
