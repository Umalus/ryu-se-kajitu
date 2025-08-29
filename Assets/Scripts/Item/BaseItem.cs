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
    float angle = 0.0f;
    public int ID { get; private set; } = -1;
    [SerializeField]
    private LayerMask downStopLayer;

    protected int categoryID = -1;
    protected async void OnCollisionEnter(Collision collision){
        //当たったオブジェクトのタグがプレイヤーなら
        if (collision.gameObject.CompareTag("Player")) {
            //効果適用
            AddEffect();
            AudioManager.instance.PlaySE((int)SEIndex.ItemSound, 0.5f);
            //未使用状態にする
            await UnuseObject(this, categoryID);
            
        }
            
    }
    public void Setup(Vector3 instancePos) {
        transform.position = instancePos;
    }

    protected async void Update() {
        UniTask task;
        if (!GameManager.instance.IsPlay)
            task = UnuseObject(this, categoryID);
        FallItem();
        RotatoItem();
        await UniTask.CompletedTask;
    }

    /// <summary>
    /// 与える影響関数の抽象メソッド
    /// </summary>
    public abstract void AddEffect();
    /// <summary>
    /// 効果を消す
    /// </summary>
    public abstract UniTask DeleteEffect();
    public void FallItem() {
        //現在の座標をキャッシュする
        Vector3 fallPosition = transform.position;
        //レイが当たるまでおとし続ける
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y * 2, downStopLayer)) {
            //fallPosition.y = hit.point.y;
            transform.position = fallPosition;
            return;
        }
        fallPosition.y -= fallSpeed;
        transform.position = fallPosition;
        //デバッグ用
        Debug.DrawRay(transform.position, Vector3.down * transform.localScale.y * 2,Color.cyan);
        
    }
    public void RotatoItem() {
        angle += 0.5f;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.up);
    }

    public void SetID(int _ID) {
        ID = _ID;
    }
}
