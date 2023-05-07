using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DungeonInfoFolder
{
    [Serializable]
    public class Stage
    {
        // 1. Node ID for Searching and Positioning stages
        public ulong nodeID;
        public List<ulong> prevStageID;
        public List<ulong> nextStageID;
        
        
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

        public StageType myStageType;

        // 2) specific information about that type
        public string specificTypeInfo;
        public List<uint> elements;
        public short limitForElements = 0;
        
        // 3) Music information
        public string musicName;
        public AudioClip musicData;
        public byte[] musicBytesData;

        public Stage(ulong inputNodeID)
        {
            nodeID = inputNodeID;
            prevStageID = new List<ulong>();
            nextStageID = new List<ulong>();
            
            myStageType = StageType.Relax;
            
            specificTypeInfo = "Just Default Stage for new Node";
            elements = new List<uint>();
        }

        public void PrintStageInfo()
        {
            Debug.Log("My Node ID : " + nodeID + "\n" + prevStageID.Count + "   " + nextStageID.Count);
        }

        public void ShowAllStageInformation()
        {
            Debug.Log("My Node ID : " + nodeID + "\n" +
                      "Prev Stage ID : " + prevStageID.Count + "\n" +
                      "Next Stage ID : " + nextStageID.Count + "\n" +
                      "My Stage Type : " + myStageType + "\n" +
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
    }
}
