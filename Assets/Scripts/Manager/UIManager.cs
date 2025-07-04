using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

using static GameConst;

public class UIManager : MonoBehaviour {
    //�e�L�X�g�Ǘ��p�񋓒萔
    private enum TextType {
        Invalid = -1,
        Timer,
        Score,
        Start,
        Combo,

        Max,
    }
    //�e�L�X�g�̃��X�g
    [SerializeField]
    private List<TextMeshProUGUI> textList = null;
    [SerializeField]
    private List<Image> images = null;
    private StringBuilder stringBuilder = new StringBuilder();
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        #region �^�C�}�[
        stringBuilder.Append(GameManager.instance.minute.ToString("00"));
        stringBuilder.Append(":");
        stringBuilder.Append(((int)GameManager.instance.second).ToString("00"));
        textList[(int)TextType.Timer].text =
            stringBuilder.ToString();
        stringBuilder.Clear();
        #endregion
        //�X�R�A�̃e�L�X�g
        textList[(int)TextType.Score].text =
            "Score : " + ScoreManager.AllScore;
        //�R���{��UI
        if (Player.GetCombo() >= FRUIT_FIRST_MIN) {
            stringBuilder.Append(Player.GetCombo().ToString());
            stringBuilder.Append("combo!!!\n+");
            stringBuilder.Append(ScoreManager.BonusScore);
            textList[(int)TextType.Combo].text =
                stringBuilder.ToString();
            stringBuilder.Clear();
        }
        else
            textList[(int)TextType.Combo].text = null;


        //�Q�[�����͕\�����Ȃ��e�L�X�g
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
