using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static CommonModul;

/// <summary>
/// フルーツを降らせるマネージャー
/// </summary>
public class FruitManager : MonoBehaviour {
    public static FruitManager instance = null;


    [SerializeField, Header("生成されるフルーツのリスト")]
    private List<BaseScoreObject> originPrefabs = null;
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
    //使用中リスト
    private List<BaseScoreObject> useObjectList = null;
    //未使用リスト
    private List<List<BaseScoreObject>> unuseObjectList = null;
    //プーリング用親オブジェクト
    [SerializeField]
    private Transform useRoot = null;
    [SerializeField]
    private Transform unuseRoot = null;

    private const int MAX_OBJECT = 128;


    

    // Start is called before the first frame update
    void Start() {
        Initialize();
    }

    private void Initialize() {
        //インスタンスに自身を設定
        instance = this;
        //リストを初期化
        useObjectList = new List<BaseScoreObject>(MAX_OBJECT);
        unuseObjectList = new List<List<BaseScoreObject>>(2);

        int halfOfMaxObject = MAX_OBJECT / 2;

        for(int i = 0; i < 2; i++) {
            unuseObjectList.Add(new List<BaseScoreObject>(halfOfMaxObject));
            for(int objCount = 0;objCount < halfOfMaxObject;objCount++ ){
                unuseObjectList[i].Add(Instantiate(originPrefabs[i], unuseRoot));
            }
        }
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
                UseObject((int)FallObjectType.Fruit, InstancePos);
                instanceTimer = 0.0f;

            }
            //フルーツと虫両方生成
            else {
                if (instanceValue <= _fruitRatio) {
                    UseObject((int)FallObjectType.Fruit, InstancePos);
                    instanceTimer = 0.0f;
                }
                else if (instanceValue > _fruitRatio) {
                    UseObject((int)FallObjectType.Insect, InstancePos);
                    instanceTimer = 0.0f;
                }
            }
        }
    }

    private void UseObject(int _category,Vector3 _instancePos) {
        //未使用リストが空なら処理しない
        if (IsEmpty(unuseObjectList)) return;
        //使用するオブジェクトをキャッシュ
        BaseScoreObject useObj = unuseObjectList[_category][0];
        //未使用リストから取り除く
        unuseObjectList[_category].RemoveAt(0);
        //使用中の親オブジェクトに設定
        useObj.transform.SetParent(useRoot);
        useObj.transform.position = _instancePos;

        useObjectList.Add(useObj);
    }

    public void UnuseObject(BaseScoreObject _obj,int _category) {
        if (_obj == null) return;
        //使用中リストから削除
        useObjectList.Remove(_obj);
        unuseObjectList[_category].Add(_obj);
        //親オブジェクト設定
        _obj.transform.SetParent(unuseRoot);

    }
}
