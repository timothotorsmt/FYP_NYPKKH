using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxScript : MonoBehaviour
{
    // Name the skybox according to what u want to use
    public Material skybox1; // test
    public Material skybox2; // test2

    // Sets the current skybox
    void SetCurrentSkybox(Material newSkybox)
    {
        GetComponent<Skybox>().material = newSkybox;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentSkybox(skybox1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}