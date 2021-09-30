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
        var DLL = Assembly.LoadFile($"{Application.streamingAssetsPath}/Mods/Core/dll/netstandard2.0/Core.dll");

        var MainSceneType = DLL.GetType("Core.UI.Scene.MainScene");

        var c = Activator.CreateInstance(MainSceneType);

        var componetName = MainSceneType.GetProperty("componetName").GetValue(c) as string;

        var path = $"{Application.streamingAssetsPath}/Mods/Core/ui/Package_fui.bytes";

        var bytes = File.ReadAllBytes(path);

        UIPackage.LoadResource loadResource = (string name, string extension, System.Type type, out DestroyMethod destroyMethod) =>
        {
            destroyMethod = DestroyMethod.Destroy;

            var path = Application.streamingAssetsPath + "/Study/" + name + extension;

            if (!File.Exists(path))
            {
                Debug.LogWarning("Can not find file " + path);
                return null;
            }

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);

            return texture;
        };

        var assetNamePrefix = Path.GetFileNameWithoutExtension(path).Replace("_fui", "");

        UIPackage.AddPackage(bytes, assetNamePrefix, loadResource);

        var com = UIPackage.CreateObject(assetNamePrefix, componetName).asCom;
        GRoot.inst.AddChild(com);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
