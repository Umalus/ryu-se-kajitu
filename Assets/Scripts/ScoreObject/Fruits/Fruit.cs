using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

public class Fruit : BaseScoreObject {
    [SerializeField]
    private BaseScoreData scoreData = null;
    [SerializeField]
    private static float fallSpeed;
    //���x�������ǂ���
    public static bool IsHalf = false;
    private void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        //�~�点��
        FallFruit();
        //�j�����鏈��
        DeleteObject((int)FallObjectType.Fruit ,(int)SEIndex.FruitSound);
    }
    private void Initialize() {
        //�X�R�A�f�[�^�ɕۑ�����Ă�X�R�A����
        SetScore(scoreData.score);
        //�����X�s�[�h������Ԃ���Ȃ���ΐ������ɗ����鑬�x���Đݒ�
        if (!IsHalf)
            fallSpeed = scoreData.fallSpeed;
    }
    /// <summary>
    /// ��������
    /// </summary>
    private void FallFruit() {
        //�|�W�V�������L���b�V����new�����
        Vector3 fallPos = transform.position;
        fallPos.y -= fallSpeed;
        transform.position = fallPos;
    }
    /// <summary>
    /// �������x�ݒ�
    /// </summary>
    /// <param name="_speed"></param>
    public static void SetFallSpeed(float _speed) {
        fallSpeed = _speed;
    }
}
