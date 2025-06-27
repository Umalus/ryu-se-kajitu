using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���̊��N���X
/// </summary>
public abstract class BaseItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player"))
            AddEffect();

        Debug.Log("!!!");
    }
    /// <summary>
    /// �^����e���֐��̒��ۃ��\�b�h
    /// </summary>
    public abstract void AddEffect();
    /// <summary>
    /// ���ʂ�����
    /// </summary>
    public abstract void DeleteEffect();
}
