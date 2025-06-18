using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

[System.Serializable]
public struct HUD4Buttons {
	[SerializeField] public Image down;
	[SerializeField] public Image left;
	[SerializeField] public Image right;
	[SerializeField] public Image up;
}

public class HUDManager : MonoBehaviour {
	//	コンポーネントの宣言
	SampleInputAction input;

	//	メンバ変数の宣言
	[Header("D-Pad"), SerializeField] HUD4Buttons arrows;
	[Header("4Buttons"), SerializeField] HUD4Buttons buttons;
	[Header("LStick"), SerializeField] Image lStick;
	[Header("RStick"), SerializeField] Image rStick;
	Vector2 lStickDefaultPosition;
	Vector2 rStickDefaultPosition;


	void Start() {
		input = MyInputManager.instance.input;
		lStickDefaultPosition = lStick.GetComponent<RectTransform>().anchoredPosition;
		rStickDefaultPosition = rStick.GetComponent<RectTransform>().anchoredPosition;

		input.Player.Move.performed += OnMovePerformed;
		input.Player.Move.canceled += OnMoveCanceled;

		input.Player.Look.performed += OnLookPerformed;
		input.Player.Look.canceled += OnLookCanceled;

		input.Player.DPad.performed += OnDPadPerformed;
		input.Player.DPad.canceled += OnDPadCanceled;

		input.Player.Jump.performed += OnJumpPerformed;
		input.Player.Jump.canceled += OnJumpCanceled;
		input.Player.TestE.performed += OnTestEPerformed;
		input.Player.TestE.canceled += OnTestECanceled;
		input.Player.TestW.performed += OnTestWPerformed;
		input.Player.TestW.canceled += OnTestWCanceled;
		input.Player.TestS.performed += OnTestSPerformed;
		input.Player.TestS.canceled += OnTestSCanceled;

	}

	void OnMovePerformed(InputAction.CallbackContext context) {
		var inputVec = context.ReadValue<Vector2>();
		lStick.GetComponent<RectTransform>().anchoredPosition = lStickDefaultPosition + inputVec * 50;
	}
	void OnMoveCanceled(InputAction.CallbackContext context) {
		lStick.GetComponent<RectTransform>().anchoredPosition = lStickDefaultPosition;
	}

	void OnLookPerformed(InputAction.CallbackContext context) {
		var inputVec = context.ReadValue<Vector2>();
		Debug.Log(inputVec);
		rStick.GetComponent<RectTransform>().anchoredPosition = rStickDefaultPosition + inputVec * 50;
	}
	void OnLookCanceled(InputAction.CallbackContext context) {
		rStick.GetComponent<RectTransform>().anchoredPosition = rStickDefaultPosition;
	}

	void OnDPadPerformed(InputAction.CallbackContext context) {
		var inputVec = context.ReadValue<Vector2>();
		arrows.up.color = Color.white;
		arrows.left.color = Color.white;
		arrows.right.color = Color.white;
		arrows.down.color = Color.white;

		if (inputVec.x > 0) {
			arrows.right.color = Color.red;
		} else if (inputVec.x < 0) {
			arrows.left.color = Color.red;
		}
		if (inputVec.y > 0) {
			arrows.up.color = Color.red;
		} else if (inputVec.y < 0) {
			arrows.down.color = Color.red;
		}
	}

	void OnDPadCanceled(InputAction.CallbackContext context) {
		arrows.up.color = Color.white;
		arrows.left.color = Color.white;
		arrows.right.color = Color.white;
		arrows.down.color = Color.white;
	}


	void OnJumpPerformed(InputAction.CallbackContext context) {
		buttons.up.color = Color.red;
	}
	void OnJumpCanceled(InputAction.CallbackContext context) {
		buttons.up.color = Color.white;
	}
	void OnTestEPerformed(InputAction.CallbackContext context) {
		buttons.right.color = Color.red;
	}
	void OnTestECanceled(InputAction.CallbackContext context) {
		buttons.right.color = Color.white;
	}
	void OnTestWPerformed(InputAction.CallbackContext context) {
		buttons.left.color = Color.red;
	}
	void OnTestWCanceled(InputAction.CallbackContext context) {
		buttons.left.color = Color.white;
	}
	void OnTestSPerformed(InputAction.CallbackContext context) {
		buttons.down.color = Color.red;
	}
	void OnTestSCanceled(InputAction.CallbackContext context) {
		buttons.down.color = Color.white;
	}
}
