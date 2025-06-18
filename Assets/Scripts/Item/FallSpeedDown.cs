using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpeedDown : BaseItem
{
    [SerializeField]
    private BaseScoreData scoreData;

    private float setSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        setSpeed = scoreData.fallSpeed * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void AddEffect() {
        Fruit.SetFallSpeed(setSpeed);
    }
}
