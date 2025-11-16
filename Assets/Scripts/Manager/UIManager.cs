using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameConst;
using static GameEnum;

public class UIManager : MonoBehaviour {
    public static UIManager instance = null;

    //テキスト管理用列挙定数
    private enum eTextType {
        Invalid = -1,
        Timer,
        Score,
        Start,
        Combo,
        Name,
        Time,

        Max,
    }

    private int prevCombo = 0;
    
    //テキストのリスト
    [SerializeField]
    private List<TextMeshProUGUI> textList = null;
    //画像のリスト
    [SerializeField]
    private List<Image> images = null;
    //エフェクトのリスト
    [SerializeField]
    private List<GameObject> effects = null;
    //エフェクトの出現位置
    [SerializeField]
    private Transform effectRoot = null;
    private StringBuilder stringBuilder = new StringBuilder();
    [SerializeField]
    private List<GameObject> useCanvas = null;
    [SerializeField]
    private List<GameObject> useButton = null;
    [SerializeField]
    Transform AddTimeRoot = null;



    #region オンラインランキング関連
    [SerializeField]
    private GameObject rankingPrefab = null;
    [SerializeField]
    private Transform rankingRoot = null;
    [SerializeField]
    private TMP_InputField inputField = null;


    private bool isShowRanking = false;

    private const int MAX_SHOW_RANKING = 10;
    #endregion



    ////使用状態リスト
    //private List<GameObject> useObjectList = null;
    ////未使用状態リスト
    //private List<GameObject> unuseObjectList = null;
    // Start is called before the first frame update
    void Start() {
        instance = this;
        inputField.onSubmit.AddListener(OnNameSubmitted);
        useCanvas[(int)eCanvasType.OfflineRanking].SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        GameObject useEffect = null;
        #region タイマー
        stringBuilder.Append(GameManager.instance.minute.ToString("00"));
        stringBuilder.Append(":");
        stringBuilder.Append(((int)GameManager.instance.second).ToString("00"));
        textList[(int)eTextType.Timer].text =
            stringBuilder.ToString();
        stringBuilder.Clear();
        #endregion
        //スコアのテキスト
        textList[(int)eTextType.Score].text =
            "Score : " + ScoreManager.AllScore;
        //コンボのUI
        if (Player.GetCombo() > FRUIT_FIRST_MIN && Player.GetCombo() >= FRUIT_FIRST_MIN) {
            if (Player.GetCombo() > prevCombo) {
                useEffect = Instantiate(effects[2], effectRoot);
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



        //ゲーム中は表示しないテキスト
        if (GameManager.instance.IsPlay) {
            HideCanvas((int)eCanvasType.OutGameCanvas);
            ShowCanvas((int)eCanvasType.InGameCanvas);
            textList[(int)eTextType.Start].enabled = false;
            images[0].enabled = false;
            textList[(int)eTextType.Start].text =
                "Game over!!\nEnd to EscapeKey";
        }
        else if (isShowRanking) {
            HideCanvas((int)eCanvasType.OutGameCanvas);
            HideCanvas((int)eCanvasType.InGameCanvas);
        }

        else {
            HideCanvas((int)eCanvasType.InGameCanvas);
            ShowCanvas((int)eCanvasType.OutGameCanvas);
            if (PhaseManager.instance.phase == GamePhase.PhaseEnd) {
                textList[(int)eTextType.Start].enabled = false;
                textList[(int)eTextType.Start].GetComponentInParent<Image>().color = new Color(0, 0, 0, 0);
            }
            else {
                textList[(int)eTextType.Start].enabled = true;
                textList[(int)eTextType.Start].GetComponentInParent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            images[0].enabled = true;
            //ボタンの表示、非表示
            if (PhaseManager.instance.phase == GamePhase.PhaseEnd) {
                for (int i = 0, max = useButton.Count; i < max; i++) {
                    useButton[i].SetActive(true);
                }
            }
            else {
                for (int i = 0, max = useButton.Count; i < max; i++) {
                    useButton[i].SetActive(false);
                }
            }
        }
        //1フレーム前のコンボ数を更新
        prevCombo = Player.GetCombo();

    }

    public void ShowOnlineRanking() {
        isShowRanking = true;
        useCanvas[2].SetActive(true);
        //子オブジェクトを削除
        foreach (Transform child in rankingRoot) {
            Destroy(child.gameObject);
        }


        List<RankingData> rankingDatas = OnlineRankingManager.instance.GetOnlineRankingData();

        OnlineRankingManager.instance.LoadRankingData((rankingDatas)=>GenerateRankingUI(rankingDatas));
        
    }

    /// <summary>
    /// ランキングUI生成
    /// </summary>
    /// <param name="_rankingDatas"></param>
    private void GenerateRankingUI(List<RankingData> _rankingDatas) {
        for (int i = 0; i < MAX_SHOW_RANKING; i++) {
            GameObject rankingDataObject = Instantiate(rankingPrefab, rankingRoot);

            if (i < _rankingDatas.Count) {
                RankingData data = _rankingDatas[i];
                rankingDataObject.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text =
                    (i + 1).ToString();
                rankingDataObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text =
                    data.name;
                rankingDataObject.transform.Find("Score").GetComponent<TextMeshProUGUI>().text =
                    data.score.ToString();
                //自分のデータなら演出発火
                if (data.id == OnlineRankingManager.instance.GetCustomID()) {
                    var animator = rankingDataObject.GetComponent<Animator>();
                    if (animator != null)
                        animator.SetTrigger("ShowRecord");

                    EffectManager.instance.ExecuteEffect((int)eEffectCategory.RankingFlash,rankingDataObject.transform);
                }

            }
            else {
                rankingDataObject.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = "";
                rankingDataObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "";
                rankingDataObject.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "";
            }
        }

    }

    private void OnNameSubmitted(string _text) {
        Debug.Log("名前確定: " + _text);
        OnlineRankingManager.instance.SetUserName(_text);
    }

    public string GetInputName() {
        return textList[(int)eTextType.Name].text;
    }

    public void HideCanvas(int _index) {
        useCanvas[_index].SetActive(false);
        if (_index == (int)eCanvasType.OfflineRanking)
            isShowRanking = false;
    }

    public void ShowCanvas(int _index) {
        useCanvas[_index].SetActive(true);
    }

    public void ResetUI() {
        textList[(int)eTextType.Start].text =
               "Start To Left Click!!";
    }

    public async UniTask ShowTimeAddUI(float _targetAlpha, float _duration = 1.0f) {
        TextMeshProUGUI time = textList[(int)eTextType.Time];
        time.enabled = true;
        time.transform.position = AddTimeRoot.position;
        Vector3 timePosition = time.transform.position;
        float elapsedTime = 0.0f;
        float startAlpha = time.color.a;
        Color changeColor = time.color;
        while (elapsedTime < _duration) {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / _duration;
            changeColor.a = Mathf.Lerp(startAlpha, _targetAlpha, t);
            time.color = changeColor;

            timePosition.y -= 0.5f;
            await UniTask.DelayFrame(1);
        }
        changeColor.a = _targetAlpha;
        time.color = changeColor;
        transform.position = timePosition;


    }
}
