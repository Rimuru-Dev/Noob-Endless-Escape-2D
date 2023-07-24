using System;
using System.Collections;
using System.Collections.Generic;
using Internal.Codebase.Infrastructure.Services.PersistenProgress;
using Internal.Codebase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

public class SaveLoadHandler : MonoBehaviour
{
    [Inject] public IPersistenProgressService PersistenProgressService;
    [Inject] public IStaticDataService StaticDataService;

    public static SaveLoadHandler Instance;

    private void Awake()
    {
        // if (Instance == null)
        //     Instance = this;
        //
        // DontDestroyOnLoad(this);

        Debug.Log($"IPersistenProgressService == null -> {PersistenProgressService == null}");
        Debug.Log($"IStaticDataService == null -> {StaticDataService == null}");
    }
}