using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitychanController : MonoBehaviour {
	//  コンポーネントの宣言
	Animator anim;
	Rigidbody rb;

	//  メンバ変数の宣言
	[SerializeField] float speed = 1.0f;
	const float JumpPower = 20.0f;
	bool canMove = true;
	bool canJump = true;
	Vector3 moveDir = Vector3.zero;

#if false
	/* 今までのUnityと同じような使い方 */
	/* 割と使いやすいが入力がデフォルトでは「押しているとき」しか用意されていない */

	//  PlayerInput コンポーネントの宣言
	PlayerInput input;
	//  各アクションの宣言
	InputAction moveAction;
	InputAction jumpAction;

	void Start() {
		//  コンポーネントの取得
		input = GameObject.Find("InputSystem").GetComponent<PlayerInput>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();

		//  アクションの取得
		moveAction = input.actions["Move"];
		jumpAction = input.actions["Jump"];
	}

	void Update() {
		//  移動処理
		moveDir = Vector3.zero;
		if (moveAction.IsPressed() && canMove) {
			Vector2 inputDir = moveAction.ReadValue<Vector2>();
			Debug.Log(inputDir);
			moveDir = new Vector3(inputDir.x, 0.0f, inputDir.y);
			transform.LookAt(transform.position + moveDir);
			if (rb.velocity.magnitude <= speed) {
				rb.AddForce(moveDir, ForceMode.Impulse);
			}
		}
		anim.SetFloat("Speed", moveDir.sqrMagnitude);

		//  ジャンプ処理
		if (jumpAction.IsPressed() && canJump) {
			rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
			canJump = false;
			anim.SetBool("Jump", true);
		}

		//	復帰
		if (transform.position.y < -10.0f) {
			transform.position = new Vector3(0.0f, 3.0f, 0.0f);
			rb.velocity = Vector3.zero;
		}
	}

#else
	/* InputActionAsset の Generate C# Class にチェックを付ける */
	/* 関数ポインタと同じような使い方 */
	/* 慣れるまでちょっと大変 */
	/* 一般的なのはコレ */

	SampleInputAction input;

	void Start() {
		//  コンポーネントの取得
		input = MyInputManager.instance.input; 
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();

		//	ActionMap[xxxx]のAction[xxxx]の[xxxx]の場合にイベント[関数名]を登録する
		//	started 押した瞬間, performed 押してる間, canceled 離した瞬間

		input.Player.Move.performed += OnMovePerformed;
		input.Player.Move.canceled += OnMoveCanceled;
		input.Player.Jump.started += OnJumpStarted;
		input.Enable();
	}

	void Update() {
		//	復帰
		if (transform.position.y < -10.0f) {
			transform.position = new Vector3(0.0f, 3.0f, 0.0f);
			rb.velocity = Vector3.zero;
		}

		//	移動
		if (moveDir.sqrMagnitude >= 0.01f) {
			transform.LookAt(transform.position + moveDir);
			if (rb.velocity.magnitude <= speed) {
				rb.AddForce(moveDir, ForceMode.Impulse);
			}

		}
		anim.SetFloat("Speed", moveDir.sqrMagnitude);

	}


	void OnMovePerformed(InputAction.CallbackContext context) {
		if (canMove) {
			Vector2 inputDir = context.ReadValue<Vector2>();
			moveDir = new Vector3(inputDir.x, 0.0f, inputDir.y);
		}
	}

	void OnMoveCanceled(InputAction.CallbackContext context) {
		moveDir = Vector3.zero;
	}

	void OnJumpStarted(InputAction.CallbackContext context) {
		if (canJump) {
			rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
			canJump = false;
			anim.SetBool("Jump", true);
		}
	}

#endif

	#region アニメーションイベント

	void Jump(int n) {
		canMove = false;
		if (n == 1) {
			canMove = true;
			canJump = true;
			anim.SetBool("Jump", false);
		}
	}
	#endregion
}
