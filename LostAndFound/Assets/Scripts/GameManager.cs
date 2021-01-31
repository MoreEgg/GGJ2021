using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public int heartNum = 4;
    public float heartDistance = 110f;
    public GameObject health;
    public GameObject heartTemplete;

    List<GameObject> heartList = new List<GameObject>();
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < heartNum; i++)
            AddHeart();
    }

    // Update is called once per frame
    void Update()
    {

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
    }
}
