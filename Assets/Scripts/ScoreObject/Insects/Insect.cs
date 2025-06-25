using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //y���W��0�ȉ��Ȃ����
        if (transform.position.y <= 0) {
            Destroy(gameObject);
        }

        FallInsect();
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
