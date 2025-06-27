using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

public class Fruit : BaseScoreObject {
    [SerializeField]
    private BaseScoreData scoreData = null;
    [SerializeField]
    private static float fallSpeed;

    public static bool IsHalf = false;
    private void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        //ç~ÇÁÇπÇÈ
        FallFruit();
        //îjä¸Ç∑ÇÈèàóù
        DeleteObject((int)SEIndex.FruitSound);
    }
    private void Initialize() {
        SetScore(scoreData.score);
        if (!IsHalf)
            fallSpeed = scoreData.fallSpeed;
    }

    private void FallFruit() {
        Vector3 fallPos = transform.position;
        fallPos.y -= fallSpeed;
        transform.position = fallPos;
    }

    public void SetScoreData(BaseScoreData _scoreData) {
        scoreData = _scoreData;
    }

    public static void SetFallSpeed(float _speed) {
        fallSpeed = _speed;
    }
}
