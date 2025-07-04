using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static ItemUtility;

/// <summary>
/// アイテムの基底クラス
/// </summary>
public abstract class BaseItem : MonoBehaviour
{
    [SerializeField]
    protected float fallSpeed = 0.0f;
    public int ID { get; private set; } = -1;

    protected int categoryID = -1;
    protected void OnCollisionEnter(Collision collision) {
        //当たったオブジェクトのタグがプレイヤーなら
        if (collision.gameObject.CompareTag("Player")) {
            //効果適用
            AddEffect();
            //未使用状態にする
            UnuseObject(this, categoryID);
            AudioManager.instance.PlaySE((int)SEIndex.ItemSound,0.5f);
        }
            
    }
    public void Setup(Vector3 instancePos) {
        transform.position = instancePos;
    }

    protected void Update() {
        FallItem();
        

    }

    /// <summary>
    /// 与える影響関数の抽象メソッド
    /// </summary>
    public abstract void AddEffect();
    /// <summary>
    /// 効果を消す
    /// </summary>
    public abstract void DeleteEffect();
    public void FallItem() {
        Vector3 fallPosition = transform.position;
        fallPosition.y -= fallSpeed;
        
        if (fallPosition.y <= 0.5f)
            fallPosition.y = 0.5f;
        transform.position = fallPosition;
    }

    public void SetID(int _ID) {
        ID = _ID;
    }
}
