using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static ItemUtility;

/// <summary>
/// �A�C�e���̊��N���X
/// </summary>
public abstract class BaseItem : MonoBehaviour
{
    [SerializeField]
    protected float fallSpeed = 0.0f;
    float angle = 0.0f;
    public int ID { get; private set; } = -1;
    [SerializeField]
    private LayerMask downStopLayer;

    protected int categoryID = -1;
    protected async void OnCollisionEnter(Collision collision){
        //���������I�u�W�F�N�g�̃^�O���v���C���[�Ȃ�
        if (collision.gameObject.CompareTag("Player")) {
            //���ʓK�p
            AddEffect();
            AudioManager.instance.PlaySE((int)SEIndex.ItemSound, 0.5f);
            //���g�p��Ԃɂ���
            await UnuseObject(this, categoryID);
            
        }
            
    }
    public void Setup(Vector3 instancePos) {
        transform.position = instancePos;
    }

    protected async void Update() {
        UniTask task;
        if (!GameManager.instance.IsPlay)
            task = UnuseObject(this, categoryID);
        FallItem();
        RotatoItem();
        await UniTask.CompletedTask;
    }

    /// <summary>
    /// �^����e���֐��̒��ۃ��\�b�h
    /// </summary>
    public abstract void AddEffect();
    /// <summary>
    /// ���ʂ�����
    /// </summary>
    public abstract UniTask DeleteEffect();
    public void FallItem() {
        //���݂̍��W���L���b�V������
        Vector3 fallPosition = transform.position;
        //���C��������܂ł��Ƃ�������
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y * 2, downStopLayer)) {
            //fallPosition.y = hit.point.y;
            transform.position = fallPosition;
            return;
        }
        fallPosition.y -= fallSpeed;
        transform.position = fallPosition;
        //�f�o�b�O�p
        Debug.DrawRay(transform.position, Vector3.down * transform.localScale.y * 2,Color.cyan);
        
    }
    public void RotatoItem() {
        angle += 0.5f;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.up);
    }

    public void SetID(int _ID) {
        ID = _ID;
    }
}
