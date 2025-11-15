using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

/// <summary>
/// 演出管理クラス
/// </summary>
public class ProductionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScoreProduction());
    }

    private IEnumerator ScoreProduction() {
        //1000点ごとに
        if(ScoreManager.AllScore > 0 && ScoreManager.AllScore % 1000 == 0) {
            //演出
            AudioManager.instance.PlaySE((int)SEIndex.ScoreEffect);
        }
        yield return null;
    }
}
