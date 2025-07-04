using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModul;
using static GameEnum;
/// <summary>
/// アイテム管理クラス
/// </summary>
public class ItemManager : MonoBehaviour {
    //自身のインスタンス
    public static ItemManager instance = null;
    //生成するオブジェクトのオリジナル
    [SerializeField]
    private List<BaseItem> originItems = null;
    //アイテムの生成位置
    private Vector3 InstancePos = Vector3.zero;
    //アイテム生成用のタイマー
    private float timer = 0.0f;
    //アイテム生成の間隔
    private const float INTERVAL = 10.0f;
    //アイテム生成範囲
    private const float INSTANCERANGE = 10.0f;
    //オブジェクトプーリング用親オブジェクトのTransform
    [SerializeField]
    private Transform UseRoot = null;
    [SerializeField]
    private Transform UnuseRoot = null;
    //プーリング用変数
    //アイテムの最大数
    private const int MAX_OBJECT = 4;
    //使用状態リスト
    private List<BaseItem> useObjectList = null;
    //未使用状態リスト
    private List<List<BaseItem>> unuseObjectList = null;
    
    

    private void Start() {
        Initialized();


    }

    private void Update() {
        InstanceItem();
    }

    private void Initialized() {
        //インスタンスを自身に設定
        instance = this;
        //リストを初期化
        useObjectList = new List<BaseItem>(MAX_OBJECT);
        unuseObjectList = new List<List<BaseItem>>((int)eItemCategory.Max);
        //今回は最大数を各アイテムの種類で割った数生成する
        int objectCountPerCategory = MAX_OBJECT / (int)eItemCategory.Max;

        //未使用状態で複製
        for (int i = 0,max = (int)eItemCategory.Max; i < max; i++) {
            unuseObjectList.Add(new List<BaseItem>(objectCountPerCategory));
            for(int itemCount = 0; itemCount < objectCountPerCategory; itemCount++) {
                //未使用状態にしてリストに追加
                unuseObjectList[i].Add(Instantiate(originItems[i],UnuseRoot));
                unuseObjectList[i][itemCount].SetID(itemCount);
                
            }
        }
        
    }

    private void InstanceItem() {
        if (!GameManager.instance.IsPlay) return;

        //タイマーをインクリメント
        timer += Time.deltaTime;
        //生成用変数を乱数によって設定
        int instanceValue = Random.Range(0, 10);
        //タイマーがインターバルを超えたら
        if (timer >= INTERVAL) {

            //乱数によって生成する物を変更し生成
            if (InRange(instanceValue, 0, 5))
                UseObject((int)eItemCategory.FallSpeed);
            else
                UseObject((int)eItemCategory.OnlyFruit);
            // タイマーリセット
            timer = 0.0f;

        }

    }
    /// <summary>
    /// 生成位置決定
    /// </summary>
    /// <returns></returns>
    private Vector3 DecideInstancePosition() {
        Vector3 decidePos = InstancePos;
        decidePos.x = Random.Range(-INSTANCERANGE, INSTANCERANGE);
        decidePos.y = 5;
        decidePos.z = Random.Range(-INSTANCERANGE, INSTANCERANGE);
        InstancePos = decidePos;
        return InstancePos;
    }
    /// <summary>
    /// オブジェクト使用
    /// </summary>
    /// <param name="_category"></param>
    public void UseObject(int _category) {
        //リストが空かどうか確認
        if (IsEmpty(unuseObjectList[_category])) return;
        BaseItem useItem = unuseObjectList[_category][0];
        unuseObjectList[_category].RemoveAt(0);
        //親オブジェクト変更
        useItem.transform.SetParent(UseRoot);
        //使うアイテムのセットアップ(座標決め)
        useItem.Setup(DecideInstancePosition());
        useObjectList.Add(useItem);
    }
    /// <summary>
    /// オブジェクト未使用状態に変更
    /// </summary>
    /// <param name="_base"></param>
    /// <param name="_category"></param>
    public void UnuseObject(BaseItem _base,int _category) {
        if (_base == null) return;
        //使用中リストから削除
        useObjectList.Remove(_base);
        unuseObjectList[_category].Add(_base);
        //親オブジェクト設定
        _base.transform.SetParent(UnuseRoot);
    }
}
