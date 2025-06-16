using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitychanController : MonoBehaviour {
	//  �R���|�[�l���g�̐錾
	Animator anim;
	Rigidbody rb;

	//  �����o�ϐ��̐錾
	[SerializeField] float speed = 1.0f;
	const float JumpPower = 20.0f;
	bool canMove = true;
	bool canJump = true;
	Vector3 moveDir = Vector3.zero;

#if false
	/* ���܂ł�Unity�Ɠ����悤�Ȏg���� */
	/* ���Ǝg���₷�������͂��f�t�H���g�ł́u�����Ă���Ƃ��v�����p�ӂ���Ă��Ȃ� */

	//  PlayerInput �R���|�[�l���g�̐錾
	PlayerInput input;
	//  �e�A�N�V�����̐錾
	InputAction moveAction;
	InputAction jumpAction;

	void Start() {
		//  �R���|�[�l���g�̎擾
		input = GameObject.Find("InputSystem").GetComponent<PlayerInput>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();

		//  �A�N�V�����̎擾
		moveAction = input.actions["Move"];
		jumpAction = input.actions["Jump"];
	}

	void Update() {
		//  �ړ�����
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

		//  �W�����v����
		if (jumpAction.IsPressed() && canJump) {
			rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
			canJump = false;
			anim.SetBool("Jump", true);
		}

		//	���A
		if (transform.position.y < -10.0f) {
			transform.position = new Vector3(0.0f, 3.0f, 0.0f);
			rb.velocity = Vector3.zero;
		}
	}

#else
	/* InputActionAsset �� Generate C# Class �Ƀ`�F�b�N��t���� */
	/* �֐��|�C���^�Ɠ����悤�Ȏg���� */
	/* �����܂ł�����Ƒ�� */
	/* ��ʓI�Ȃ̂̓R�� */

	SampleInputAction input;

	void Start() {
		//  �R���|�[�l���g�̎擾
		input = MyInputManager.instance.input; 
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();

		//	ActionMap[xxxx]��Action[xxxx]��[xxxx]�̏ꍇ�ɃC�x���g[�֐���]��o�^����
		//	started �������u��, performed �����Ă��, canceled �������u��

		input.Player.Move.performed += OnMovePerformed;
		input.Player.Move.canceled += OnMoveCanceled;
		input.Player.Jump.started += OnJumpStarted;
		input.Enable();
	}

	void Update() {
		//	���A
		if (transform.position.y < -10.0f) {
			transform.position = new Vector3(0.0f, 3.0f, 0.0f);
			rb.velocity = Vector3.zero;
		}

		//	�ړ�
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

	#region �A�j���[�V�����C�x���g

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
