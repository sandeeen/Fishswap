using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerManager : MonoBehaviour
{
    public Alteruna.Avatar avatar;
    public int myIndex;
    [SerializeField] Image image;
    private float score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject swapWarningPrefab;
    GameObject swapWarningObject;
    public int state;
    private bool hasStarted = false;
    Multiplayer multiplayer;

    private void Awake()
    {
        avatar = GetComponent<Alteruna.Avatar>();
        if(avatar.Possessor != null)
        {
            myIndex = avatar.Possessor.Index;
        }
        if(FindObjectsOfType<PlayerManager>().Length > 1)
        {
            enabled = false;
        }
        //else
        //{
        //    enabled = false;
        //    myIndex = -1;
        //}
    }



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(avatar.IsMe);
        if(!avatar.IsMe)
        {
            enabled = false;
        }
        else
        {
            myIndex = avatar.Possessor.Index;
            image = GameObject.Find("Image").GetComponent<Image>();
            scoreText = GameObject.Find("Text").GetComponent<TMP_Text>();
            multiplayer = FindObjectOfType<Multiplayer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!multiplayer.IsConnected)
        {
            return;
        }
        if (avatar.Possessor.Index == LowestUserIndex())
        {
            if (!hasStarted)
            {
                FindObjectOfType<SwapController>().StartSwapping();
                FindObjectOfType<SwapController>().StartCheckingRange();
                hasStarted = true;
            }
        }
        if (state == 0)
        {
            
            image.color = Color.red;
        }
        if(state == 1)
        {
            image.color = Color.green;
        }
        if(state == 2)
        { 
            image.color = Color.blue;
        }
    }

    private int LowestUserIndex()
    {
        List<User> users = multiplayer.GetUsers();

        int lowestIndex = 0;
        int lowestId = int.MaxValue;
        for(int i = 0; i < users.Count; i++)
        {
            if (users[i].Index < lowestId)
            {
                lowestId = users[i].Index;
                lowestIndex = i;
            }
        }
        return lowestId;
    } 



    public IEnumerator Freeze(float time)
    {
        GetComponent<FisherController>().frozen = true;
        GetComponent<FishController>().frozen = true;
        yield return new WaitForSeconds(time);
        GetComponent<FisherController>().frozen = false;
        GetComponent<FishController>().frozen = false;
    }

    private void BecomeFish()
    {
        if (!GetComponent<PlayerManager>().enabled) { return; }
        GetComponent<FisherController>().enabled = false;
        GetComponent<FishController>().enabled = true;
    }

    private void BecomeFisher()
    {
        if (!GetComponent<PlayerManager>().enabled) { return; }
        GetComponent<FisherController>().enabled = true;
        GetComponent<FishController>().enabled = false;
    }

    public void SwapState(int newState)
    {
        state = newState;
        if(state == 0)
        {
            BecomeFisher();
        }
        else
        {
            BecomeFish();
        }
    }
}
