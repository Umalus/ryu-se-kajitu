using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour {
    [Serializable]
    public class Paramater {
        [SerializeField, Header("追いかける対象")]
        public GameObject targetObj = null;
        public Vector3 position;
        public Vector3 angles = new Vector3(10.0f, 0.0f, 0.0f);
        public float distance = 0.0f;
        public Vector3 offset;
    }

    [SerializeField, Header("メインのカメラ")]
    private Camera mainCamera = null;
    [SerializeField]
    private float lerpTime = 4.0f;
    [SerializeField, Header("各種パラメーター")]
    private Paramater param;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Transform child;
    //カメラ回転用メンバ変数
    private Vector3 startClickPos = Vector3.zero;
    private Vector3 currentClickPos = Vector3.zero;
    private float distanceX = 0.0f;
    private float distanceY = 0.0f;
    [SerializeField]
    private float rotateSpeed = 1.0f;

    private void Update() {
        //カメラを回転
        param.angles += RotateCamera();
    }

    private void LateUpdate() {
        //いずれかの要素が入っていなかったら
        if (parent == null || child == null || mainCamera == null) {
            return;
        }
        //自然に追跡
        if (param.targetObj != null) {
            param.position = Vector3.Lerp(
            a: param.position,
            b: param.targetObj.transform.position,
            t: Time.deltaTime * lerpTime);
        }

        

        // パラメータを各種オブジェクトに反映
        parent.position = param.position;
        parent.eulerAngles = param.angles;

        var childPos = child.localPosition;
        childPos.z = -param.distance;
        child.localPosition = childPos;

        mainCamera.transform.localPosition = param.offset;
    }
    private Vector3 RotateCamera() {
        //最終的に値を返す用のベクトル
        Vector3 resultRotation = Vector3.zero;
        

        #region TouchScreen
        #endregion

        #region Mouse

        var click = Mouse.current;

        var clickPos = click.position.ReadValue();
        var leftClick = click.leftButton;

        //クリック時
        if (leftClick.wasPressedThisFrame) {
            startClickPos = clickPos;
        }
        //押している間
        if (leftClick.isPressed) {
            currentClickPos = clickPos;
            distanceX = currentClickPos.x - startClickPos.x;
            distanceY = currentClickPos.y - startClickPos.y;
            Debug.Log($"X{distanceX}");
            Debug.Log($"Y{distanceY}");
        }
        //離されたとき
        if (leftClick.wasReleasedThisFrame) {
            distanceX = distanceY = 0.0f;
        }
        
        resultRotation.x = -distanceY * rotateSpeed;
        resultRotation.y = distanceX * rotateSpeed;

        return resultRotation;
        #endregion
    }

   
}
