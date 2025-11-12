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
        //ç~ÇÁÇπÇÈ
        fallObject();
        //îjä¸Ç∑ÇÈèàóù
        DeleteObject((int)FallObjectType.Apple ,SEID);
    } 
}
