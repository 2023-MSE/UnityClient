using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataAndAudioClipConvertor : MonoBehaviour
{
    public static MusicExtension MusicDataAnalyzer(string inputMusicName)
    {
        if (inputMusicName.Contains(".wav"))
            return MusicExtension.WAV;
        else if (inputMusicName.Contains(".mp3"))
            return MusicExtension.MP3;

        return MusicExtension.NONE;
    }
    
    public static AudioClip ConvertWavByteToAudioClip(byte[] inputByte)
    {
        AudioClip outputAudioClip = null;
        outputAudioClip = WavUtility.ToAudioClip(inputByte);
        return outputAudioClip;
    }
    
    public static AudioClip ConvertMp3ByteToAudioClip(byte[] inputByte)
    {
        AudioClip outputAudioClip = null;
        outputAudioClip = Mp3ToAudioClipCreate(inputByte);
        return outputAudioClip;
    }

    private static AudioClip Mp3ToAudioClipCreate(byte[] mp3Bytes)
    {
        // 읽어온 MP3 파일의 데이터를 읽을 위치와 길이를 저장할 변수
        int position = 0;
        int length = mp3Bytes.Length;

        // MP3 파일의 스테레오 여부
        bool isStereo = false;

        // MP3 파일의 샘플링 레이트
        int sampleRate = 44100;

        // MP3 파일의 비트 레이트
        int bitRate = 128000;

        // MP3 파일의 데이터를 읽어올 때마다 호출되는 함수
        AudioClip.PCMReaderCallback pcmReaderCallback = (data) => {
            int bytesRead = 0;
            while (bytesRead < data.Length) {
                // mp3Bytes 배열에서 position 위치부터 data.Length 만큼 데이터를 읽어옴
                int bytesRemaining = length - position;
                int bytesToRead = Mathf.Min(bytesRemaining, data.Length - bytesRead);
                Array.Copy(mp3Bytes, position, data, bytesRead, bytesToRead);
                bytesRead += bytesToRead;
                position += bytesToRead;
                
                if (bytesToRead == 0) {
                    // 데이터를 모두 읽은 경우, position을 0으로 리셋함
                    position = 0;
                    break;
                }
            }
        };

        // AudioClip.Create 함수를 사용하여 AudioClip 생성
        return AudioClip.Create("RandomName" + GUID.Generate(), length, isStereo ? 2 : 1, sampleRate, true, pcmReaderCallback);
    }
}
