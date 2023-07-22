using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyboxCamera
{
    public class SkyboxScript : MonoBehaviour
    {
        // Name the skybox according to what u want to use
        // Should change this to serializefield
        public Material Skybox1; // test
        public Material Skybox2; // test2

        // Sets the current skybox
        void SetCurrentSkybox(Material newSkybox)
        {
            GetComponent<Skybox>().material = newSkybox;
        }

        // Start is called before the first frame update
        void Start()
        {
            SetCurrentSkybox(Skybox1);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}