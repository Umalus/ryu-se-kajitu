using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {
    //���g�̃C���X�^���X
    public static FadeManager instance = null;

    [SerializeField]
    private Image fadeImage = null;

    private CancellationTokenSource cts;

    private CancellationToken token;

    private const float DURATION_TIME = 0.3f;
    // Start is called before the first frame update
    private void Awake() {

        //�C���X�^���X�Ɏ��g��ݒ�
        instance = this;
        Color startAlpha = fadeImage.color;
        startAlpha.a = 1.0f;
        fadeImage.color = startAlpha;

        cts = new CancellationTokenSource();

        token = cts.Token;

    }

    // Update is called once per frame
    void Update() {

    }
    /// <summary>
    /// �t�F�[�h�C������
    /// </summary>
    public async UniTask FadeIn(float _duration = DURATION_TIME) {
        await FadeTargetAlpha(0.0f, _duration);
        fadeImage.enabled = false;
    }

    /// <summary>
    /// �t�F�[�h�A�E�g����
    /// </summary>
    public async UniTask FadeOut(float _duration = DURATION_TIME) {
        fadeImage.enabled = true;
        await FadeTargetAlpha(1.0f, _duration);
    }

    private async UniTask FadeTargetAlpha(float _targetAlpha, float _duration) {
        float elapsedTime = 0.0f;
        float startAlpha = fadeImage.color.a;
        Color changeColor = fadeImage.color;
        while (elapsedTime < _duration) {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / _duration;
            changeColor.a = Mathf.Lerp(startAlpha, _targetAlpha, t);
            fadeImage.color = changeColor;

            await UniTask.DelayFrame(1, PlayerLoopTiming.Update, token);
        }
        changeColor.a = _targetAlpha;
        fadeImage.color = changeColor;

    }
}
