using System;
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
    
    public void SaveMusicAudioClipToStage(string inputMusicName, AudioClip inputMusicData)
    {
        StageEditor.Instance.EditingStage.musicName = inputMusicName;
        StageEditor.Instance.EditingStage.musicData = inputMusicData;
        StageEditor.Instance.EditingStage.musicBytesDataReal = ConvertAudioClipToBytes(inputMusicData);
        Debug.Log(StageEditor.Instance.EditingStage.musicBytesDataReal.Length);

        myAudioSource.clip = inputMusicData;
    }
    
    public byte[] ConvertAudioClipToBytes(AudioClip inputMusicData)
    {
        float[] samples = new float[inputMusicData.samples * inputMusicData.channels];
        inputMusicData.GetData(samples, 0);

        byte[] bytes = new byte[samples.Length * 2];
        Buffer.BlockCopy(samples, 0, bytes, 0, bytes.Length);

        return bytes;
    }
    
    public AudioClip ConvertBytesToAudioClip(byte[] bytesData)
    {
        if (bytesData is not { Length: > 0 }) return null;
        
        float[] floatData = new float[bytesData.Length / 2];
        Buffer.BlockCopy(bytesData, 0, floatData, 0, bytesData.Length);
        for (int i = 0; i < bytesData.Length; i += 4) {
            floatData[i / 4] = BitConverter.ToSingle(bytesData, i);
        }

        int lengthSamples;
        AudioClip newClip;
        
        if (myAudioSource.clip != null)
        {
            lengthSamples = floatData.Length / myAudioSource.clip.channels;
            newClip = AudioClip.Create("newClip", lengthSamples, myAudioSource.clip.channels,
                myAudioSource.clip.frequency, false);
        }
        else
        {
            lengthSamples = floatData.Length / 2;
            newClip = AudioClip.Create("newClip", lengthSamples, 2, 44100, false);
        }

        newClip.SetData(floatData, 0);

        return newClip;
    }
    
    # region Old
    public void SaveMusicToStage(string inputMusicName, byte[] inputMusicData)
    {
        StageEditor.Instance.EditingStage.musicName = inputMusicName;
        StageEditor.Instance.EditingStage.musicBytesDataReal = inputMusicData;
        
        LoadMusicToAudioSource();
    }
    
    public byte[] LoadMusicFromStage()
    {
        return StageEditor.Instance.EditingStage.musicBytesDataReal;
    }
    
    public void LoadMusicToAudioSource()
    {
        if (string.IsNullOrEmpty(StageEditor.Instance.EditingStage.musicName))
            return;
        
        AudioType thisMusicExtension = DataAndAudioClipConvertor.MusicDataAnalyzer(StageEditor.Instance.EditingStage.musicName);
        switch (thisMusicExtension)
        {
            case AudioType.MPEG :
                myAudioSource.clip = DataAndAudioClipConvertor.ConvertMp3ByteToAudioClip(LoadMusicFromStage());
                break;
            case AudioType.WAV :
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
    #endregion
}
