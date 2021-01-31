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
    private int score = 0;
    public Text scoreText;

    List<GameObject> heartList = new List<GameObject>();
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    
    // Update is called once per frame
    void Update()
    {

    }

    private void Init() {
        for (int i = 0; heartList.Count < initHeartNum; i++)
            AddHeart();
        score = 0;
        UpdateScore();
    }

    public void AddHeart()
    {
        GameObject newHeart = Instantiate(heartTemplete, health.transform);
        
        newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector3(heartTemplete.GetComponent<RectTransform>().localPosition.x + heartList.Count * heartDistance, 0.0f);
        newHeart.SetActive(true);
        heartList.Add(newHeart);
    }
    public void RemoveHeart()
    {
        if (heartList.Count > 1) {
            GameObject h = heartList[heartList.Count - 1];
            heartList.RemoveAt(heartList.Count - 1);
            Destroy(h);
        }
    }

    public void AddPoints(int amount) {
        score += amount;
        UpdateScore();
    }

    private void UpdateScore() {

        scoreText.text = score.ToString();
    }

    public void ShowStatus() {
        status.SetActive(true);
    }

    public void EnterNextScene(string scene)
    {
        status.SetActive(false);
        SceneManager.LoadScene(scene);
    }
}
