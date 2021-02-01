using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creddit : MonoBehaviour
{
    GameManager gameManager;

    public Text point;
    public Text heart;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        point.text = gameManager.score.ToString();
        heart.text = gameManager.nowHeart.ToString();
    }

    public bool isEnd = false;
    // Update is called once per frame
    void Update()
    {
        if (isEnd && gameManager.isKeyDown())
            gameManager.EnterNextScene("Title");
    }
}
