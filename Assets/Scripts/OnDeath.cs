using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDeath : MonoBehaviour
{
    public GameObject player;

    private PlayerCI playerScript;

    void start()
    {
        playerScript = player.GetComponent<PlayerCI>();
    }
    void Update()
    {
        // ef dead í playerscript er sama og true reload scene
        if (player.GetComponent<PlayerCI>().dead == true)
        {
            SceneManager.LoadScene("main");
        }
            
        
    }
}
