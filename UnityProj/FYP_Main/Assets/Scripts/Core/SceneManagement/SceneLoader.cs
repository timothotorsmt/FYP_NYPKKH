using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Common.DesignPatterns;
using DG.Tweening;
using UnityEngine.Events;

namespace Core.SceneManagement
{
    // The class to manage and change different scenes
    public class SceneLoader : SingletonPersistent<SceneLoader>
    {
        #region variables

        [SerializeField] private SceneAssetList _sceneList;
        public UnityEvent _sceneChangeAction;
        private SceneID _currentScene;

        #endregion 

        private void Start()
        {
            _currentScene = _sceneList.SceneList.Where(x => x.SceneName == SceneManager.GetActiveScene().name).Select(x => x.SceneAssetID).First();
        }

        
        public void ChangeScene(SceneID newSceneID, bool loadToLoadingScreen = true)
        {

            DOTween.CompleteAll();

            // Check for any null cases
            if (_sceneList.SceneList.Where(s => s.SceneAssetID == newSceneID).Count() > 0)
            {
                
                _currentScene = newSceneID;
                // If you want the loading screen to show up just set the 2nd parameter to true
                // If checked as true for loadToLoadingScreen, load the loading screen 
                if (loadToLoadingScreen)
                {
                    StartCoroutine(LoadSceneCoroutine(newSceneID));
                }
                else
                {
                    SceneManager.LoadScene(GetSceneName(newSceneID));
                }
                // Save the data if the scene changes to a hub
                if (newSceneID == SceneID.HUB_NORMAL || 
                    newSceneID == SceneID.HUB_WONDERLAND || 
                    newSceneID == SceneID.LINES ||
                    newSceneID == SceneID.STOMA)
                {
                    PlayerDataSaver.playerData.currentSceneID = newSceneID;
                    PlayerDataSaver.SaveCurrentData();
                }
            }
        }

        public SceneID GetSceneID()
        {
            return _currentScene;
        }

        private IEnumerator LoadSceneCoroutine(SceneID newSceneID)
        {
            // Loads the Loading Screen 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GetSceneName(SceneID.LOADING));
            
            // Wait for 1 second minimum (showing the loading screen)
            yield return new WaitForSeconds(1.0f);

            // If the current load for the scene is not done
            if (!asyncLoad.isDone) 
            {
                // wait until the asynchronous loading is done
                yield return new WaitUntil(() => asyncLoad.isDone);
                
            }

            // Once complete
            // Loads the expected scene
            DOTween.KillAll();
            SceneManager.LoadScene(GetSceneName(newSceneID));

            _sceneChangeAction.Invoke();
            _sceneChangeAction.RemoveAllListeners();
        }

        // Returns the string name based on the given string ID
        private string GetSceneName(SceneID sceneToSearch)
        {
            return _sceneList.SceneList.Where(s => s.SceneAssetID == sceneToSearch).Select(s => s.SceneName).LastOrDefault();
        }
    }
}