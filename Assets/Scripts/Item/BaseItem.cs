using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

/// <summary>
/// �A�C�e���̊��N���X
/// </summary>
public abstract class BaseItem : MonoBehaviour
{
    [SerializeField]
    protected float fallSpeed = 0.0f;

    protected void OnCollisionEnter(Collision collision) {
        //���������I�u�W�F�N�g�̃^�O���v���C���[�Ȃ�
        if (collision.gameObject.CompareTag("Player")) {
            //���ʓK�p
            AddEffect();
            //�������悤�Ɍ�����
            gameObject.SetActive(false);
            AudioManager.instance.PlaySE((int)SEIndex.ItemSound,0.5f);
        }
            
    }
    protected void Update() {
        FallItem();
        

    }

    /// <summary>
    /// �^����e���֐��̒��ۃ��\�b�h
    /// </summary>
    public abstract void AddEffect();
    /// <summary>
    /// ���ʂ�����
    /// </summary>
    public abstract void DeleteEffect();
    public void FallItem() {
        Vector3 fallPosition = transform.position;
        fallPosition.y -= fallSpeed;
        
        if (fallPosition.y <= 0.5f)
            fallPosition.y = 0.5f;
        transform.position = fallPosition;
    }
}
