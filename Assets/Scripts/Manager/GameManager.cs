using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Q�[�������X�Ǘ��N���X
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �C���X�^���X
    /// </summary>
    public static GameManager instance = null;

    /// <summary>
    /// �J�n����
    /// </summary>
    [SerializeField]
    private const int PLAY_TIME = 90;
    /// <summary>
    /// ���݂̎���
    /// </summary>
    public float currentTime { get; private set; } = PLAY_TIME;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԃ����炷
        currentTime -= Time.deltaTime;


    }
}
