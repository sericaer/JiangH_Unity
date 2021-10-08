using FairyGUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

class ModManager
{
    public Mod core;

    public ModManager(KernelAssembly kernelAssembly, string path)
    {
        Mod.kernelAssembly = kernelAssembly;
        ModElem.kernelAssembly = kernelAssembly;

        core = new Mod(path + "/Core/");
    }
}

class Mod
{
    public static KernelAssembly kernelAssembly;

    public string path { get; private set; }

    private Dictionary<string, Type> sceneTypeDict;

    private string packageName => "Package";

    public Mod(string modPath)
    {
        this.path = modPath;
        this.sceneTypeDict = new Dictionary<string, Type>();

        LoadAssembly();
        LoadUIPackage();
    }

    private void LoadUIPackage()
    {

        //UIPackage.LoadResource loadResource = (string name, string extension, System.Type type, out DestroyMethod destroyMethod) =>
        //{
        //    destroyMethod = DestroyMethod.Destroy;

        //    var path = Application.streamingAssetsPath + "/Study/" + name + extension;

        //    if (!File.Exists(path))
        //    {
        //        Debug.LogWarning("Can not find file " + path);
        //        return null;
        //    }

        //    byte[] bytes = System.IO.File.ReadAllBytes(path);

        //    Texture2D texture = new Texture2D(1, 1);
        //    texture.LoadImage(bytes);

        //    return texture;
        //};

        var uiPath = $"{path}/ui/{packageName}_fui.bytes";

        var bytes = File.ReadAllBytes(uiPath);

        UIPackage.AddPackage(bytes, packageName, null);
    }

    private void LoadAssembly()
    {
        var assembly = Assembly.LoadFile($"{path}/dll/netstandard2.0/Assembly.dll");

        foreach (var type in assembly.GetTypes())
        {
            if (type.Namespace.EndsWith(".UI.Scene"))
            {
                var attrib = type.GetCustomAttribute(kernelAssembly.SceneAttribType);
                if (attrib != null)
                {
                    var sceneName = kernelAssembly.SceneAttribType.GetProperty("name").GetValue(attrib) as string;
                    sceneTypeDict.Add(sceneName, type);
                }
            }
        }
    }

    internal ModElem CreateScene(string name)
    {
        Type type;
        if (!sceneTypeDict.TryGetValue(name, out type))
        {
            return null;
        }

        var mainSceneObj = Activator.CreateInstance(type);

        return new ModElem(packageName, name, mainSceneObj);
    }


}

class ModElem
{
    public static KernelAssembly kernelAssembly;

    public string packageName { get; private set; }
    public string name { get; private set; }
    public object logic { get; private set; }

    public GComponent ui
    {
        get
        {
            if(_ui == null)
            {
                _ui = UIPackage.CreateObject(packageName, name).asCom;

                foreach(var property in logic.GetType().GetProperties())
                {
                    var btnAttrib = property.GetCustomAttribute(kernelAssembly.ButtonAttribType);
                    if(btnAttrib != null)
                    {
                        var btnName = kernelAssembly.ButtonAttribType.GetProperty("name").GetValue(btnAttrib) as string;
                        var btnUI = _ui.GetChild(btnName).asButton;

                        foreach(var sub in property.PropertyType.GetProperties())
                        {
                            var textAttrib = sub.GetCustomAttribute(kernelAssembly.TextAttribType);
                            if (textAttrib != null)
                            {
                                btnUI.text = sub.GetValue(property.GetValue(logic)) as String;
                            }
                        }
                    }
                }

                //foreach (var child in _ui.GetChildren())
                //{
                //    if(child.name)
                //}
            }

            return _ui;
        }
    }

    private GComponent _ui;


    public ModElem(string packageName, string name, object logic)
    {
        this.packageName = packageName;
        this.name = name;
        this.logic = logic;
    }
}