using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

using static GameConst;
using static GameEnum;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;

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

    #region オフラインランキング関連
    [SerializeField]
    private OffLineRanking ranking = null;
    [SerializeField]
    private GameObject rankingPrefab = null;
    [SerializeField]
    private Transform rankingRoot = null;

    private bool isShowRanking = false;

    private const int MAX_SHOW_RANKING = 10;
    #endregion
    #region オンラインランキング関連
    [SerializeField]
    private OnlineRankingManager onlineRankingManager = null;
    #endregion



    ////使用状態リスト
    //private List<GameObject> useObjectList = null;
    ////未使用状態リスト
    //private List<GameObject> unuseObjectList = null;
    // Start is called before the first frame update
    void Start() {
        instance = this;
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
        if (Player.GetCombo() >= FRUIT_FIRST_MIN) {
            if (Player.GetCombo() > prevCombo) {
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
            textList[(int)eTextType.Start].enabled = true;
            images[0].enabled = true;
            //ボタンの表示、非表示
            if (GameManager.instance.phase == GameEnum.GamePhase.PhaseEnd) {
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
        //1秒後にエフェクトを削除
        if (useEffect != null) {
            Destroy(useEffect, 1.0f);
        }

    }

    public void ShowOfflineRanking() {
        isShowRanking = true;
        useCanvas[2].SetActive(true);
        //子オブジェクトを削除
        foreach (Transform child in rankingRoot) {
            Destroy(child.gameObject);
        }

        List<RankingData> rankingDatas = ranking.GetRankingDatas();
        for (int i = 0; i < MAX_SHOW_RANKING; i++) {
            GameObject rankingDataObject = Instantiate(rankingPrefab, rankingRoot);

            if (i < rankingDatas.Count) {
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
        if (_index == (int)eCanvasType.OfflineRanking)
            isShowRanking = false;
    }

    public void ShowCanvas(int _index) {
        useCanvas[_index].SetActive(true);
    }

    public void ResetUI() {
        textList[(int)eTextType.Start].text =
               "Start To Touch Screen!!";
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
