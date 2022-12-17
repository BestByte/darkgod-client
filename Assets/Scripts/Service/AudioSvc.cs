/****************************************************
    文件：AudioService.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/4 5:21:39
	功能：声音播放服务
*****************************************************/

using PEProtocol;

using UnityEngine;

public class AudioService : MonoBehaviour {
    public static AudioService Instance = null;
    public AudioSource bgAudio;
    public AudioSource uiAudio;

    public void InitSvc() {
        Instance = this;
        PECommon.Log("Init Audio...");
    }

    public void StopBGMusic() {
        if (bgAudio != null) {
            bgAudio.Stop();
        }
    }


    public void PlayBGMusic(string name, bool isLoop = true) {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        if (bgAudio.clip == null || bgAudio.clip.name != audio.name) {
            bgAudio.clip = audio;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }
    }

    public void PlayUIAudio(string name) {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        uiAudio.clip = audio;
        uiAudio.Play();
    }

    public void PlayCharAudio(string name, AudioSource audioSrc) {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        audioSrc.clip = audio;
        audioSrc.Play();
    }
}