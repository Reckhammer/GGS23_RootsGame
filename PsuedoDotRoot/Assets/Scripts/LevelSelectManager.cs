using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{

    [SerializeField] private int levelHover = 1;
    //private Transform transform;

    // Start is called before the first frame update
    void Awake()
    {
     //   transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
                transform.position = new Vector2 (-1.5f + (levelHover - 1) * 2f,2f);

                if (levelHover > 3) { levelHover = 1; }
                if (levelHover < 1) { levelHover = 3; }

        if (Input.GetKeyDown(KeyCode.A))
        {
            levelHover -= 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            levelHover += 1;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (levelHover == 1){}
            if (levelHover == 2){}
            if (levelHover == 3){}
        }

    }
}
