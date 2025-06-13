using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// フルーツを降らせるマネージャー
/// </summary>
public class FruitManager : MonoBehaviour {
    [SerializeField, Header("生成されるフルーツのリスト")]
    private List<GameObject> originPrefabs = null;
    [SerializeField]
    private Vector3 InstancePos = Vector3.zero;
    [SerializeField]
    private int instanceValue = -1;
    [SerializeField]
    private float instanceTime = 0.0f;
    public bool OnlyFruit = false;
    // Start is called before the first frame update
    void Start() {

    }

    private void Update() {
        instanceTime += Time.deltaTime;
        InstancePos = DecideInstancePosition();
        instanceValue = Random.Range(0, 11);

        if (instanceValue < 3 && instanceTime >= 1.0f) {
            Instantiate(originPrefabs[0], InstancePos, Quaternion.identity);
            instanceTime = 0.0f;
        }
    }

    private Vector3 DecideInstancePosition() {
        Vector3 decidePos = InstancePos;
        decidePos.x = Random.Range(-10.0f, 10.0f);
        decidePos.z = Random.Range(-10.0f, 10.0f);
        return decidePos;
    }
}
