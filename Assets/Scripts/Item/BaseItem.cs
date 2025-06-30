using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

/// <summary>
/// アイテムの基底クラス
/// </summary>
public abstract class BaseItem : MonoBehaviour
{
    [SerializeField]
    protected float fallSpeed = 0.0f;

    protected void OnCollisionEnter(Collision collision) {
        //当たったオブジェクトのタグがプレイヤーなら
        if (collision.gameObject.CompareTag("Player")) {
            //効果適用
            AddEffect();
            //消えたように見せる
            gameObject.SetActive(false);
            AudioManager.instance.PlaySE((int)SEIndex.ItemSound,0.5f);
        }
            
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
}
