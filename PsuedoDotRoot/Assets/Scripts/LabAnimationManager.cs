using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LabAnimationManager : MonoBehaviour
{
    public Animator labAnimator;
    public string animToPlay = "Idle";
    public float animationWaitTime = 0f;

    public GameObject CreditsUI;
    public GameObject ReplayUI;

    void Start()
    {
        if (PlayerPrefs.HasKey("Level 3"))
        {
            animToPlay = "Level3Complete";
            animationWaitTime = 25f;
        }
        else if (PlayerPrefs.HasKey("Level 2"))
        {
            animToPlay = "Level2Complete";
            animationWaitTime = 15f;
        }
        else
        {
            animToPlay = "Level1Complete";
            animationWaitTime = 20f;
        }

        StartCoroutine(PlayScene());
    }

    IEnumerator PlayScene()
    {
        // Wait to start animation
        yield return new WaitForSeconds(2f);

        labAnimator.SetBool(animToPlay, true);

        // Wait for animation to finish
        yield return new WaitForSeconds(animationWaitTime);

        if (animToPlay.Equals("Level3Complete"))
        {
            CreditsUI.SetActive(true);
            LabLook lookScript = GameObject.FindObjectOfType<LabLook>();
            lookScript.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            yield return new WaitForSeconds(5f);

            ReplayUI.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }
}
