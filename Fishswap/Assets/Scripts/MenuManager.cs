using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Multiplayer multiplayer;
    [SerializeField] GameObject menuManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int userCount = multiplayer.GetUsers().Count;
        if(userCount == 2)
        {
            menuManager.GetComponent<Canvas>().enabled = false;
        }
    }
}
