using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Common.DesignPatterns;
using DG.Tweening;

namespace Core.SceneManagement
{
    public class SceneLoader : SingletonPersistent<SceneLoader>
    {
        [SerializeField] private SceneAssetList _sceneList;
        // Loading Bar
        //[SerializeField] private Slider _loadingBar;

        public void ChangeScene(SceneID newSceneID, bool loadToLoadingScreen = false)
        {
            DOTween.KillAll();

            // Check for any null cases
            if (_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Count() > 0)
            {
                // Load up the last instance of the sceneID
                // Should be changed to async and show the entire loading page but not now !! 
                // for i am lazy.
                //SceneManager.LoadScene(_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Select(s => s.SceneName).LastOrDefault());
                
                // If you want the loading screen to show up just set the 2nd parameter to true
                // If checked as true for loadToLoadingScreen, load the loading screen 
                if (loadToLoadingScreen)
                {
                    StartCoroutine(LoadSceneCoroutine(newSceneID));
                }
                else
                {
                    SceneManager.LoadScene(_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Select(s => s.SceneName).LastOrDefault());
                }

            }
        }
        private IEnumerator LoadSceneCoroutine(SceneID newSceneID)
        {
            // Loads the Loading Screen 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)SceneManagement.SceneID.LOADING);
            // Actions done while in loading screen
            //while (!asyncLoad.isDone)
            //{
            //    LoadingSceneSprite.Instance.RotateLoop();
            //    yield return null;
            //}
            yield return new WaitForSeconds(1.0f);

            // Loads the expected scene
            DOTween.KillAll();
            SceneManager.LoadScene(_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Select(s => s.SceneName).LastOrDefault());
        }
    }
}