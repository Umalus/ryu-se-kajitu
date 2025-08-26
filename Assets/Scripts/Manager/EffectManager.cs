using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using static CommonModul;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;
    [SerializeField]
    private List<GameObject> effectOrigin = null;
    //オブジェクトプーリング用親オブジェクトのTransform
    [SerializeField]
    private Transform UseRoot = null;
    [SerializeField]
    private Transform UnuseRoot = null;
    //プーリング用変数
    //使用状態リスト
    private List<GameObject> useObjectList = null;
    //未使用状態リスト
    private List<List<GameObject>> unuseObjectList = null;
    private readonly int MAX_OBJECT = 16;
    private void Start() {
        Initialized();
    }

    private void Initialized() {
        //インスタンスを自身に設定
        instance = this;
        //リストを初期化
        useObjectList = new List<GameObject>(MAX_OBJECT);
        unuseObjectList = new List<List<GameObject>>((int)eEffectCategory.effectCategoryMax);
        //今回は最大数を各アイテムの種類で割った数生成する
        int objectCountPerCategory = MAX_OBJECT / (int)eEffectCategory.effectCategoryMax;

        //未使用状態で複製
        for (int i = 0, max = (int)eEffectCategory.effectCategoryMax ; i < max; i++) {
            unuseObjectList.Add(new List<GameObject>(objectCountPerCategory));
            for (int itemCount = 0; itemCount < objectCountPerCategory; itemCount++) {
                //未使用状態にしてリストに追加
                unuseObjectList[i].Add(Instantiate(effectOrigin[i], UnuseRoot));
            }
        }

    }
    public void ExecuteEffect(int _category,Transform _instatncePos) {
        UseObject(_category,_instatncePos);
    }

    

    /// <summary>
    /// オブジェクト使用
    /// </summary>
    /// <param name="_category"></param>
    public void UseObject(int _category, Transform _instatncePos) {
        GameObject useEffect = null;
        //リストが空かどうか確認
        if (IsEmpty(unuseObjectList[_category])) {
            useEffect = Instantiate(effectOrigin[_category], UseRoot);
        }
        else {
            useEffect = unuseObjectList[_category][0];
            unuseObjectList[_category].RemoveAt(0);
            //親オブジェクト変更
            useEffect.transform.SetParent(UseRoot);
        }
        
        //使うアイテムのセットアップ(座標決め)
        useEffect.transform.position = _instatncePos.position;
        useObjectList.Add(useEffect);
    }
    /// <summary>
    /// オブジェクト未使用状態に変更
    /// </summary>
    /// <param name="_base"></param>
    /// <param name="_category"></param>
    public async UniTask UnuseObject(GameObject _base, int _category) {
        if (_base == null) return;
        //使用中リストから削除
        useObjectList.Remove(_base);
        unuseObjectList[_category].Add(_base);
        //親オブジェクト設定
        _base.transform.SetParent(UnuseRoot);
        await UniTask.CompletedTask;
    }
}
