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
    //�I�u�W�F�N�g�v�[�����O�p�e�I�u�W�F�N�g��Transform
    [SerializeField]
    private Transform UseRoot = null;
    [SerializeField]
    private Transform UnuseRoot = null;
    //�v�[�����O�p�ϐ�
    //�g�p��ԃ��X�g
    private List<GameObject> useObjectList = null;
    //���g�p��ԃ��X�g
    private List<List<GameObject>> unuseObjectList = null;
    private readonly int MAX_OBJECT = 16;
    private void Start() {
        Initialized();
    }

    private void Initialized() {
        //�C���X�^���X�����g�ɐݒ�
        instance = this;
        //���X�g��������
        useObjectList = new List<GameObject>(MAX_OBJECT);
        unuseObjectList = new List<List<GameObject>>((int)eEffectCategory.effectCategoryMax);
        //����͍ő吔���e�A�C�e���̎�ނŊ���������������
        int objectCountPerCategory = MAX_OBJECT / (int)eEffectCategory.effectCategoryMax;

        //���g�p��Ԃŕ���
        for (int i = 0, max = (int)eEffectCategory.effectCategoryMax ; i < max; i++) {
            unuseObjectList.Add(new List<GameObject>(objectCountPerCategory));
            for (int itemCount = 0; itemCount < objectCountPerCategory; itemCount++) {
                //���g�p��Ԃɂ��ă��X�g�ɒǉ�
                unuseObjectList[i].Add(Instantiate(effectOrigin[i], UnuseRoot));
            }
        }

    }
    public void ExecuteEffect(int _category,Transform _instatncePos) {
        UseObject(_category,_instatncePos);
    }

    

    /// <summary>
    /// �I�u�W�F�N�g�g�p
    /// </summary>
    /// <param name="_category"></param>
    public void UseObject(int _category, Transform _instatncePos) {
        GameObject useEffect = null;
        //���X�g���󂩂ǂ����m�F
        if (IsEmpty(unuseObjectList[_category])) {
            useEffect = Instantiate(effectOrigin[_category], UseRoot);
        }
        else {
            useEffect = unuseObjectList[_category][0];
            unuseObjectList[_category].RemoveAt(0);
            //�e�I�u�W�F�N�g�ύX
            useEffect.transform.SetParent(UseRoot);
        }
        
        //�g���A�C�e���̃Z�b�g�A�b�v(���W����)
        useEffect.transform.position = _instatncePos.position;
        useObjectList.Add(useEffect);
    }
    /// <summary>
    /// �I�u�W�F�N�g���g�p��ԂɕύX
    /// </summary>
    /// <param name="_base"></param>
    /// <param name="_category"></param>
    public async UniTask UnuseObject(GameObject _base, int _category) {
        if (_base == null) return;
        //�g�p�����X�g����폜
        useObjectList.Remove(_base);
        unuseObjectList[_category].Add(_base);
        //�e�I�u�W�F�N�g�ݒ�
        _base.transform.SetParent(UnuseRoot);
        await UniTask.CompletedTask;
    }
}
