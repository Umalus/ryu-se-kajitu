using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private enum textType {
        Invalid = -1,
        Timer,
        Score,

    }

    [SerializeField]
    private List<TextMeshProUGUI> textList = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textList[(int)textType.Timer].text =
            "Time : " + GameManager.instance.currentTime.ToString("0f");
        textList[(int)textType.Score].text =
            "Score : " + ScoreManager.Score;
    }
}
