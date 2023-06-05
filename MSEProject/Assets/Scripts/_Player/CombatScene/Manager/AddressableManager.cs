using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using _Creator.DungeonInfoFolder;
using Unity.VisualScripting;
using UnityEngine.AddressableAssets;

public class AddressableManager : Singleton<AddressableManager>
{
    private Dictionary<uint, AsyncOperationHandle> assetDict;

    private void Start()
    {
        assetDict = new Dictionary<uint, AsyncOperationHandle>();
    }

    public void SetAddressable(List<StageInfoStruct> stageInfo)
    {
        foreach (StageInfoStruct info in stageInfo)
        {
            Addressables.LoadAssetAsync<GameObject>(info.prefabPath).Completed +=
                (handle) =>
                {
                    Debug.Log("Load Asset " + info.stageInfo);
                    Debug.Assert(handle.Status == AsyncOperationStatus.Succeeded, "Fail to load Asset" + handle.Status);
                    assetDict.Add(info.thisStageInfoIndex, handle);
                };
        }
    }
    
    public AsyncOperationHandle GetHandle(uint index)
    {
        return assetDict[index];
    }
}
