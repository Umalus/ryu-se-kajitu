using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �~�j�}�b�v�Ǘ��N���X
/// �v���C���[�ɒǔ�����
/// </summary>
public class MiniMapCamera : MonoBehaviour
{
    /// <summary>
    /// �I�t�Z�b�g
    /// </summary>
    [SerializeField]
    private Vector3 offset = Vector3.zero;
    /// <summary>
    /// �ǔ�������W
    /// </summary>
    [SerializeField]
    private Transform targetPos = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;
    }

    
    void Update()
    {
        //��x�L���b�V�����đ�����Ȃ������Ƃ�new�����
        Vector3 followPos = transform.position;
        followPos.x = targetPos.position.x;
        followPos.z = targetPos.position.z;
        transform.position = followPos;
    }
}
