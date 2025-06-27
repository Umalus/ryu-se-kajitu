using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの基底クラス
/// </summary>
public abstract class BaseItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player"))
            AddEffect();

        Debug.Log("!!!");
    }
    /// <summary>
    /// 与える影響関数の抽象メソッド
    /// </summary>
    public abstract void AddEffect();
    /// <summary>
    /// 効果を消す
    /// </summary>
    public abstract void DeleteEffect();
}
