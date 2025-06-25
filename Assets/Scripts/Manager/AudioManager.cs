using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager:MonoBehaviour{
    public static AudioManager instance = null;

    //BGMを取得するためのデータ
    [SerializeField]
    private BGMAssigner bgm = null;
    //SEを取得するためのデータ
    [SerializeField]
    private SEAssigner se = null;

    //BGM用オーディオソース
    [SerializeField]
    private AudioSource bgmAudioSource = null;
    //SE用オーディオソース
    [SerializeField]
    private AudioSource[] seAudioSources = null;

    private void Start() {
        instance = this;
    }
    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="_bgmID"></param>
    public void PlayBGM(int _bgmID) {
        //clipに指定のBGMを設定し再生
        bgmAudioSource.clip = bgm.BGMList[_bgmID];
        bgmAudioSource.Play();
    }
    /// <summary>
    /// BGMの停止
    /// </summary>
    public void StopBGM() {
        bgmAudioSource.Stop();
    }
    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="_seID"></param>
    public void PlaySE(int _seID) {
        //使用していないオーディオソースを探す
        for(int i = 0,max = seAudioSources.Length; i < max; i++) {
            AudioSource source = seAudioSources[i];
            if (source == null ||
                source.isPlaying) continue;
            //使われていないオーディオソースがあれば設定し再生
            source.clip = se.seList[_seID];
            source.PlayOneShot(source.clip);
        }

        
    }

}
