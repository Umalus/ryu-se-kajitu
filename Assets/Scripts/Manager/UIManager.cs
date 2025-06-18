using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;

public class UIManager : MonoBehaviour
{
    private enum textType {
        Invalid = -1,
        Timer,
        Score,
        Start,

        Max,
    }

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
        stringBuilder.Append(GameManager.instance.minute.ToString("00"));
        stringBuilder.Append(":");
        stringBuilder.Append(((int)GameManager.instance.second).ToString("00"));
        textList[(int)textType.Timer].text =
            stringBuilder.ToString();
        stringBuilder.Clear();

        textList[(int)textType.Score].text =
            "Score : " + ScoreManager.Score;
        if (GameManager.instance.IsPlay) {
            textList[(int)textType.Start].enabled = false;
            textList[(int)textType.Start].text =
                "Game over!!\nEnd to EscapeKey";
        }
        else
            textList[(int)textType.Start].enabled = true;
    }
}
