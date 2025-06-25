using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 得点を引いてくるクラス
/// </summary>
public class Insect : BaseScoreObject
{
    [SerializeField]
    private BaseScoreData scoreData;
    private void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        //y座標が0以下なら消す
        if (transform.position.y <= 0) {
            Destroy(gameObject);
        }

        FallInsect();
    }
    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Initialize() {
        SetScore(scoreData.score);
    }
    /// <summary>
    /// 虫を落下させる
    /// </summary>
    private void FallInsect() {
        Vector3 fallPos = transform.position;
        fallPos.y -= scoreData.fallSpeed;
        transform.position = fallPos;
    }
}
