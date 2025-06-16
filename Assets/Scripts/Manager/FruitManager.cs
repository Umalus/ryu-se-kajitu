using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �t���[�c���~�点��}�l�[�W���[
/// </summary>
public class FruitManager : MonoBehaviour {
    [SerializeField, Header("���������t���[�c�̃��X�g")]
    private List<GameObject> originPrefabs = null;
    [SerializeField, Header("�����͈�")]
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

    }

    private void Update() {
        instanceTime += Time.deltaTime;
        InstancePos = DecideInstancePosition();
        instanceValue = Random.Range(0, 11);
        switch (GameManager.instance.phase) {
            case GameEnum.GamePhase.opening:
                InstanceObject(2, 3, 7);
                break;
            case GameEnum.GamePhase.middle:
                InstanceObject(1, 4, 5);
                break;
            case GameEnum.GamePhase.ending:
                InstanceObject(0.5f,2,2);
                break;
        }

    }

    private Vector3 DecideInstancePosition() {
        Vector3 decidePos = InstancePos;
        decidePos.x = Random.Range(-InstanceRange, InstanceRange);
        decidePos.z = Random.Range(-InstanceRange, InstanceRange);
        return decidePos;
    }

    private void InstanceObject(float _interval,int _fruitRatio,int _insectRatio) {
        if (instanceTime >= _interval) {
            if (instanceValue <= _fruitRatio) {
                Instantiate(originPrefabs[(int)FallObjectType.Fruit], InstancePos, Quaternion.identity);
                instanceTime = 0.0f;
            }
            else if (instanceValue > _insectRatio) {
                Instantiate(originPrefabs[(int)FallObjectType.Insect], InstancePos, Quaternion.identity);
                instanceTime = 0.0f;
            }

        }
    }
}
