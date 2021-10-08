using System;

namespace JiangH.GUI
{
    public class Button : Attribute
    {
        public string name { get; private set; }

        public Button(string name)
        {
            this.name = name;
        }
    }
}
