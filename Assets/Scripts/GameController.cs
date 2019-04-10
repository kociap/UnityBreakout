using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class GameController {
    public static bool inputEnabled = true;
    public static bool loadingScene = false;

    public static void PlayerLost() {
        SceneLoader.ReloadCurrentScene();
    }
}
