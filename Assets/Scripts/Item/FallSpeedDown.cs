using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ă���X�s�[�h�𔼕��ɂ���
/// </summary>
public class FallSpeedDown : BaseItem
{
    
    //�X�s�[�h���擾���邽�߂̃f�[�^
    [SerializeField]
    private BaseScoreData scoreData;
    //�ݒ肷�邽�߂̃X�s�[�h
    private float setSpeed = 0.0f;
    //�^�C�}�[�𑖂点�邩�ǂ���
    private static bool IsRunningTime = false;
    //�^�C�}�[
    [SerializeField]
    float timer = 0.0f;


    private new void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
        IsRunningTime = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        setSpeed = scoreData.fallSpeed * 0.5f;
    }
    private new void Update() {
        base.Update();
        if (IsRunningTime)
            timer += Time.deltaTime;

        if (timer >= 10.0f) {
            //�t�^���ꂽ���ʂ�����
            DeleteEffect();
            //�I�u�W�F�N�g������
            Destroy(gameObject);
            //�^�C�}�[���~�߂�
            IsRunningTime = false;
            //�^�C�}�[���Z�b�g
            timer = 0.0f;
        }
    }

    /// <summary>
    /// �������^�������
    /// </summary>
    public override void AddEffect() {
        Fruit.SetFallSpeed(setSpeed);
        Fruit.IsHalf = true;
    }
    /// <summary>
    /// ���ʂ�����
    /// </summary>
    public override void DeleteEffect() {
        Fruit.IsHalf = false;
    }

    
}
