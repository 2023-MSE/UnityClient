using System;
using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder;
using UnityEngine;

namespace _Player
{
    [Serializable]
    public class DeployedDungeonList
    {
        public List<DeployedDungeon> deployedList;

        public DeployedDungeonList()
        {
            deployedList = new List<DeployedDungeon>();
        }
        
        public void SetDeployedList(List<DeployedDungeon> inputDungeons)
        {
            deployedList = inputDungeons;
        }
    }

    [Serializable]
    public class DeployedDungeon {
        public long id;
        public string name;
        public string createdTime;
        public long userId;
    }
}