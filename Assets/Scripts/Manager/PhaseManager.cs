using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using static GameConst;
using static CommonModul;

/// <summary>
/// フェーズ管理クラス
/// </summary>
public class PhaseManager : MonoBehaviour {
    public static PhaseManager instance;
    //フェーズ管理用タイマーの定数
    private const int PHASE_OPNING_TIME_START = 90;
    private const int PHASE_OPNING_TIME_END = 70;
    private const int PHASE_MIDDLE_TIME_END = 20;
    private const int PHASE_ENDING_TIME_END = 1;
    public GamePhase phase { get; private set; } = 0;

    public bool meteorMode { get; private set; } = false;

    public void Start() {
        instance = this;
    }

    private void Update() {
        if (GameManager.instance.IsPlay)
            Phase(meteorMode);
    }
    /// <summary>
    /// 残り時間でフェーズ管理
    /// </summary>
    private void Phase(bool _isMeteor) {
        //メテオモードを優先的に判定
        if (_isMeteor) {
            phase = GamePhase.Meteor;
            return;
        }
        //メテオモードで無ければ通常のフェーズ判定
        float totalTime = GameManager.instance.GetTotalTime();
        //総時間が指定の値の範囲内ならフェーズを切り替える
        if (InRange((int)totalTime, PHASE_ENDING_TIME_END, PHASE_MIDDLE_TIME_END))
            phase = GamePhase.ending;
        else if (InRange((int)totalTime, PHASE_MIDDLE_TIME_END, PHASE_OPNING_TIME_END))
            phase = GamePhase.middle;
        else if (InRange((int)totalTime, PHASE_OPNING_TIME_END, PHASE_OPNING_TIME_START))
            phase = GamePhase.opening;
        else {
            phase = GamePhase.PhaseEnd;
            GameManager.instance.IsPlay = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// メテオモードの切り替え
    /// </summary>
    public void ChangeMeteorMode() {
        meteorMode = !meteorMode;
    }
}
