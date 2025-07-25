using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static GameConst;
using static CommonModul;
using UnityEngine.InputSystem;
using UnityEditor;
using Cysharp.Threading.Tasks;
/// <summary>
/// �Q�[�������X�Ǘ��N���X
/// </summary>
public class GameManager : MonoBehaviour {
    /// <summary>
    /// �C���X�^���X
    /// </summary>
    public static GameManager instance = null;
    //���g��InputSystem
    private Bozu inputAction = null;
    //�t�F�[�Y�Ǘ��p�^�C�}�[�̒萔
    private const int PHASE_OPNING_TIME_START = 90;
    private const int PHASE_OPNING_TIME_END = 70;
    private const int PHASE_MIDDLE_TIME_END = 20;
    private const int PHASE_ENDING_TIME_END = 1;

    private bool isAddRanking = false;
    /// <summary>
    /// �J�n����
    /// </summary>
    [SerializeField]
    private int playTime = 90;

    public float totalTime;

    public bool IsPlay = false;
    /// <summary>
    /// ���݂̎���
    /// </summary>
    public float second { get; private set; } = 0;

    public int minute { get; private set; } = 0;
    public float prevTime { get; private set; } = 0;

    public GamePhase phase { get; private set; } = 0;
    // Start is called before the first frame update
    async void Start() {
        await Initialize();
        //BGM�Đ�
        AudioManager.instance.PlayBGM(0);
        
    }

    // Update is called once per frame
    void Update() {
        //�v���C�ł����ԂłȂ��Ȃ珈�����Ȃ�
        if (!IsPlay) return;
        //�^�C�}�[����
        Timer();
        //�t�F�[�Y����
        Phase();
    }
    /// <summary>
    /// ���ԊǗ�
    /// </summary>
    private void Timer() {
        //�^�C�}�[��0�ȉ��Ȃ珈�����Ȃ�
        if (second <= 0) {
            second = 0;
            return;
        }

        //���Ԃ����炷
        totalTime = minute * MINUTE + second;
        totalTime -= Time.deltaTime;
        //���Ԃ̍Đݒ�
        minute = (int)totalTime / MINUTE;
        second = totalTime - minute * MINUTE;

        prevTime = second;
    }

    /// <summary>
    /// �t�F�[�Y�Ǘ�
    /// </summary>
    private void Phase() {
        //�����Ԃ��w��̒l�͈͓̔��Ȃ�t�F�[�Y��؂�ւ���
        if (InRange((int)totalTime, PHASE_ENDING_TIME_END, PHASE_MIDDLE_TIME_END))
            phase = GamePhase.ending;
        else if (InRange((int)totalTime, PHASE_MIDDLE_TIME_END, PHASE_OPNING_TIME_END))
            phase = GamePhase.middle;
        else if (InRange((int)totalTime, PHASE_OPNING_TIME_END, PHASE_OPNING_TIME_START))
            phase = GamePhase.opening;
        else {
            phase = GamePhase.PhaseEnd;
            IsPlay = false;
            Teardown();
        }

    }

    private async UniTask Initialize() {
        //�O������擾���₷���悤�Ɏ��g�̃C���X�^���X��ݒ�
        instance = this;
        //�t���[�����[�g���Œ�
        Application.targetFrameRate = 60;
        //���Ԑݒ�
        second = playTime;
        totalTime = minute * MINUTE + second;

        //inputSystem�̏�����
        inputAction = InputSystemManager.instance.InputSystem;

        inputAction.GameManager.Start.performed += OnStartPreformed;
        inputAction.GameManager.End.performed += OnEndPreformed;

        await FadeManager.instance.FadeIn();
    }

    /// <summary>
    /// �������
    /// </summary>
    private void Teardown() {
        inputAction.GameManager.Start.performed -= OnStartPreformed;
    }

    public async void ReturnTitle() {
        await FadeManager.instance.FadeOut();
    }
    public void ShowRanking() {
        UIManager.instance.ShowRanking();
    }

    public void AddSocreData() {
        if (isAddRanking) return;
        OffLineRanking.instance.AddRankingData(UIManager.instance.GetInputName(),ScoreManager.AllScore);
        UIManager.instance.ShowRanking();
        isAddRanking = true;
    }
    /// <summary>
    /// �Q�[���J�n
    /// </summary>
    /// <param name="_context"></param>
    private void OnStartPreformed(InputAction.CallbackContext _context) {
        IsPlay = true;
        ScoreManager.AllScore = 0;
    }

    /// <summary>
    /// �Q�[���I��
    /// </summary>
    /// <param name="_context"></param>
    private void OnEndPreformed(InputAction.CallbackContext _context) {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;

#else
         Application.Quit();
#endif
    }
}
