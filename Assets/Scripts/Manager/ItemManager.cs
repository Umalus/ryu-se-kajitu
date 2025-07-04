using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModul;
using static GameEnum;
/// <summary>
/// �A�C�e���Ǘ��N���X
/// </summary>
public class ItemManager : MonoBehaviour {
    //���g�̃C���X�^���X
    public static ItemManager instance = null;
    //��������I�u�W�F�N�g�̃I���W�i��
    [SerializeField]
    private List<BaseItem> originItems = null;
    //�A�C�e���̐����ʒu
    private Vector3 InstancePos = Vector3.zero;
    //�A�C�e�������p�̃^�C�}�[
    private float timer = 0.0f;
    //�A�C�e�������̊Ԋu
    private const float INTERVAL = 10.0f;
    //�A�C�e�������͈�
    private const float INSTANCERANGE = 10.0f;
    //�I�u�W�F�N�g�v�[�����O�p�e�I�u�W�F�N�g��Transform
    [SerializeField]
    private Transform UseRoot = null;
    [SerializeField]
    private Transform UnuseRoot = null;
    //�v�[�����O�p�ϐ�
    //�A�C�e���̍ő吔
    private const int MAX_OBJECT = 4;
    //�g�p��ԃ��X�g
    private List<BaseItem> useObjectList = null;
    //���g�p��ԃ��X�g
    private List<List<BaseItem>> unuseObjectList = null;
    
    

    private void Start() {
        Initialized();


    }

    private void Update() {
        InstanceItem();
    }

    private void Initialized() {
        //�C���X�^���X�����g�ɐݒ�
        instance = this;
        //���X�g��������
        useObjectList = new List<BaseItem>(MAX_OBJECT);
        unuseObjectList = new List<List<BaseItem>>((int)eItemCategory.Max);
        //����͍ő吔���e�A�C�e���̎�ނŊ���������������
        int objectCountPerCategory = MAX_OBJECT / (int)eItemCategory.Max;

        //���g�p��Ԃŕ���
        for (int i = 0,max = (int)eItemCategory.Max; i < max; i++) {
            unuseObjectList.Add(new List<BaseItem>(objectCountPerCategory));
            for(int itemCount = 0; itemCount < objectCountPerCategory; itemCount++) {
                //���g�p��Ԃɂ��ă��X�g�ɒǉ�
                unuseObjectList[i].Add(Instantiate(originItems[i],UnuseRoot));
                unuseObjectList[i][itemCount].SetID(itemCount);
                
            }
        }
        
    }

    private void InstanceItem() {
        if (!GameManager.instance.IsPlay) return;

        //�^�C�}�[���C���N�������g
        timer += Time.deltaTime;
        //�����p�ϐ��𗐐��ɂ���Đݒ�
        int instanceValue = Random.Range(0, 10);
        //�^�C�}�[���C���^�[�o���𒴂�����
        if (timer >= INTERVAL) {

            //�����ɂ���Đ������镨��ύX������
            if (InRange(instanceValue, 0, 5))
                UseObject((int)eItemCategory.FallSpeed);
            else
                UseObject((int)eItemCategory.OnlyFruit);
            // �^�C�}�[���Z�b�g
            timer = 0.0f;

        }

    }
    /// <summary>
    /// �����ʒu����
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
    /// �I�u�W�F�N�g�g�p
    /// </summary>
    /// <param name="_category"></param>
    public void UseObject(int _category) {
        //���X�g���󂩂ǂ����m�F
        if (IsEmpty(unuseObjectList[_category])) return;
        BaseItem useItem = unuseObjectList[_category][0];
        unuseObjectList[_category].RemoveAt(0);
        //�e�I�u�W�F�N�g�ύX
        useItem.transform.SetParent(UseRoot);
        //�g���A�C�e���̃Z�b�g�A�b�v(���W����)
        useItem.Setup(DecideInstancePosition());
        useObjectList.Add(useItem);
    }
    /// <summary>
    /// �I�u�W�F�N�g���g�p��ԂɕύX
    /// </summary>
    /// <param name="_base"></param>
    /// <param name="_category"></param>
    public void UnuseObject(BaseItem _base,int _category) {
        if (_base == null) return;
        //�g�p�����X�g����폜
        useObjectList.Remove(_base);
        unuseObjectList[_category].Add(_base);
        //�e�I�u�W�F�N�g�ݒ�
        _base.transform.SetParent(UnuseRoot);
    }
}
