using UnityEngine;

public class AudioManager : MonoBehaviour {
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
    /// <param name="_volume"></param>
    public void PlaySE(int _seID, float _volume = 1.0f, bool _isLoop = false) {
        //SEをキャッシュ
        AudioClip clip = se.seList[_seID];

        if (clip == null) {
            Debug.LogWarning($"SE clip at ID {_seID} is null.");
            return;
        }


        //使用していないオーディオソースを探す
        for (int i = 0, max = seAudioSources.Length; i < max; i++) {
            AudioSource source = seAudioSources[i];
            source.loop = _isLoop;
            if (source == null || source.isPlaying) continue;
            //使われていないオーディオソースがあれば設定し再生
            source.clip = clip;
            source.volume = _volume;
            source.PlayOneShot(source.clip);
            return;
        }
    }
}
