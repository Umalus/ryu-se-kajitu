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
        //�~�点��
        fallObject();
        //�j�����鏈��
        DeleteObject((int)FallObjectType.Fruit ,(int)SEIndex.FruitSound);
    } 
}
