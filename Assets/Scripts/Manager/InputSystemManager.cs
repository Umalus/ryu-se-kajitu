using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystemManager : MonoBehaviour
{
    public static InputSystemManager instance = null;

    public Bozu InputSystem = null;
    void Start()
    {
    }

    private void Awake() {
        instance = this;
        InputSystem = new Bozu();
    }
}
