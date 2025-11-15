using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using static GameEnum;

public class Player : MonoBehaviour {

    //プレイヤーの速度
    [SerializeField]
    private float playerVelocity = 5.0f;
    //得点加算用コンボ
    [SerializeField]
    private static int combo = 0;
    [SerializeField]
    private float brinkPower = 2.5f;
    [SerializeField]
    private float speed = 5.0f;

    private const int BLINK_SE_ID = 7;
    //プレイヤーの方向
    private Vector3 playerDir;
    //進行方向
    private Vector3 moveDir;
    //InputSystem
    private Bozu inputActions = null;
    //アニメーション変更用アニメーター
    private Animator anim = null;
    //物理挙動
    private Rigidbody rb = null;
    //ブリンクできるかどうか
    private bool canBrink = true;
    private List<GameObject> abirrities = null;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Fruit")) {
            BaseScoreObject addScoreObj = other.gameObject.GetComponent<BaseScoreObject>();
            combo++;
            if (combo > 0 && combo % 5 == 0)
                GameManager.instance.AddSecond(3.0f);
            addScoreObj.SetIsGet(true);
            ScoreManager.AddScore(addScoreObj, combo);

            EffectManager.instance.ExecuteEffect(addScoreObj.effectID, other.transform);
        }
        else if (other.gameObject.CompareTag("Insect")) {
            BaseScoreObject addScoreObj = other.gameObject.GetComponent<BaseScoreObject>();
            combo = 0;
            addScoreObj.SetIsGet(true);
            ScoreManager.AddScore(addScoreObj, combo);

            EffectManager.instance.ExecuteEffect(addScoreObj.effectID, other.transform);
        }
    }
    void Start() {
        anim = GetComponent<Animator>();
        inputActions = InputSystemManager.instance.InputSystem;
        rb = GetComponent<Rigidbody>();

        inputActions.Player.Move.performed += OnMovePreformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        inputActions.Player.Brink.started += OnBrinkStarted;
        inputActions.Enable();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (!GameManager.instance.IsPlay) return;
        if (playerDir.sqrMagnitude >= Mathf.Epsilon) {
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
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));
        //カメラの方向と入力から進む方向を決定
        moveDir = cameraForward * playerDir.z + Camera.main.transform.right * playerDir.x;
        //進行方向に向かせる
        transform.LookAt(transform.position + moveDir);
        //実際に移動させる
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// プレイヤーインプット用移動関数
    /// </summary>
    /// <param name="_context"></param>
    private void OnMovePreformed(InputAction.CallbackContext _context) {
        //コールバックの入力ベクトルを取得
        Vector2 inputDir = _context.ReadValue<Vector2>();
        //進行方向に変換
        playerDir = new Vector3(inputDir.x, 0, inputDir.y);

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

    private void OnBrinkStarted(InputAction.CallbackContext _context) {
        if (!canBrink) return;

        canBrink = false;
        rb.AddForce(playerVelocity * moveDir * brinkPower,ForceMode.Impulse) ;
        AudioManager.instance.PlaySE(BLINK_SE_ID);
        Invoke(nameof(ResetVelocity),0.5f);
        Invoke(nameof(ResetCanBrink), 3.0f);
    }
    public static void SetCombo(int _combo) {
        combo = _combo;
    }
    public void Reset() {
        transform.position = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        playerDir = Vector3.zero;
    }

    private void ResetVelocity() {
        rb.velocity = Vector3.zero;
    }

    private void ResetCanBrink() {
        canBrink = true;
    }
}
