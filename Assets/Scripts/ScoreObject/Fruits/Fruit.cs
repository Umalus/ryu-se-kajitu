using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

public class Fruit : BaseScoreObject {
    [SerializeField]
    private BaseScoreData scoreData = null;
    [SerializeField]
    private static float fallSpeed;
    //速度半減かどうか
    public static bool IsHalf = false;
    private void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        //降らせる
        FallFruit();
        //破棄する処理
        DeleteObject((int)FallObjectType.Fruit ,(int)SEIndex.FruitSound);
    }
    private void Initialize() {
        //スコアデータに保存されてるスコアを代入
        SetScore(scoreData.score);
        //もしスピード半分状態じゃなければ生成時に落ちる速度を再設定
        if (!IsHalf)
            fallSpeed = scoreData.fallSpeed;
    }
    /// <summary>
    /// 落下処理
    /// </summary>
    private void FallFruit() {
        //ポジションをキャッシュしnewを回避
        Vector3 fallPos = transform.position;
        fallPos.y -= fallSpeed;
        transform.position = fallPos;
    }
    /// <summary>
    /// 落下速度設定
    /// </summary>
    /// <param name="_speed"></param>
    public static void SetFallSpeed(float _speed) {
        fallSpeed = _speed;
    }
}
