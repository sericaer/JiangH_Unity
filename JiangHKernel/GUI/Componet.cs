using System;

namespace JiangH.GUI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Component : Attribute
    {
        public string name { get; private set; }

        public Component(string name)
        {
            this.name = name;
        }
    }
}
