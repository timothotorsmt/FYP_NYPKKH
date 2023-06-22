using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.SceneManagement {
    [System.Serializable]
    public class Scene
    {
        // A unique identifier of the current scene asset
        public SceneID SceneAssetID;
        public string SceneName;
    }

    // This enumerator gives scenes an identification code
    // Scene changes are made by calls based on the ID
    public enum SceneID
    {
        MAIN_MENU,
        HUB,
        MINIGAME,
        SETTINGS,
        SCOREBOARD
    }
}
