using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //テキスト管理用列挙定数
    private enum TextType {
        Invalid = -1,
        Timer,
        Score,
        Start,
        Combo,

        Max,
    }
    //テキストのリスト
    [SerializeField]
    private List<TextMeshProUGUI> textList = null;
    [SerializeField]
    private List<Image> images = null;
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
        //コンボのUI


        //ゲーム中は表示しないテキスト
        if (GameManager.instance.IsPlay) {
            textList[(int)TextType.Start].enabled = false;
            images[0].enabled = false;
            textList[(int)TextType.Start].text =
                "Game over!!\nEnd to EscapeKey";
        }
        else {
            textList[(int)TextType.Start].enabled = true;
            images[0].enabled = true;
        }
           
    }
}
