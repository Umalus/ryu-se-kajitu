using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Joystick useJoyStick = null;
    [SerializeField]
    private float playerVelocity = 5.0f;
    public int Score = 0;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Vector3 direction = useJoyStick.Vertical * Vector3.forward + useJoyStick.Horizontal * Vector3.right;
        rb.AddForce(direction * playerVelocity * Time.deltaTime,ForceMode.VelocityChange);
    }
}
