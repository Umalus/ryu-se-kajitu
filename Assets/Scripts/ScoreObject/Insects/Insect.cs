using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using static GameEnum;

/// <summary>
/// ���_�������Ă���N���X
/// </summary>
public class Insect : BaseScoreObject
{
    private void Start() {
        base.Initialize();
    }

    // Update is called once per frame
    void Update() {
        //�~�点��
        fallObject();
        //�j�����鏈��
        DeleteObject((int)FallObjectType.Insect,(int)SEIndex.InsectSound);
    }
    
}
