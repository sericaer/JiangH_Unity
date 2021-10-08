using System;
using System.Reflection;

internal class KernelAssembly
{
    public Type SceneAttribType { get; private set; }

    private Assembly assembly;

    public KernelAssembly(string path)
    {
        assembly = Assembly.LoadFile(path + "GUI.dll");

        SceneAttribType = assembly.GetType("JiangH.GUI.Scene");
    }

    internal Type GetType(string typeName)
    {
        return assembly.GetType(typeName);
    }
}