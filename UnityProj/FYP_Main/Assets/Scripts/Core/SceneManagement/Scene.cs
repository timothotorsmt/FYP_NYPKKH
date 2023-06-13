using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Core.SceneManagement {
    [System.Serializable]
    public class Scene
    {
        // A unique identifier of the current scene asset
        public SceneID SceneAssetID;
        public SceneAsset SceneAsset;
    }
}
