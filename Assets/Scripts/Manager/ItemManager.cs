using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModul;
/// <summary>
/// アイテム管理クラス
/// </summary>
public class ItemManager : MonoBehaviour {
    public static ItemManager instance = null;

    [SerializeField]
    private List<BaseItem> originItems = null;

    private Vector3 InstancePos = Vector3.zero;
    private float timer = 0.0f;
    private const float INTERVAL = 10.0f;
    private const float INSTANCERANGE = 10.0f;

    [SerializeField]
    private Transform UseRoot = null;
    [SerializeField]
    private Transform UnuseRoot = null;
    private void Start() {
        instance = this;
    }

    private void Update() {
        InstanceItem();
    }

    private void InstanceItem() {
        if (!GameManager.instance.IsPlay) return;

        //タイマーをインクリメント
        timer += Time.deltaTime;
        //生成用変数を乱数によって設定
        int instanceValue = Random.Range(0, 10);
        //タイマーがインターバルを超えたら
        if (timer >= INTERVAL) {

            //
            if (InRange(instanceValue, 0, 5))
                Instantiate(originItems[1], DecideInstancePosition(),Quaternion.identity);
            else
                Instantiate(originItems[0], DecideInstancePosition(), Quaternion.identity);
            // タイマーリセット
            timer = 0.0f;

        }

    }

    private Vector3 DecideInstancePosition() {
        Vector3 decidePos = InstancePos;
        decidePos.x = Random.Range(-INSTANCERANGE, INSTANCERANGE);
        decidePos.y = 5;
        decidePos.z = Random.Range(-INSTANCERANGE, INSTANCERANGE);
        InstancePos = decidePos;
        return InstancePos;
    }
}
