using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

using static GameConst;
using static GameEnum;

public class UIManager : MonoBehaviour {
    public static UIManager instance = null;
    
    //�e�L�X�g�Ǘ��p�񋓒萔
    private enum eTextType {
        Invalid = -1,
        Timer,
        Score,
        Start,
        Combo,
        Name,

        Max,
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
    #region �I�t���C�������L���O�֘A
    [SerializeField]
    private OffLineRanking ranking = null;
    [SerializeField]
    private GameObject rankingPrefab = null;
    [SerializeField]
    private Transform rankingRoot = null;

    private bool isShowRanking = false;

    private const int MAX_SHOW_RANKING = 10;
    #endregion
    #region �I�����C�������L���O�֘A
    [SerializeField]
    private OnlineRankingManager rankingManager = null;
    #endregion



    ////�g�p��ԃ��X�g
    //private List<GameObject> useObjectList = null;
    ////���g�p��ԃ��X�g
    //private List<GameObject> unuseObjectList = null;
    // Start is called before the first frame update
    void Start() {
        instance = this;
        useCanvas[(int)eCanvasType.Ranking].SetActive(false);
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
        else if (isShowRanking) {
            useCanvas[(int)eCanvasType.OutGameCanvas].SetActive(false);
            useCanvas[(int)eCanvasType.InGameCanvas].SetActive(false);
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

    public void ShowRanking() {
        isShowRanking = true;
        useCanvas[2].SetActive(true);
        //�q�I�u�W�F�N�g���폜
        foreach(Transform child in rankingRoot) {
            Destroy(child.gameObject);
        }

        List<RankingData> rankingDatas = ranking.GetRankingDatas();
        for(int i = 0;i < MAX_SHOW_RANKING; i++) {
            GameObject rankingDataObject = Instantiate(rankingPrefab, rankingRoot);

            if(i < rankingDatas.Count) {
                RankingData data = rankingDatas[i];
                rankingDataObject.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text =
                    (i + 1).ToString();
                rankingDataObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text =
                    data.name;
                rankingDataObject.transform.Find("Score").GetComponent<TextMeshProUGUI>().text =
                    data.score.ToString();
            }
            else {
                rankingDataObject.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = "-" + 1.ToString();
                rankingDataObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "";
                rankingDataObject.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "";
            }
        }

    }

    public string GetInputName() {
        return textList[(int)eTextType.Name].text;
    }

    public void HideCanvas(int _index) {
        useCanvas[_index].SetActive(false);
        if (_index == (int)eCanvasType.Ranking)
            isShowRanking = false;
    }

    public void ShowCanvas(int _index) {
        useCanvas[_index].SetActive(true);
    }
}
