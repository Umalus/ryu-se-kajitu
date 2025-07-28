using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
/// <summary>
/// 落ちてくるスピードを半分にする
/// </summary>
public class FallSpeedDown : BaseItem
{
    
    //スピードを取得するためのデータ
    [SerializeField]
    private List<BaseScoreData> itemObjectList;
    //設定するためのスピード
    private float setSpeed = 0.0f;
    //タイマー
    [SerializeField]
    float timer = 0.0f;


    private new void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);

    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0,max = itemObjectList.Count; i < max; i++) {
            setSpeed = itemObjectList[i].fallSpeed * 0.5f;
        }
        categoryID = (int)eItemCategory.FallSpeed;
    }
    private new void Update() {
        base.Update();
        
    }

    /// <summary>
    /// 取ったら与える効果
    /// </summary>
    public override void AddEffect() {
        BaseScoreObject.SetFallSpeed(setSpeed);
        BaseScoreObject.isHalfSpeed = true;
    }
    /// <summary>
    /// 効果を消す
    /// </summary>
    public override async UniTask DeleteEffect() {
        timer = 0.0f;
        while (true) {
            if (timer >= 10.0f)
                break;

            timer += Time.deltaTime;

            await UniTask.DelayFrame(1);

        }
        BaseScoreObject.isHalfSpeed = false;
    }

    
}
