using System;
using System.IO;
using System.Reflection;

internal class KernelAssembly
{
    public Type SceneAttribType { get; private set; }
    public Type ButtonAttribType { get; private set; }

    public Type TextAttribType { get; private set; }

    private Assembly assembly;

    public KernelAssembly(string path)
    {

        var bytes = File.ReadAllBytes(path + "GUI.dll");

        assembly = Assembly.Load(bytes);

        SceneAttribType = assembly.GetType("JiangH.GUI.Scene");

        ButtonAttribType = assembly.GetType("JiangH.GUI.Button");

        TextAttribType = assembly.GetType("JiangH.GUI.Text");
    }

    internal Type GetType(string typeName)
    {
        return assembly.GetType(typeName);
    }
}