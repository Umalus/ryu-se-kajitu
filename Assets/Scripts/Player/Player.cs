using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    //InputSystem
    private Bozu inputActions = null;
    //�v���C���[�̑��x
    [SerializeField]
    private float playerVelocity = 5.0f;
    //���_���Z�p�R���{
    [SerializeField]
    private static int combo = 0;
    //�v���C���[�̕���
    private Vector3 playerDir;
    //�A�j���[�V�����ύX�p�A�j���[�^�[
    private Animator anim = null;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Fruit")) {
            BaseScoreObject addScoreObj = other.gameObject.GetComponent<BaseScoreObject>();
            combo++;
            if (combo > 0 && combo % 5 == 0)
                GameManager.instance.AddSecond(3.0f);
            addScoreObj.SetIsGet(true);
            ScoreManager.AddScore(addScoreObj,combo);
        }
        else if (other.gameObject.CompareTag("Insect")) {
            BaseScoreObject addScoreObj = other.gameObject.GetComponent<BaseScoreObject>();
            combo = 0;
            addScoreObj.SetIsGet(true);
            ScoreManager.AddScore(addScoreObj, combo);
        }
    }
    void Start() {
        anim = GetComponent<Animator>();
        inputActions = InputSystemManager.instance.InputSystem;

        inputActions.Player.Move.performed += OnMovePreformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        inputActions.Enable();
    }

    // Update is called once per frame
    private void Update() {
        if (!GameManager.instance.IsPlay) return;
        if(playerDir.sqrMagnitude >= Mathf.Epsilon) {
            Move();
        }

    }

    /// <summary>
    /// �R���{�l�擾
    /// </summary>
    /// <returns></returns>
    public static int GetCombo() {
        return combo;
    }
    private void Move() {
        //�J�����̕�������XZ���ʂ��擾
        var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));
        //�J�����̕����Ɠ��͂���i�ޕ���������
        var moveDirection = cameraForward * playerDir.z + Camera.main.transform.right * playerDir.x;
        //�i�s�����Ɍ�������
        transform.LookAt(transform.position + moveDirection);
        //���ۂɈړ�������
        transform.position += playerVelocity * Time.deltaTime * moveDirection;
        //SE�Đ�
        //AudioManager.instance.PlaySE(0,0.5f,true);
    }

    /// <summary>
    /// �v���C���[�C���v�b�g�p�ړ��֐�
    /// </summary>
    /// <param name="_context"></param>
    private void OnMovePreformed(InputAction.CallbackContext _context) {
        //�R�[���o�b�N�̓��̓x�N�g�����擾
        Vector2 inputDir = _context.ReadValue<Vector2>();
        //�i�s�����ɕϊ�
        playerDir = new Vector3(inputDir.x,0,inputDir.y);

        //�A�j���[�V�����ύX
        anim.SetBool("IsMove", true);
        
    }
    /// <summary>
    /// �v���C���[�C���v�b�g�p�ړ��֐�
    /// </summary>
    /// <param name="_context"></param>
    private void OnMoveCanceled(InputAction.CallbackContext _context) {
        //�i�s�������Z�b�g
        playerDir = Vector3.zero;
        //�A�j���[�V�����ύX
        anim.SetBool("IsMove", false);
    }
    
}
