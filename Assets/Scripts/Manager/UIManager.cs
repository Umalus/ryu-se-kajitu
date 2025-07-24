using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

using static GameConst;
using Cysharp.Threading.Tasks;

public class UIManager : MonoBehaviour {
    //テキスト管理用列挙定数
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

    ////使用状態リスト
    //private List<GameObject> useObjectList = null;
    ////未使用状態リスト
    //private List<GameObject> unuseObjectList = null;
    // Start is called before the first frame update
    void Start() {

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



        //ゲーム中は表示しないテキスト
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
            //ボタンの表示、非表示
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
        //1フレーム前のコンボ数を更新
        prevCombo = Player.GetCombo();
        //1秒後にエフェクトを削除
        if (useEffect != null) {
            Destroy(useEffect, 1.0f);
        }

    }
}
