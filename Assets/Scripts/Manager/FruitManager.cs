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
    [SerializeField]
    private Vector3 InstancePos = Vector3.zero;
    [SerializeField]
    private int instanceValue = -1;
    [SerializeField]
    private float instanceTime = 0.0f;
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

        instanceTime += Time.deltaTime;
        InstancePos = DecideInstancePosition();
        instanceValue = Random.Range(0, 11);
        switch (GameManager.instance.phase) {
            case GamePhase.opening:
                    InstanceObject(2, 6);
                break;
            case GamePhase.middle:
                InstanceObject(1, 5);
                break;
            case GamePhase.ending:
                InstanceObject(0.1f, 2);
                break;
            case GamePhase.PhaseEnd:
                break;
        }

    }

    private Vector3 DecideInstancePosition() {
        //生成位置決定用一時変数
        Vector3 decidePos = InstancePos;
        decidePos.x = Random.Range(-InstanceRange, InstanceRange);
        decidePos.z = Random.Range(-InstanceRange, InstanceRange);
        return decidePos;
    }

    private void InstanceObject(float _interval, int _fruitRatio) {
        if (instanceTime >= _interval) {
            if (OnlyFruit) {
                //フルーツのみ生成
                Instantiate(originPrefabs[(int)FallObjectType.Fruit], InstancePos, Quaternion.identity);
                instanceTime = 0.0f;

            }
            //フルーツと虫両方生成
            else {
                if (instanceValue <= _fruitRatio) {
                    Instantiate(originPrefabs[(int)FallObjectType.Fruit], InstancePos, Quaternion.identity);
                    instanceTime = 0.0f;
                }
                else if (instanceValue > _fruitRatio) {
                    Instantiate(originPrefabs[(int)FallObjectType.Insect], InstancePos, Quaternion.identity);
                    instanceTime = 0.0f;
                }
            }


        }
    }
}
