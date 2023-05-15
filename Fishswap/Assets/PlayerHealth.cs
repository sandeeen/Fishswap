using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : AttributesSync
{
    [SynchronizableField] public int health = 3;

    private Alteruna.Avatar avatar;

    private void Awake()
    {
        avatar = GetComponent<Alteruna.Avatar>();

    }

    private void Start()
    {
        if (!avatar.IsMe)
        {
            enabled = false;
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            health -= 1;
        }

     if (Input.GetKeyDown(KeyCode.M))
        {
            InvokeRemoteMethod("Test",UserId.AllInclusive, "Hello wrodl!");
        }
    }


[SynchronizableMethod]
    private void Test(string testString)
    {
        Debug.Log(testString);
    }


}
