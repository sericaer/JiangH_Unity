using System;

namespace JiangH.GUI
{
    public class Text : Attribute
    {
        public string name { get; private set; }

        public Text(string name)
        {
            this.name = name;
        }
    }
}
