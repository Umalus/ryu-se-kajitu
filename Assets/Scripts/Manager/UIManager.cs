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
        stringBuilder.Append(((int)GameManager.instance.currentTime).ToString("00"));
        textList[(int)textType.Timer].text =
            stringBuilder.ToString();
        stringBuilder.Clear();

        textList[(int)textType.Score].text =
            "Score : " + ScoreManager.Score;
    }
}
