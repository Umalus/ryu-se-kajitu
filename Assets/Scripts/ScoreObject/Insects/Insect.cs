using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using static GameEnum;

/// <summary>
/// “¾“_‚ğˆø‚¢‚Ä‚­‚éƒNƒ‰ƒX
/// </summary>
public class Insect : BaseScoreObject
{
    private void Start() {
        base.Initialize();
    }

    // Update is called once per frame
    void Update() {
        //~‚ç‚¹‚é
        fallObject();
        //”jŠü‚·‚éˆ—
        DeleteObject((int)FallObjectType.Insect,(int)SEIndex.InsectSound);
    }
    
}
