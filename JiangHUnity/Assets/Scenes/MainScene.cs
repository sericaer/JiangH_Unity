using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System.Reflection;
using System;
using System.IO;

public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var kernelDll = new KernelAssembly($"{Application.streamingAssetsPath}/Kernels/netstandard2.0/");

        var modMgr = new ModManager(kernelDll,
            $"{Application.streamingAssetsPath}/Mods/");

        var sceneObj = modMgr.core.CreateScene(nameof(MainScene));
        GRoot.inst.AddChild(sceneObj.ui);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
