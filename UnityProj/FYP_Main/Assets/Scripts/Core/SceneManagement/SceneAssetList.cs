using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.SceneManagement {
    // Create a menu thing to show
    [CreateAssetMenu(menuName = "Custom data containers/Scene list")]
    // Physical data container to store all the sceneassets
    public class SceneAssetList : ScriptableObject
    {
        // Physical scene list
        public List<Scene> SceneList;
    }
}
