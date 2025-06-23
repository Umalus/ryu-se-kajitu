using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager:MonoBehaviour{
    public static AudioManager instance = null;

    [SerializeField]
    private BGMAssigner bgm = null;
    [SerializeField]
    private SEAssigner se = null;

    [SerializeField]
    private AudioSource bgmAudioSource = null;
    [SerializeField]
    private AudioSource[] seAudioSources = null;

    private void Start() {
        instance = this;
    }

    public void PlayBGM(int _bgmID) {
        bgmAudioSource.clip = bgm.BGMList[_bgmID];
        bgmAudioSource.Play();
    }

    public void StopBGM() {
        bgmAudioSource.Stop();
    }

    public void PlaySE(int _seID) {
        for(int i = 0,max = seAudioSources.Length; i < max; i++) {
            AudioSource source = seAudioSources[i];
            if (source == null ||
                source.isPlaying) continue;

            source.clip = se.seList[_seID];
            source.PlayOneShot(source.clip);
        }

        
    }

}
