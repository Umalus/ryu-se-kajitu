using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミニマップ管理クラス
/// プレイヤーに追尾する
/// </summary>
public class MiniMapCamera : MonoBehaviour
{
    /// <summary>
    /// オフセット
    /// </summary>
    [SerializeField]
    private Vector3 offset = Vector3.zero;
    /// <summary>
    /// 追尾する座標
    /// </summary>
    [SerializeField]
    private Transform player = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followPos = transform.position;
        followPos.x = player.position.x;
        followPos.z = player.position.z;
        transform.position = followPos;
    }
}
