using Core.UI.Componet;
using System;

namespace Core.UI.Scene
{
    [JiangH.GUI.Scene("MainScene")]
    public class MainScene
    {
        [JiangH.GUI.Button("Player")]
        public Player player { get; private set; }

        public MainScene()
        {
            player = new Player();
            player.name = "1111";
        }
    }
}
