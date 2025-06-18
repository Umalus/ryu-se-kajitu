using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : BaseScoreObject
{
    [SerializeField]
    public BaseScoreData scoreData = null;
    [SerializeField]
    private static float fallSpeed;
    private void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 0) {
            Destroy(gameObject);
        }

        FallFruit();
    }
    private void Initialize() {
        SetScore(scoreData.score);
        fallSpeed = scoreData.fallSpeed;
    }

    private void FallFruit() {
        Vector3 fallPos = transform.position;
        fallPos.y -= scoreData.fallSpeed;
        transform.position = fallPos;
    }

    public void SetScoreData(BaseScoreData _scoreData) {
        scoreData = _scoreData;
    }
    
    public static void SetFallSpeed(float _speed) {
        fallSpeed = _speed;
    }
}
