using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Multiplayer multiplayer;
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
        }
    }
}
