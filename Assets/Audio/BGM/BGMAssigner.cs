using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BGMAssigner :ScriptableObject
{
    //BGM�̃��X�g
    [SerializeField]
    public List<AudioClip> BGMList = null;
}
