using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���̊��N���X
/// </summary>
public abstract class BaseItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player"))
            AddEffect();

        Debug.Log("!!!");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �^����e���֐��̒��ۃ��\�b�h
    /// </summary>
    public abstract void AddEffect();

    public abstract void DeleteEffect();
}
