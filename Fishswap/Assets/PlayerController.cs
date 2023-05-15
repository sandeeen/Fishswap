using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Alteruna.Avatar avatar;
   
    private void Awake()
    {
        avatar = GetComponent<Alteruna.Avatar>();

    }

    void Start()
    {

        if (!avatar.IsMe)
        {
            enabled = false;
        }

    }


}