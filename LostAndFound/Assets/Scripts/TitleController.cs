using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public string nextScene;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        gameManager.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isKeyDown())
            gameManager.EnterNextScene(nextScene);
    }
}
