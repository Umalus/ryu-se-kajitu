using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// “¾“_‚ğˆø‚¢‚Ä‚­‚éƒNƒ‰ƒX
/// </summary>
public class Insect : BaseScoreObject
{
    [SerializeField]
    private BaseScoreData scoreData;
    private void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y <= 0) {
            Destroy(gameObject);
        }

        FallInsect();
    }
    /// <summary>
    /// ‰Šú‰»ŠÖ”
    /// </summary>
    private void Initialize() {
        SetScore(scoreData.score);
    }
    /// <summary>
    /// ’‚ğ—‰º‚³‚¹‚é
    /// </summary>
    private void FallInsect() {
        Vector3 fallPos = transform.position;
        fallPos.y -= scoreData.fallSpeed;
        transform.position = fallPos;
    }
}
