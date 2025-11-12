using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static GameConst;
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
    public bool isDemo = false;
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

        for (int i = 0, max = (int)FallObjectType.FallObjMax; i < max; i++) {
            unuseObjectList.Add(new List<BaseScoreObject>(halfOfMaxObject));
            for (int objCount = 0; objCount < halfOfMaxObject; objCount++) {
                unuseObjectList[i].Add(Instantiate(originPrefabs[i], unuseRoot));
            }
        }
    }

    private void Update() {
        //フルーツの生成確率用変数( 10 - この値 が虫の生成確率)
        int fruitRatio = 0;
        //生成間隔
        float interval = 0.0f;
        if (isDemo) {
            OnlyFruit = true;
            interval = 1.0f;
            fruitRatio = 5;
            //生成時間のタイマーを増加
            instanceTimer += Time.deltaTime;
            //生成位置の決定
            InstancePos = DecideInstancePosition();
            instanceValue = 5;
            InstanceObject(interval, fruitRatio);
            return;
        }

        //ゲームプレイ状態でなければ処理しない
        if (!GameManager.instance.IsPlay) return;
        //生成時間のタイマーを増加
        instanceTimer += Time.deltaTime;
        //生成位置の決定
        InstancePos = DecideInstancePosition();
        //生成値をランダムで決定(%)
        instanceValue = Random.Range(0, 100);

        //フェーズで生成頻度等切り替え
        switch (PhaseManager.instance.phase) {
            //フェーズによって生成間隔や確率を変更
            case GamePhase.opening:
                interval = 2.0f;
                fruitRatio = 60;
                break;
            case GamePhase.middle:
                interval = 1.0f;
                fruitRatio = 50;
                break;
            case GamePhase.ending:
                interval = 0.3f;
                fruitRatio = 50;
                break;
            case GamePhase.PhaseEnd:
                interval = -1.0f;
                fruitRatio = -1;
                break;
            case GamePhase.Meteor:
                OnlyFruit = true;
                interval = 0.1f;
                //本来フルーツのみの生成になっているが万が一生成レートが変になっていると生成出来ないので明示的に変更
                fruitRatio = 100;
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
            //どのフルーツを生成するかの変数
            int FruitRatio = Random.Range(0, APPLE_RATIO + BANANA_RATIO + PINE_RATIO);
            if (OnlyFruit) {
                //フルーツのみ生成
                InstanceFruit(FruitRatio);
                instanceTimer = 0.0f;

            }
            //フルーツと虫両方生成
            else {
                if (instanceValue <= _fruitRatio) {
                    InstanceFruit(FruitRatio);
                    instanceTimer = 0.0f;
                }
                else {
                    UseObject((int)FallObjectType.Insect, InstancePos);
                    instanceTimer = 0.0f;
                }
            }
        }
    }

    private void InstanceFruit(int _ratio) {
        //パイン
        if (_ratio >= APPLE_RATIO + BANANA_RATIO && _ratio < APPLE_RATIO + BANANA_RATIO + PINE_RATIO)
            UseObject((int)FallObjectType.PineApple, InstancePos);
        //バナナ
        else if (_ratio >= APPLE_RATIO && _ratio < APPLE_RATIO + BANANA_RATIO)
            UseObject((int)FallObjectType.Banana, InstancePos);
        //アップル
        else if (_ratio < APPLE_RATIO)
            UseObject((int)FallObjectType.Apple, InstancePos);
    }

    private void UseObject(int _category, Vector3 _instancePos) {
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

    public void UnuseObject(BaseScoreObject _obj, int _category) {
        if (_obj == null) return;
        //使用中リストから削除
        useObjectList.Remove(_obj);
        unuseObjectList[_category].Add(_obj);
        //親オブジェクト設定
        _obj.transform.SetParent(unuseRoot);

    }
}
