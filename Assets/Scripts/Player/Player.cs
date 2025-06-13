using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    [SerializeField]
    private Joystick useJoyStick = null;
    [SerializeField]
    private float playerVelocity = 5.0f;
    public int Score = 0;
    Rigidbody rb;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Fruit") || other.gameObject.CompareTag("Insect")) {
            BaseScoreObject addScoreObj = other.gameObject.GetComponent<BaseScoreObject>();
            ScoreManager.Score += addScoreObj.score;
            Destroy(other.gameObject);
        }
    }
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update() {

        Vector3 direction = useJoyStick.Vertical * Vector3.forward + useJoyStick.Horizontal * Vector3.right;
        transform.position += Vector3.Normalize(direction) * playerVelocity;

        transform.eulerAngles = direction;
    }
}
