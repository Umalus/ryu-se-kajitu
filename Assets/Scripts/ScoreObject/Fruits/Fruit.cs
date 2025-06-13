using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : BaseScoreObject
{
    [SerializeField]
    private BaseScoreData scoreData;
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
    }

    private void FallFruit() {
        Vector3 fallPos = transform.position;
        fallPos.y -= scoreData.fallSpeed;
        transform.position = fallPos;
    }
    
}
