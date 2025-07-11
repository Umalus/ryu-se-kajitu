using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static CommonModul;

/// <summary>
/// �t���[�c���~�点��}�l�[�W���[
/// </summary>
public class FruitManager : MonoBehaviour {
    public static FruitManager instance = null;


    [SerializeField, Header("���������t���[�c�̃��X�g")]
    private List<BaseScoreObject> originPrefabs = null;
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
    //�g�p�����X�g
    private List<BaseScoreObject> useObjectList = null;
    //���g�p���X�g
    private List<List<BaseScoreObject>> unuseObjectList = null;
    //�v�[�����O�p�e�I�u�W�F�N�g
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
        //�C���X�^���X�Ɏ��g��ݒ�
        instance = this;
        //���X�g��������
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
                UseObject((int)FallObjectType.Fruit, InstancePos);
                instanceTimer = 0.0f;

            }
            //�t���[�c�ƒ���������
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
        //���g�p���X�g����Ȃ珈�����Ȃ�
        if (IsEmpty(unuseObjectList)) return;
        //�g�p����I�u�W�F�N�g���L���b�V��
        BaseScoreObject useObj = unuseObjectList[_category][0];
        //���g�p���X�g�����菜��
        unuseObjectList[_category].RemoveAt(0);
        //�g�p���̐e�I�u�W�F�N�g�ɐݒ�
        useObj.transform.SetParent(useRoot);
        useObj.transform.position = _instancePos;

        useObjectList.Add(useObj);
    }

    public void UnuseObject(BaseScoreObject _obj,int _category) {
        if (_obj == null) return;
        //�g�p�����X�g����폜
        useObjectList.Remove(_obj);
        unuseObjectList[_category].Add(_obj);
        //�e�I�u�W�F�N�g�ݒ�
        _obj.transform.SetParent(unuseRoot);

    }
}
