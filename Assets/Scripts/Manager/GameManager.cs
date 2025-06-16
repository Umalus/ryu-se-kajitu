using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
/// <summary>
/// ゲーム内諸々管理クラス
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// インスタンス
    /// </summary>
    public static GameManager instance = null;

    /// <summary>
    /// 開始時間
    /// </summary>
    [SerializeField]
    private int playTime = 90;

    public float totalTime;
    /// <summary>
    /// 現在の時間
    /// </summary>
    public float second { get; private set; } = 0;

    public int minute { get; private set; } = 0;
    public float prevTime { get; private set; } = 0;

    public GamePhase phase { get; private set; } = 0;
    // Start is called before the first frame update
    void Start(){
        instance = this;
        Application.targetFrameRate = 60;
        second = playTime;
        totalTime = minute * MINUTE + second;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.UpdateScore();
        Timer();
        Phase();
    }

    private void Timer() {
        if(second <= 0) {
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
        if (totalTime < 30)
            phase = GamePhase.ending;
        else if (30 <= totalTime && totalTime < 60)
            phase = GamePhase.middle;
        else
            phase = GamePhase.opening;
    }
}
