using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager:MonoBehaviour{
    public static AudioManager instance = null;

    //BGM���擾���邽�߂̃f�[�^
    [SerializeField]
    private BGMAssigner bgm = null;
    //SE���擾���邽�߂̃f�[�^
    [SerializeField]
    private SEAssigner se = null;

    //BGM�p�I�[�f�B�I�\�[�X
    [SerializeField]
    private AudioSource bgmAudioSource = null;
    //SE�p�I�[�f�B�I�\�[�X
    [SerializeField]
    private AudioSource[] seAudioSources = null;

    private void Start() {
        instance = this;
    }
    /// <summary>
    /// BGM�̍Đ�
    /// </summary>
    /// <param name="_bgmID"></param>
    public void PlayBGM(int _bgmID) {
        //clip�Ɏw���BGM��ݒ肵�Đ�
        bgmAudioSource.clip = bgm.BGMList[_bgmID];
        bgmAudioSource.Play();
    }
    /// <summary>
    /// BGM�̒�~
    /// </summary>
    public void StopBGM() {
        bgmAudioSource.Stop();
    }
    /// <summary>
    /// SE�̍Đ�
    /// </summary>
    /// <param name="_seID"></param>
    public void PlaySE(int _seID) {
        //�g�p���Ă��Ȃ��I�[�f�B�I�\�[�X��T��
        for(int i = 0,max = seAudioSources.Length; i < max; i++) {
            AudioSource source = seAudioSources[i];
            if (source == null ||
                source.isPlaying) continue;
            //�g���Ă��Ȃ��I�[�f�B�I�\�[�X������ΐݒ肵�Đ�
            source.clip = se.seList[_seID];
            source.PlayOneShot(source.clip);
        }

        
    }

}
