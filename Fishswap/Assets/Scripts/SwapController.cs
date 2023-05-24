using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Alteruna;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class SwapController : AttributesSync
{
    Dictionary<int, PlayerManager> playerManagers;
    [SerializeField] GameObject swapWarningPrefab;
    private GameObject swapWarningObject;
    [SerializeField] Canvas canvas;
    [SerializeField] float warningTimer;
    float timeUntilNextSwap;
    [SerializeField] float medianPower;
    [SerializeField] float deviationPower;
    [SerializeField] float min;
    [SerializeField] float basePower;
    [SerializeField] float multiplier;
    System.Random random;
    List<float> values;
    [SerializeField] AnimationCurve curve;
    [SerializeField] int timesToRunPerFrame;
    [SerializeField] float spacing;
    [SerializeField] Image image;
    int currentFisherIndex;
    [SerializeField] Transform fishSpawn;
    [SerializeField] Transform bobberSpawn;
    [SerializeField] GameObject fish;
    [SerializeField] GameObject bobber;
    [SerializeField] float catchRange;
    bool paused = false;
    bool state = false;
    int score;
    PlayerManager playerManager;
    [SerializeField] ScoreBoard scoreBoard;

    Coroutine swapPlayers;

    // Start is called before the first frame update
    void Start()
    {
        playerManagers = new Dictionary<int, PlayerManager>();
    }

    public void StartSwapping()
    {
        values = new List<float>();
        random = new System.Random();
        if(swapPlayers != null)
        {
            StopCoroutine(swapPlayers);
        }
        swapPlayers = StartCoroutine(SwapPlayers());
        Debug.LogError("THIS HAS RUN, IF IT HAS RAN TWICE SOMETHING IS WRONG");
    }

    public void StartCheckingRange()
    {
        StartCoroutine(CheckRange());
    }

    private IEnumerator CheckRange()
    {
        while (true)
        {
            yield return null;
            CheckCatchRange();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CheckCatchRange()
    {
        if(Vector3.Distance(fish.transform.position,bobber.transform.position) < catchRange)
        {
            Catch();
        }
    }

    private void Catch()
    {
        InvokeRemoteMethod("CatchSync", UserId.AllInclusive);
        InvokeRemoteMethod("AddScore", UserId.AllInclusive, currentFisherIndex);
    }

    private IEnumerator SwapPlayers()
    {
        yield return new WaitForSeconds((float)GenerateRandomValue());
        InvokeRemoteMethod("SwapWarning", UserId.AllInclusive);
        yield return new WaitForSeconds(warningTimer);
        Debug.Log("Swap!");
        state = !state;
        int stateInt = state ? 1 : 0;
        List<User> users = FindObjectOfType<Multiplayer>().GetUsers();
        for (int i = 0; i < users.Count; i++)
        {
            InvokeRemoteMethod("Swap", users[i].Index, (stateInt + i) % 2, users[i].Index); 
            if((stateInt + i) % 2 == 0)
            {
                currentFisherIndex = users[i].Index;
            }
        }
        InvokeRemoteMethod("StopSwapWarning", UserId.AllInclusive);
        StartCoroutine(SwapPlayers());
    }

    [SynchronizableMethod]
    private void CatchSync()
    {
        PlayerManager[] playermanagers = FindObjectsOfType<PlayerManager>();
        for(int i = 0; i < playermanagers.Length; i++)
        {
            if (playermanagers[i].enabled)
            {
                StartCoroutine(playermanagers[i].Freeze(0.25f));
            }
            else
            {
                Debug.Log("Disabled");
            }
        }
        fish.transform.position = fishSpawn.position;
        bobber.transform.position = bobberSpawn.position;
        StartCoroutine(CatchDelayTeleport());
    }

    IEnumerator CatchDelayTeleport() 
    {
        yield return null;
        fish.transform.position = fishSpawn.position;
        bobber.transform.position = bobberSpawn.position;
    }

    [SynchronizableMethod]
    private void AddScore(int index)
    {
        if(index == 0)
        {
            scoreBoard.IncreasePlayer1Score(1);
        }
        else
        {
            scoreBoard.IncreasePlayer2Score(1);
        }
    }

    public double GenerateRandomValue()
    {
        double u1 = 1.0 - random.NextDouble();
        double u2 = 1.0 - random.NextDouble();

        // Use Box-Muller transform to generate normally distributed value
        double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        double value = Mathf.Pow(basePower, (float)z) * multiplier + min;
        return value;
    }

    private void LoadAllPlayers()
    {
        PlayerManager[] playerManagersFound = FindObjectsOfType<PlayerManager>();
        playerManagers.Clear();
        for(int i = 0; i < playerManagersFound.Length; i++)
        {
            if(!playerManagers.ContainsKey(playerManagersFound[i].myIndex))
            {
                playerManagers.Add(playerManagersFound[i].myIndex, playerManagersFound[i]);
            }
        }
    }

    [SynchronizableMethod]
    public void Swap(int state, int index)
    {
        PlayerManager[] playerManagersFound = FindObjectsOfType<PlayerManager>();
        for(int i = 0; i < playerManagersFound.Length; i++)
        {
            playerManagersFound[i].SwapState(state);
        }
    }

    [SynchronizableMethod]
    public void SwapWarning()
    {
        if (swapWarningObject == null)
        {
            swapWarningObject = Instantiate(swapWarningPrefab, canvas.transform);
        }
        Debug.Log("swap warning");
        swapWarningObject.SetActive(true);
    }

    [SynchronizableMethod]
    public void StopSwapWarning()
    {
        Debug.Log("Stop warning");
        if (swapWarningObject == null)
        {
            swapWarningObject = Instantiate(swapWarningPrefab, canvas.transform);
        }
        swapWarningObject.SetActive(false);
    }


    [ContextMenu("Create graph")]
    public void CreateGraph()
    {
        while(curve.keys.Length > 0)
        {
            curve.RemoveKey(0);
        }

        float[] buckets = new float[(int)Mathf.Ceil(100 / spacing)];

        for(int i = 0; i < values.Count; i++)
        {
            buckets[Mathf.FloorToInt(values[i] / spacing)]++;
        }

        for(int i = 0; i < buckets.Length; i++)
        {
            curve.AddKey(i * spacing, buckets[i] / (values.Count * 0.01f));
        }

        Debug.Log("AVERAGE: " + values.Average());
        values.Sort();
        Debug.Log("MEDIAN: " + values[values.Count/2]);
    }
}
