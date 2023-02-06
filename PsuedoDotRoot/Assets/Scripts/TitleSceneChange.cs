using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneChange());   
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("LevelSelect");
    }
}
