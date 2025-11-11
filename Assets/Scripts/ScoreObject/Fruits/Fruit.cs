using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

public class Fruit : BaseScoreObject {
    private void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        //~‚ç‚¹‚é
        fallObject();
        //”jŠü‚·‚éˆ—
        DeleteObject((int)FallObjectType.Apple ,(int)SEIndex.FruitSound);
    } 
}
