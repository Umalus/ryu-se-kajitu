using Cysharp.Threading.Tasks;
using UnityEngine;

using static GameEnum;
using static ItemUtility;
/// <summary>
/// �����Ă���X�s�[�h�𔼕��ɂ���
/// </summary>
public class FallSpeedDown : BaseItem
{
    
    //�X�s�[�h���擾���邽�߂̃f�[�^
    [SerializeField]
    private BaseScoreData scoreData;
    //�ݒ肷�邽�߂̃X�s�[�h
    private float setSpeed = 0.0f;
    //�^�C�}�[�𑖂点�邩�ǂ���
    private static bool IsRunningTime = false;
    //�^�C�}�[
    [SerializeField]
    float timer = 0.0f;


    private new void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
        IsRunningTime = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        setSpeed = scoreData.fallSpeed * 0.5f;
        categoryID = (int)eItemCategory.FallSpeed;
    }
    private new void Update() {
        base.Update();
        if (IsRunningTime)
            timer += Time.deltaTime;
    }

    /// <summary>
    /// �������^�������
    /// </summary>
    public override void AddEffect() {
        Fruit.SetFallSpeed(setSpeed);
        Fruit.IsHalf = true;
    }
    /// <summary>
    /// ���ʂ�����
    /// </summary>
    public override async UniTask DeleteEffect() {
        while (true) {
            if (timer >= 10.0f)
                break;

            timer += Time.deltaTime;

            await UniTask.DelayFrame(1);

        }
        Fruit.IsHalf = false;
    }

    
}
