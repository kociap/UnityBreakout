using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameController {
    public static void PlayerLost() {
        SceneLoader.ReloadCurrentScene();
    }
}
