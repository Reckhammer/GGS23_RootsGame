using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerController playerRef;

    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef.PlayerDied += OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        print("Starting Death Sequence");

        // Reset level parameters
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
