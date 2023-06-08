using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DungeonInfoFolder
{
    [Serializable]
    public class Stage
    {
        public ulong id;
        // 1. Node ID for Searching and Positioning stages
        public ulong identifierId;
        public List<ulong> prevStage;
        public List<ulong> nextStage;
        
        
        // 2. Specific Stage information
        // 1) Type information
        [Serializable]
        public enum StageType
        {
            Boss,
            Monster,
            Totem,
            Relax
        }

        public StageType stageType;

        // 2) specific information about that type
        public string specificTypeInfo;
        public List<uint> elements;
        public short limitForElements = 0;
        
        // 3) Music information
        public string musicName;
        public AudioClip musicData;
        public byte[] musicBytesData;

        public Stage(ulong inputIdentifierId)
        {
            identifierId = inputIdentifierId;
            prevStage = new List<ulong>();
            nextStage = new List<ulong>();
            
            stageType = StageType.Relax;
            
            specificTypeInfo = "Just Default Stage for new Node";
            elements = new List<uint>();
        }

        public void PrintStageInfo()
        {
            Debug.Log("My Node ID : " + identifierId + "\n" + prevStage.Count + "   " + nextStage.Count);
        }

        public void ShowAllStageInformation()
        {
            Debug.Log("My Node ID : " + identifierId + "\n" +
                      "Prev Stage ID : " + prevStage.Count + "\n" +
                      "Next Stage ID : " + nextStage.Count + "\n" +
                      "My Stage Type : " + stageType + "\n" +
                      "Specific Type Info : " + specificTypeInfo + "\n" +
                      "Element Count : " + elements.Count + "\n" +
                      "Limit for Elements : " + limitForElements);
            
            // Show Stage Music Info
            if (string.IsNullOrEmpty(musicName))
                Debug.Log("Music Name : " + musicName);
            else
                Debug.Log("Music Name : " + musicName + "\n" +
                          "Music Bytes Data : " + musicBytesData.Length);
        }

        public void ShowStageMusicData()
        {
            Debug.Log("Music Name : " + musicName + "\n" +
                      "Music Bytes Data : " + musicBytesData);
        }
    }
}
