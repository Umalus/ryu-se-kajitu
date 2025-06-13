using System;
using UnityEngine;

public class CameraManager : MonoBehaviour {
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

    private void LateUpdate() {
        if (parent == null || child == null || mainCamera == null) {
            return;
        }

        if (param.targetObj != null) {
            param.position = Vector3.Lerp(
            a: param.position,
            b: param.targetObj.transform.position,
            t: Time.deltaTime * lerpTime);
        }

        // �p�����[�^���e��I�u�W�F�N�g�ɔ��f
        parent.position = param.position;
        parent.eulerAngles = param.angles;

        var childPos = child.localPosition;
        childPos.z = -param.distance;
        child.localPosition = childPos;

        mainCamera.transform.localPosition = param.offset;
    }
}
