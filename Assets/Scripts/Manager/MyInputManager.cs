using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{
    //  ���g�̃C���X�^���X
    public static MyInputManager instance;

    //  ���͊Ǘ�
    public SampleInputAction input;

	void Awake() {
		instance = this; 
		DontDestroyOnLoad(gameObject);

        input = new SampleInputAction();
        Application.targetFrameRate = 60;
	}

	// Start is called before the first frame update
	void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
