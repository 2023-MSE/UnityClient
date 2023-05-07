using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MusicData
{
    public string musicName;
    public byte[] musicBytesData;
}

public class MusicDungeonInterface : Singleton<MusicDungeonInterface>
{
    public AudioSource myAudioSource;
    
    public void SaveMusicToStage(string inputMusicName, byte[] inputMusicData)
    {
        StageEditor.Instance.EditingStage.musicName = inputMusicName;
        StageEditor.Instance.EditingStage.musicBytesData = inputMusicData;
        
        LoadMusicToAudioSource();
    }
    
    public byte[] LoadMusicFromStage()
    {
        return StageEditor.Instance.EditingStage.musicBytesData;
    }
    
    public void LoadMusicToAudioSource()
    {
        if (string.IsNullOrEmpty(StageEditor.Instance.EditingStage.musicName))
            return;
        
        MusicExtension thisMusicExtension = DataAndAudioClipConvertor.MusicDataAnalyzer(StageEditor.Instance.EditingStage.musicName);
        switch (thisMusicExtension)
        {
            case MusicExtension.MP3 :
                myAudioSource.clip = DataAndAudioClipConvertor.ConvertMp3ByteToAudioClip(LoadMusicFromStage());
                break;
            case MusicExtension.WAV :
                myAudioSource.clip = DataAndAudioClipConvertor.ConvertWavByteToAudioClip(LoadMusicFromStage());
                break;
        }
    }
    
    public void PlayMusic()
    {
        myAudioSource.Play();
    }

    public void PauseMusic()
    {
        myAudioSource.Pause();
    }
}
