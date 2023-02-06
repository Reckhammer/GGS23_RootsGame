using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerController playerRef;
    public KeyItem keyItem;
    public GameObject lvlCompleteMsg;

    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        keyItem = GameObject.FindObjectOfType<KeyItem>();
        lvlCompleteMsg = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef.PlayerDied += OnPlayerDeath;
        keyItem.KeyItemCollected += OnLevelComplete;
    }

    void OnPlayerDeath()
    {
        StartCoroutine(DeathSequence());
    }

    void OnLevelComplete()
    {
        StartCoroutine(LevelCompleteSequence());
    }

    IEnumerator DeathSequence()
    {
        print("Starting Death Sequence");

        // Reset level parameters
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    IEnumerator LevelCompleteSequence()
    {
        print("Starting LevelCompleteSequence");
        lvlCompleteMsg.GetComponent<TextMeshProUGUI>().text = keyItem.fileName + " Collected";
        lvlCompleteMsg.SetActive(true);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        
        //PlayerPrefs.HasKey("Level 1")
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Lab");
    }
}
