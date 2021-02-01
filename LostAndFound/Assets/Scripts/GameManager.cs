using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int initHeartNum = 4;
    public float heartDistance = 110f;
    public GameObject status;
    public GameObject health;
    public GameObject heartTemplete;
    public GameObject negative;
    public Text scoreText;

    public int score = 0;
    public int nowHeart;
    List<GameObject> heartList = new List<GameObject>();

    private static GameManager gameManagerInstance;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (gameManagerInstance == null)
            gameManagerInstance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void Init()
    {
        nowHeart = initHeartNum;
        UpdateHeart();

        score = 0;
        UpdateScore();
    }

    public void AddHeart()
    {
        nowHeart++;
        UpdateHeart();
    }
    public void RemoveHeart()
    {
        nowHeart--;
        UpdateHeart();
    }

    private void UpdateHeart()
    {
        negative.SetActive(false);
        for (int i = 0; i < heartList.Count; i++)
            Destroy(heartList[i]);
        heartList.Clear();

        if (nowHeart >= 0)
        {
            for (int i = 0; i < nowHeart; i++)
            {
                GameObject newHeart = Instantiate(heartTemplete, health.transform);

                newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector3(heartTemplete.GetComponent<RectTransform>().localPosition.x + heartList.Count * heartDistance, 0.0f);
                newHeart.SetActive(true);
                heartList.Add(newHeart);
            }
        }
        else
        {
            negative.SetActive(true);

            for (int i = 0; i < (int)Mathf.Abs(nowHeart); i++)
            {
                GameObject newHeart = Instantiate(heartTemplete, health.transform);

                newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector3(heartTemplete.GetComponent<RectTransform>().localPosition.x + heartList.Count * heartDistance + 80f, 0.0f);
                newHeart.SetActive(true);
                heartList.Add(newHeart);
            }
        }
    }

    public void AddPoints(int amount)
    {
        score += amount;
        UpdateScore();
    }

    private void UpdateScore()
    {

        scoreText.text = score.ToString();
    }

    public void ShowStatus()
    {
        status.SetActive(true);
    }

    public void HideStatus()
    {
        status.SetActive(false);
    }

    public void EnterNextScene(string scene)
    {
        status.SetActive(false);
        SceneManager.LoadScene(scene);
    }

    public bool isKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return true;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }
}
