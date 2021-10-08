using System;

namespace JiangH.GUI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Scene : Attribute
    {
        public string name { get; private set; }

        public Scene(string name)
        {
            this.name = name;
        }
    }
}
