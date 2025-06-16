using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private const int PLAY_TIME = 90;
    /// <summary>
    /// 現在の時間
    /// </summary>
    public float currentTime { get; private set; } = PLAY_TIME;

    public int minute;
    public float prevTime;
    // Start is called before the first frame update
    void Start(){
        instance = this;
        Application.targetFrameRate = 60;
        minute = 0;
        prevTime = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.UpdateScore();
        //時間を減らす
        currentTime -= Time.deltaTime;
        if(currentTime >= 60) {
            minute++;
            currentTime -= 60.0f;
        }
        prevTime = currentTime;
    }
}
