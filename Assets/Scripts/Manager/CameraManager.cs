using System;
using UnityEngine;
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
    private Bozu inputAction = null;

    private Vector3 startClickPos = Vector3.zero;
    private Vector3 currentClickPos = Vector3.zero;

    void Start() {
        inputAction = InputSystemManager.instance.InputSystem;

        
    }
    private void LateUpdate() {
        if (parent == null || child == null || mainCamera == null) {
            return;
        }
        RotateCamera();

        //自然に追跡
        if (param.targetObj != null) {
            param.position = Vector3.Lerp(
            a: param.position,
            b: param.targetObj.transform.position,
            t: Time.deltaTime * lerpTime);
        }

        parent.eulerAngles += RotateCamera();

        // パラメータを各種オブジェクトに反映
        parent.position = param.position;
        parent.eulerAngles = param.angles;

        var childPos = child.localPosition;
        childPos.z = -param.distance;
        child.localPosition = childPos;

        mainCamera.transform.localPosition = param.offset;
    }
    private Vector3 RotateCamera() {
        Vector3 resultRotation;
        float distanceX;
        float distanceY;

        #region TouchScreen
        #endregion

        #region Mouse

        var click = Mouse.current;

        var clickPos = click.position.ReadValue();
        var leftClick = click.leftButton;

        if (leftClick.wasPressedThisFrame) {
            startClickPos = clickPos;
        }

        if (leftClick.isPressed) {
            currentClickPos = clickPos;
            distanceX = currentClickPos.x - startClickPos.x;
            distanceY = currentClickPos.y - startClickPos.y;
            Debug.Log($"X{distanceX}");
            Debug.Log($"Y{distanceY}");
        }


        #endregion

        resultRotation = Vector3.zero;


        return resultRotation;
        //// マウスの右クリックを押している間
        //if (_context.phase == InputActionPhase.Performed) {
        //    // マウスの移動量
        //    float mouseInputX = _context.ReadValue<Vector2>().x;
        //    float mouseInputY = _context.ReadValue<Vector2>().y;
        //    // targetの位置のY軸を中心に、回転（公転）する
        //    transform.RotateAround(param.targetObj.transform.position, Vector3.up, mouseInputX * Time.deltaTime * 200f);
        //    // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
        //    transform.RotateAround(param.targetObj.transform.position, transform.right, mouseInputY * Time.deltaTime * 200f);
        //}


        //Debug.Log(inputPos);
    }
}
