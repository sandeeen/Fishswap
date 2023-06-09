using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlescreenManager : MonoBehaviour
{

    AudioSource audioSource;

    [SerializeField] Vector3 fishSpawnPoint;
    [SerializeField] Vector3 walterSpawnPoint;
    [SerializeField] Vector3 contrainerSpawnPoint;
    [SerializeField] Vector3 wellSpawnPoint;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip beyondTheSea;

    [SerializeField] GameObject spaceFish;
    [SerializeField] GameObject spaceWalter;
    [SerializeField] GameObject spaceContainer;
    [SerializeField] GameObject spaceWell;

    public bool canSpawnFish = true;
    public bool canSpawnWalter = true;

    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject TitleButton;

    private bool hasRevealed = false;
    float timer;
    float timeToSpawn = 30f;

    void Start()
    {
        SoundManager.Instance.PlayAudio(menuMusic, 1f);
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TitleReveal());
    }



    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && hasRevealed)
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && hasRevealed)
        {
            QuitGame();
        }

        if (timer > timeToSpawn && hasRevealed)
        {
            timer = 0f;
            StartCoroutine(SpawnStuff());
        }

    }

    void SpawnFish()
    {
        Instantiate(spaceFish, fishSpawnPoint, Quaternion.identity);
    }

    void SpawnWalter()
    {
        Instantiate(spaceWalter, walterSpawnPoint, Quaternion.identity);
    }

    void SpawnContainer()
    {
        Instantiate(spaceContainer, contrainerSpawnPoint, Quaternion.identity);
    }

    void SpawnWell()
    {
        Instantiate(spaceWell, wellSpawnPoint, Quaternion.identity);
    }

    public void StartGame()
    {
        Debug.Log("Start the game Walter");
    }

    public void QuitGame()
    {
        Application.Quit();
        
    }

    private IEnumerator TitleReveal()
    {
        yield return new WaitForSeconds(34f);
        hasRevealed = true;
        StartButton.SetActive(true);
        TitleButton.SetActive(true);
        yield return new WaitForSeconds(29f);
        audioSource.PlayOneShot(beyondTheSea);
        audioSource.loop = true;
    }

    private IEnumerator SpawnStuff()
    {
        SpawnFish();
        SpawnWalter();

        yield return new WaitForSeconds(5f);

        SpawnContainer();
        SpawnWell();
    }
}
