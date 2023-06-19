using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using Common.DesignPatterns;
using DG.Tweening;

namespace Core.SceneManagement{
    public class SceneLoader : SingletonPersistent<SceneLoader>
    {
        [SerializeField] private SceneAssetList _sceneList;

        public void ChangeScene(SceneID newSceneID) 
        {
            DOTween.KillAll(); 
            
            // Check for any null cases
            if (_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Count() > 0) {
                // Load up the last instance of the sceneID
                // Should be changed to async and show the entire loading page but not now !! 
                // for i am lazy.
                SceneManager.LoadScene(_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Select(s => s.SceneName).LastOrDefault());
            }
        }
    }
}
