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
    private Transform player = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followPos = transform.position;
        followPos.x = player.position.x;
        followPos.z = player.position.z;
        transform.position = followPos;
    }
}
