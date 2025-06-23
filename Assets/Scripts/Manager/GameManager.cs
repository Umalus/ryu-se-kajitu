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
        instance = this;
        Application.targetFrameRate = 60;
        second = playTime;
        totalTime = minute * MINUTE + second;

        AudioManager.instance.PlayBGM(0);

        inputAction = InputSystemManager.instance.InputSystem;

        inputAction.GameManager.Start.performed += OnStartPreformed;
        inputAction.GameManager.End.performed += OnEndPreformed;
    }

    // Update is called once per frame
    void Update() {
        if (!IsPlay) return;
        Timer();
        Phase();
    }

    private void Timer() {
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

    private void Phase() {
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

    private void OnStartPreformed(InputAction.CallbackContext _context) {
        IsPlay = true;
    }

    private void OnEndPreformed(InputAction.CallbackContext _context) {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;

#else
         Application.Quit();
#endif
    }
}
