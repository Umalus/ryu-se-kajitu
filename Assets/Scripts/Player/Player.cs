using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    //InputSystem
    private Bozu inputActions = null;
    //プレイヤーの速度
    [SerializeField]
    private float playerVelocity = 5.0f;
    //得点加算用コンボ
    [SerializeField]
    private static int combo = 0;
    //プレイヤーの方向
    private Vector3 playerDir;
    //アニメーション変更用アニメーター
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
    /// コンボ値取得
    /// </summary>
    /// <returns></returns>
    public static int GetCombo() {
        return combo;
    }
    private void Move() {
        //カメラの方向からXZ平面を取得
        var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));
        //カメラの方向と入力から進む方向を決定
        var moveDirection = cameraForward * playerDir.z + Camera.main.transform.right * playerDir.x;
        //進行方向に向かせる
        transform.LookAt(transform.position + moveDirection);
        //実際に移動させる
        transform.position += playerVelocity * Time.deltaTime * moveDirection;
        //SE再生
        //AudioManager.instance.PlaySE(0,0.5f,true);
    }

    /// <summary>
    /// プレイヤーインプット用移動関数
    /// </summary>
    /// <param name="_context"></param>
    private void OnMovePreformed(InputAction.CallbackContext _context) {
        //コールバックの入力ベクトルを取得
        Vector2 inputDir = _context.ReadValue<Vector2>();
        //進行方向に変換
        playerDir = new Vector3(inputDir.x,0,inputDir.y);

        //アニメーション変更
        anim.SetBool("IsMove", true);
        
    }
    /// <summary>
    /// プレイヤーインプット用移動関数
    /// </summary>
    /// <param name="_context"></param>
    private void OnMoveCanceled(InputAction.CallbackContext _context) {
        //進行方向リセット
        playerDir = Vector3.zero;
        //アニメーション変更
        anim.SetBool("IsMove", false);
    }
    
}
