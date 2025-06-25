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
    private Vector3 cameraPos;
    private Vector3 startClickPos = Vector3.zero;
    private Vector3 currentClickPos = Vector3.zero;
    private float distanceX = 0.0f;
    private float distanceY = 0.0f;
    [SerializeField]
    private float rotateSpeed = 1.0f;


    [SerializeField]
    private bool UseMouse = true;
    private void Update() {
        //カメラを回転
        param.angles = RotateCamera();
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
        //カメラを回転
        //param.angles = RotateCamera();


        // パラメータを各種オブジェクトに反映
        parent.position = param.position;
        parent.eulerAngles = param.angles;

        var childPos = child.localPosition;
        childPos.z = -param.distance;
        child.localPosition = childPos;

        mainCamera.transform.localPosition = param.offset;
    }

    /// <summary>
    /// カメラ回転
    /// </summary>
    /// <returns></returns>
    private Vector3 RotateCamera() {



        //スマホ用タッチ検出
        if (UseMouse) {
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
                //触った地点と現在の地点の距離を計算
                distanceX = currentClickPos.x - startClickPos.x;
                distanceY = currentClickPos.y - startClickPos.y;
                Debug.Log($"X{distanceX}");
                Debug.Log($"Y{distanceY}");
            }
            ////離されたとき
            //if (leftClick.wasReleasedThisFrame) {
            //    distanceX = distanceY = 0.0f;
            //}
            #endregion
        }


        //PC用クリック検出
        else {

            #region TouchScreen
            var touch = Touchscreen.current;

            Vector2 touchPos = touch.position.ReadValue();
            var press = touch.press;
            //クリック時
            if (press.wasPressedThisFrame) {
                startClickPos = touchPos;

                Debug.Log(startClickPos.x);
                Debug.Log(startClickPos.y);

            }

            //押している間
            if (press.isPressed) {
                currentClickPos = touchPos;
                distanceX = currentClickPos.x - startClickPos.x;
                distanceY = currentClickPos.y - startClickPos.y;
                //Debug.Log($"X{distanceX}");
                //Debug.Log($"Y{distanceY}");

            }
            //離した時
            if (press.wasReleasedThisFrame) {
                cameraPos.x = distanceX;
                cameraPos.y = distanceY;
            }


            #endregion
        }



        //もし指定範囲内(おおよそスティックの位置)なら現在の角度の値を返す
        if ((startClickPos.x < 900 && startClickPos.y < 450))
            return param.angles;
        cameraPos = param.angles;

        cameraPos.x = -distanceY * rotateSpeed;
        cameraPos.y = distanceX * rotateSpeed;
        //もしカメラのX軸回転が0以下なら0にする
        if (cameraPos.x <= 0)
            cameraPos.x = 0.0f;
        //もしカメラのY軸回転が85以上なら85にする
        if (cameraPos.x >= 85)
            cameraPos.x = 85.0f;
        return cameraPos;

    }


}
