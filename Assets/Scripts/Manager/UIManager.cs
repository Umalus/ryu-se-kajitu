using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

using static GameConst;
using Cysharp.Threading.Tasks;

public class UIManager : MonoBehaviour {
    //�e�L�X�g�Ǘ��p�񋓒萔
    private enum eTextType {
        Invalid = -1,
        Timer,
        Score,
        Start,
        Combo,

        Max,
    }
    private enum eCanvasType {
        Invalid = -1,
        InGameCanvas,
        OutGameCanvas,

        Max
    }
    private int prevCombo = 0;
    //�e�L�X�g�̃��X�g
    [SerializeField]
    private List<TextMeshProUGUI> textList = null;
    //�摜�̃��X�g
    [SerializeField]
    private List<Image> images = null;
    //�G�t�F�N�g�̃��X�g
    [SerializeField]
    private List<GameObject> effects = null;
    //�G�t�F�N�g�̏o���ʒu
    [SerializeField]
    private Transform effectRoot = null;
    private StringBuilder stringBuilder = new StringBuilder();
    [SerializeField]
    private List<GameObject> useCanvas = null;
    [SerializeField]
    private List<GameObject> useButton = null;

    ////�g�p��ԃ��X�g
    //private List<GameObject> useObjectList = null;
    ////���g�p��ԃ��X�g
    //private List<GameObject> unuseObjectList = null;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        GameObject useEffect = null;
        #region �^�C�}�[
        stringBuilder.Append(GameManager.instance.minute.ToString("00"));
        stringBuilder.Append(":");
        stringBuilder.Append(((int)GameManager.instance.second).ToString("00"));
        textList[(int)eTextType.Timer].text =
            stringBuilder.ToString();
        stringBuilder.Clear();
        #endregion
        //�X�R�A�̃e�L�X�g
        textList[(int)eTextType.Score].text =
            "Score : " + ScoreManager.AllScore;
        //�R���{��UI
        if (Player.GetCombo() >= FRUIT_FIRST_MIN) {
            if(Player.GetCombo() > prevCombo) {
                useEffect = Instantiate(effects[1], effectRoot);
            }
            stringBuilder.Append(Player.GetCombo().ToString());
            stringBuilder.Append("combo!!!\n+");
            stringBuilder.Append(ScoreManager.BonusScore);
            textList[(int)eTextType.Combo].text =
                stringBuilder.ToString();
            stringBuilder.Clear();
        }
        else
            textList[(int)eTextType.Combo].text = null;



        //�Q�[�����͕\�����Ȃ��e�L�X�g
        if (GameManager.instance.IsPlay) {
            useCanvas[(int)eCanvasType.OutGameCanvas].SetActive(false);
            useCanvas[(int)eCanvasType.InGameCanvas].SetActive(true);
            textList[(int)eTextType.Start].enabled = false;
            images[0].enabled = false;
            textList[(int)eTextType.Start].text =
                "Game over!!\nEnd to EscapeKey";
        }

        else {
            useCanvas[(int)eCanvasType.InGameCanvas].SetActive(false);
            useCanvas[(int)eCanvasType.OutGameCanvas].SetActive(true);
            textList[(int)eTextType.Start].enabled = true;
            images[0].enabled = true;
            //�{�^���̕\���A��\��
            if(GameManager.instance.phase == GameEnum.GamePhase.PhaseEnd) {
                for(int i = 0,max = useButton.Count; i < max; i++) {
                    useButton[i].SetActive(true);
                }
            }
            else {
                for (int i = 0, max = useButton.Count; i < max; i++) {
                    useButton[i].SetActive(false);
                }
            }
        }
        //1�t���[���O�̃R���{�����X�V
        prevCombo = Player.GetCombo();
        //1�b��ɃG�t�F�N�g���폜
        if (useEffect != null) {
            Destroy(useEffect, 1.0f);
        }

    }
}
