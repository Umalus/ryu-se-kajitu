using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
/// <summary>
/// �t���[�c���~�点��}�l�[�W���[
/// </summary>
public class FruitManager : MonoBehaviour {
    public static FruitManager instance = null;


    [SerializeField, Header("���������t���[�c�̃��X�g")]
    private List<GameObject> originPrefabs = null;
    [SerializeField, Header("�����͈�")]
    private float InstanceRange = 0;
    //�����ʒu
    [SerializeField]
    private Vector3 InstancePos = Vector3.zero;
    //�������邽�߂̕ϐ�
    [SerializeField]
    private int instanceValue = -1;
    //��������܂ł̎���
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
        //�Q�[���v���C��ԂłȂ���Ώ������Ȃ�
        if (!GameManager.instance.IsPlay) return;

        //�t���[�c�̐����m���p�ϐ�( 10 - ���̒l �����̐����m��)
        int fruitRatio = 0;
        //�����Ԋu
        float interval = 0.0f;
        //�������Ԃ̃^�C�}�[�𑝉�
        instanceTimer += Time.deltaTime;
        //�����ʒu�̌���
        InstancePos = DecideInstancePosition();
        //�����l�������_���Ō���
        instanceValue = Random.Range(0, 11);
        switch (GameManager.instance.phase) {
            //�t�F�[�Y�ɂ���Đ����Ԋu��m����ύX
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
        //�����ʒu����p�ꎞ�ϐ�
        Vector3 decidePos = InstancePos;
        decidePos.x = Random.Range(-InstanceRange, InstanceRange);
        decidePos.z = Random.Range(-InstanceRange, InstanceRange);
        return decidePos;
    }

    private void InstanceObject(float _interval, int _fruitRatio) {
        //�s���l������Ə������Ȃ�
        if (_interval < 0 || _fruitRatio < 0) return;

        if (instanceTimer >= _interval) {
            if (OnlyFruit) {
                //�t���[�c�̂ݐ���
                Instantiate(originPrefabs[(int)FallObjectType.Fruit], InstancePos, Quaternion.identity);
                instanceTimer = 0.0f;

            }
            //�t���[�c�ƒ���������
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
