using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class CameraManager : MonoBehaviour {
    public static CameraManager instance = null;
    [Serializable]
    public class Paramater {
        [SerializeField, Header("�ǂ�������Ώ�")]
        public GameObject targetObj = null;
        public Vector3 position;
        public Vector3 angles = new Vector3(10.0f, 0.0f, 0.0f);
        public float distance = 0.0f;
        public Vector3 offset;
    }

    [SerializeField, Header("���C���̃J����")]
    private Camera mainCamera = null;
    [SerializeField]
    private float lerpTime = 4.0f;
    [SerializeField, Header("�e��p�����[�^�[")]
    private Paramater param;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Transform child;
    #region �J������]
    //�J������]�p�����o�ϐ�
    private Vector3 cameraPos;
    private Vector3 startClickPos = Vector3.zero;
    private Vector3 currentClickPos = Vector3.zero;
    private float distanceX = 0.0f;
    private float distanceY = 0.0f;
    [SerializeField]
    private float rotateSpeed = 1.0f;
    private bool isFirstTouch = false;
    private Vector2 currentPos = Vector2.zero;
    #endregion
    //�}�E�X���X�}�z���ǂ���(�f�o�b�O�p)
    [SerializeField]
    private bool UseMouse = false;

    private void Awake() {
        instance = this;
    }
    private void Update() {
        //�J��������]
        param.angles = RotateCamera();
    }

    private void LateUpdate() {
        //�����ꂩ�̗v�f�������Ă��Ȃ�������
        if (parent == null || child == null || mainCamera == null) {
            return;
        }
        //���R�ɒǐ�
        if (param.targetObj != null) {
            param.position = Vector3.Lerp(
            a: param.position,
            b: param.targetObj.transform.position,
            t: Time.deltaTime * lerpTime);
        }
        //�J��������]
        //param.angles = RotateCamera();


        // �p�����[�^���e��I�u�W�F�N�g�ɔ��f
        parent.position = param.position;
        parent.eulerAngles = param.angles;

        var childPos = child.localPosition;
        childPos.z = -param.distance;
        child.localPosition = childPos;

        mainCamera.transform.localPosition = param.offset;
    }

    /// <summary>
    /// �J������]
    /// </summary>
    /// <returns></returns>
    private Vector3 RotateCamera() {
        //PC�p�N���b�N���o
        if (UseMouse) {
            #region Mouse

            var click = Mouse.current;

            var clickPos = click.position.ReadValue();
            var leftClick = click.leftButton;

            //�N���b�N��
            if (leftClick.wasPressedThisFrame) {

                startClickPos = clickPos;

            }
            //�����Ă����
            if (leftClick.isPressed) {
                currentClickPos = clickPos;
                //�G�����n�_�ƌ��݂̒n�_�̋������v�Z
                distanceX = currentClickPos.x - startClickPos.x;
                distanceY = currentClickPos.y - startClickPos.y;
                currentPos = new Vector2(distanceX, distanceY);
                Debug.Log($"X{distanceX}");
                Debug.Log($"Y{distanceY}");
            }
            ////�����ꂽ�Ƃ�
            //if (leftClick.wasReleasedThisFrame) {
            //    distanceX = distanceY = 0.0f;
            //}
            #endregion
        }



        //�X�}�z�p�^�b�`���o
        else {
            var touchs = Input.touchCount;

            if (touchs >= 1) {

                #region TouchScreen
                var touch1 = Input.touches[0];




                Vector2 touchPos1 = touch1.position;
                if (touchPos1 == null) return Vector3.zero;
                var press1 = touch1.phase;
                //�^�b�v��
                if (press1 == UnityEngine.TouchPhase.Began) {

                    startClickPos = touchPos1;
                    Debug.Log(startClickPos.x);
                    Debug.Log(startClickPos.y);

                }
                //�ŏ��̃^�b�v�ʒu���w����W(��ʍ�����)�Ȃ��ڂ̃^�b�v�ʒu���擾���A�^�b�v�J�n�ʒu�֑��
                if (startClickPos.x < 400.0f && startClickPos.y < 500.0f) {
                    return Vector3.zero;
                }



                //�����Ă����
                if (press1 == UnityEngine.TouchPhase.Moved) {
                    currentClickPos = touchPos1;
                    distanceX = currentClickPos.x - startClickPos.x;
                    distanceY = currentClickPos.y - startClickPos.y;

                    Debug.Log(distanceX);
                    Debug.Log(distanceY);
                }
                //��������
                if (press1 == UnityEngine.TouchPhase.Canceled) {
                    cameraPos.x = distanceX;
                    cameraPos.y = distanceY;

                    //startClickPos = touch1.position;
                }


                #endregion
            }

        }

        //�����w��͈͓�(�����悻�X�e�B�b�N�̈ʒu)�Ȃ猻�݂̊p�x�̒l��Ԃ�
        if ((startClickPos.x < 400 && startClickPos.y < 250))
            return param.angles;


        cameraPos = param.angles;
        //�Z�o��������|�W�V������ύX
        cameraPos.x = -distanceY * rotateSpeed;
        cameraPos.y = distanceX * rotateSpeed;
        //�����J������X����]��0�ȉ��Ȃ�0�ɂ���
        if (cameraPos.x <= 0)
            cameraPos.x = 0.0f;
        //�����J������Y����]��85�ȏ�Ȃ�85�ɂ���
        if (cameraPos.x >= 85)
            cameraPos.x = 85.0f;
        return cameraPos;

    }

    public void ResetCamera() {
        param.position = Vector3.zero;
        param.angles = Vector3.zero;
    }

}
