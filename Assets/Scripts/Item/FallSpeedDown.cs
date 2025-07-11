using Cysharp.Threading.Tasks;
using UnityEngine;

using static GameEnum;
using static ItemUtility;
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
        categoryID = (int)eItemCategory.FallSpeed;
    }
    private new void Update() {
        base.Update();
        if (IsRunningTime)
            timer += Time.deltaTime;
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
    public override async UniTask DeleteEffect() {
        while (true) {
            if (timer >= 10.0f)
                break;

            timer += Time.deltaTime;

            await UniTask.DelayFrame(1);

        }
        Fruit.IsHalf = false;
    }

    
}
