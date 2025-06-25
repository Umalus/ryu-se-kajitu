using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;

public class UIManager : MonoBehaviour
{
    //テキスト管理用列挙定数
    private enum TextType {
        Invalid = -1,
        Timer,
        Score,
        Start,

        Max,
    }
    //テキストのリスト
    [SerializeField]
    private List<TextMeshProUGUI> textList = null;
    private StringBuilder stringBuilder = new StringBuilder();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region タイマー
        stringBuilder.Append(GameManager.instance.minute.ToString("00"));
        stringBuilder.Append(":");
        stringBuilder.Append(((int)GameManager.instance.second).ToString("00"));
        textList[(int)TextType.Timer].text =
            stringBuilder.ToString();
        stringBuilder.Clear();
        #endregion
        //スコアのテキスト
        textList[(int)TextType.Score].text =
            "Score : " + ScoreManager.AllScore;
        //ゲーム中は表示しないテキスト
        if (GameManager.instance.IsPlay) {
            textList[(int)TextType.Start].enabled = false;
            textList[(int)TextType.Start].text =
                "Game over!!\nEnd to EscapeKey";
        }
        else
            textList[(int)TextType.Start].enabled = true;
    }
}
