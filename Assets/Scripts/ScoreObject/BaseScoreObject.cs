using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///　スコアオブジェクトの実物の基底クラス
/// </summary>
public abstract class BaseScoreObject : MonoBehaviour
{
    [SerializeField]
    protected BaseScoreData scoreData = null;

    public int score { get; private set; } = -1;

    protected static float fallSpeed = -1.0f;

    protected bool isGet;
    
    protected void SetScore(int _value) { score = _value; }

    public static bool isHalfSpeed;
    public virtual void Initialize() {
        SetScore(scoreData.score);
        if (isHalfSpeed)
            fallSpeed = scoreData.fallSpeed * 0.5f;

        else
            fallSpeed = scoreData.fallSpeed;
            
    }

    protected void fallObject() {
        //ポジションをキャッシュしnewを回避
        Vector3 fallPos = transform.position;
        fallPos.y -= fallSpeed;
        transform.position = fallPos;
    }

    /// <summary>
    /// オブジェクトを破棄した時の処理
    /// </summary>
    /// <param name="_seIndex"></param>
    public void DeleteObject(int _category, int _seIndex) {
        if(transform.position.y < 0 || isGet || !GameManager.instance.IsPlay) {
            //SE再生
            if (isGet)
                AudioManager.instance.PlaySE(_seIndex);
            FruitManager.instance.UnuseObject(this, _category);
        }
    }

    public void SetIsGet(bool _value) {
        isGet = _value;
    }

    public static void SetFallSpeed(float _speed) {
        fallSpeed = _speed;
    }
}
