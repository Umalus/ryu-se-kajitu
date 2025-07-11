using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///�@�X�R�A�I�u�W�F�N�g�̎����̊��N���X
/// </summary>
public abstract class BaseScoreObject : MonoBehaviour
{
    public int score { get; private set; } = 0;

    public bool isGet { get; protected set; }
    
    public void SetScore(int _value) { score = _value; }

    /// <summary>
    /// �I�u�W�F�N�g��j���������̏���
    /// </summary>
    /// <param name="_seIndex"></param>
    public void DeleteObject(int _category, int _seIndex) {
        if(transform.position.y < 0 || isGet || !GameManager.instance.IsPlay) {
            //SE�Đ�
            if (isGet)
                AudioManager.instance.PlaySE(_seIndex);
            FruitManager.instance.UnuseObject(this, _category);
        }
    }

    public void SetIsGet(bool _value) {
        isGet = _value;
    }
}
