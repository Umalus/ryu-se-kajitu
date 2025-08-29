using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class CameraManager : MonoBehaviour {
    public static CameraManager instance = null;
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
    #region カメラ回転
    //カメラ回転用メンバ変数
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
    //マウスかスマホかどうか(デバッグ用)
    [SerializeField]
    private bool UseMouse = false;

    private void Awake() {
        instance = this;
    }
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
        //PC用クリック検出
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
                currentPos = new Vector2(distanceX, distanceY);
                Debug.Log($"X{distanceX}");
                Debug.Log($"Y{distanceY}");
            }
            ////離されたとき
            //if (leftClick.wasReleasedThisFrame) {
            //    distanceX = distanceY = 0.0f;
            //}
            #endregion
        }



        //スマホ用タッチ検出
        else {
            var touchs = Input.touchCount;

            if (touchs >= 1) {

                #region TouchScreen
                var touch1 = Input.touches[0];




                Vector2 touchPos1 = touch1.position;
                if (touchPos1 == null) return Vector3.zero;
                var press1 = touch1.phase;
                //タップ時
                if (press1 == UnityEngine.TouchPhase.Began) {

                    startClickPos = touchPos1;
                    Debug.Log(startClickPos.x);
                    Debug.Log(startClickPos.y);

                }
                //最初のタップ位置が指定座標(画面左半分)なら二つ目のタップ位置を取得し、タップ開始位置へ代入
                if (startClickPos.x < 400.0f && startClickPos.y < 500.0f) {
                    return Vector3.zero;
                }



                //押している間
                if (press1 == UnityEngine.TouchPhase.Moved) {
                    currentClickPos = touchPos1;
                    distanceX = currentClickPos.x - startClickPos.x;
                    distanceY = currentClickPos.y - startClickPos.y;

                    Debug.Log(distanceX);
                    Debug.Log(distanceY);
                }
                //離した時
                if (press1 == UnityEngine.TouchPhase.Canceled) {
                    cameraPos.x = distanceX;
                    cameraPos.y = distanceY;

                    //startClickPos = touch1.position;
                }


                #endregion
            }

        }

        //もし指定範囲内(おおよそスティックの位置)なら現在の角度の値を返す
        if ((startClickPos.x < 400 && startClickPos.y < 250))
            return param.angles;


        cameraPos = param.angles;
        //算出距離からポジションを変更
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

    public void ResetCamera() {
        param.position = Vector3.zero;
        param.angles = Vector3.zero;
    }

}
