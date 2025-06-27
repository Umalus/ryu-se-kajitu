using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

/// <summary>
/// ���_�������Ă���N���X
/// </summary>
public class Insect : BaseScoreObject
{
    [SerializeField]
    private BaseScoreData scoreData;
    private void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        //�~�点��
        FallInsect();
        //�j�����鏈��
        DeleteObject((int)SEIndex.InsectSound);
    }
    /// <summary>
    /// �������֐�
    /// </summary>
    private void Initialize() {
        SetScore(scoreData.score);
    }
    /// <summary>
    /// ���𗎉�������
    /// </summary>
    private void FallInsect() {
        Vector3 fallPos = transform.position;
        fallPos.y -= scoreData.fallSpeed;
        transform.position = fallPos;
    }
}
