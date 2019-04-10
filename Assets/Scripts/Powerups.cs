using System;
using System.Collections.Generic;

public static class Powerups {
    public static void PowerupCollected(Powerup powerup) {
        if(powerup.type == Powerup.Type.SlowMotion) {
            SlowMotionController.timeLeft += 2.0f; // TODO move to data classes
        }
    }
}
