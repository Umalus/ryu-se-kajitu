using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
/// <summary>
/// フルーツを降らせるマネージャー
/// </summary>
public class FruitManager : MonoBehaviour {
    public static FruitManager instance = null;


    [SerializeField, Header("生成されるフルーツのリスト")]
    private List<GameObject> originPrefabs = null;
    [SerializeField, Header("生成範囲")]
    private float InstanceRange = 0;
    //生成位置
    [SerializeField]
    private Vector3 InstancePos = Vector3.zero;
    //生成するための変数
    [SerializeField]
    private int instanceValue = -1;
    //生成するまでの時間
    [SerializeField]
    private float instanceTimer = 0.0f;
    public bool OnlyFruit = false;
    private enum FallObjectType {
        Invalid = -1,
        Fruit,
        Insect,
    }

    // Start is called before the first frame update
    void Start() {
        instance = this;
    }

    private void Update() {
        //ゲームプレイ状態でなければ処理しない
        if (!GameManager.instance.IsPlay) return;

        //フルーツの生成確率用変数( 10 - この値 が虫の生成確率)
        int fruitRatio = 0;
        //生成間隔
        float interval = 0.0f;
        //生成時間のタイマーを増加
        instanceTimer += Time.deltaTime;
        //生成位置の決定
        InstancePos = DecideInstancePosition();
        //生成値をランダムで決定
        instanceValue = Random.Range(0, 11);
        switch (GameManager.instance.phase) {
            //フェーズによって生成間隔や確率を変更
            case GamePhase.opening:
                interval = 2.0f;
                fruitRatio = 6;
                break;
            case GamePhase.middle:
                interval = 1.0f;
                fruitRatio = 5;
                break;
            case GamePhase.ending:
                interval = 0.1f;
                fruitRatio = 4;
                break;
            case GamePhase.PhaseEnd:
                interval = -1.0f;
                fruitRatio = -1;
                break;
        }
        InstanceObject(interval, fruitRatio);
    }

    private Vector3 DecideInstancePosition() {
        //生成位置決定用一時変数
        Vector3 decidePos = InstancePos;
        decidePos.x = Random.Range(-InstanceRange, InstanceRange);
        decidePos.z = Random.Range(-InstanceRange, InstanceRange);
        return decidePos;
    }

    private void InstanceObject(float _interval, int _fruitRatio) {
        //不正値が入ると処理しない
        if (_interval < 0 || _fruitRatio < 0) return;

        if (instanceTimer >= _interval) {
            if (OnlyFruit) {
                //フルーツのみ生成
                Instantiate(originPrefabs[(int)FallObjectType.Fruit], InstancePos, Quaternion.identity);
                instanceTimer = 0.0f;

            }
            //フルーツと虫両方生成
            else {
                if (instanceValue <= _fruitRatio) {
                    Instantiate(originPrefabs[(int)FallObjectType.Fruit], InstancePos, Quaternion.identity);
                    instanceTimer = 0.0f;
                }
                else if (instanceValue > _fruitRatio) {
                    Instantiate(originPrefabs[(int)FallObjectType.Insect], InstancePos, Quaternion.identity);
                    instanceTimer = 0.0f;
                }
            }
        }
    }
}
