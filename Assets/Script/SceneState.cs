using System;
using UnityEngine;

namespace SceneState
{
    [Serializable]
    public enum State
    {
        Pause = 0,
        Game,
        Slow,
        Over,
        Clear
    }
}
