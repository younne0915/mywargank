﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class GameEntrance
    {
        public static void Entrance()
        {
            StateMachineController.getInstance().SetNextState(GameStateType.Login);
        }
    }
}
