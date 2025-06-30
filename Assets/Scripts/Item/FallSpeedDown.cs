using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 落ちてくるスピードを半分にする
/// </summary>
public class FallSpeedDown : BaseItem
{
    
    //スピードを取得するためのデータ
    [SerializeField]
    private BaseScoreData scoreData;
    //設定するためのスピード
    private float setSpeed = 0.0f;
    //タイマーを走らせるかどうか
    private static bool IsRunningTime = false;
    //タイマー
    [SerializeField]
    float timer = 0.0f;


    private new void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
        IsRunningTime = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        setSpeed = scoreData.fallSpeed * 0.5f;
    }
    private new void Update() {
        base.Update();
        if (IsRunningTime)
            timer += Time.deltaTime;

        if (timer >= 10.0f) {
            //付与された効果を消す
            DeleteEffect();
            //オブジェクトを消す
            Destroy(gameObject);
            //タイマーを止める
            IsRunningTime = false;
            //タイマーリセット
            timer = 0.0f;
        }
    }

    /// <summary>
    /// 取ったら与える効果
    /// </summary>
    public override void AddEffect() {
        Fruit.SetFallSpeed(setSpeed);
        Fruit.IsHalf = true;
    }
    /// <summary>
    /// 効果を消す
    /// </summary>
    public override void DeleteEffect() {
        Fruit.IsHalf = false;
    }

    
}
