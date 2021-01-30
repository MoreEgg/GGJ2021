using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float speed = 0.05f;
    public Vector2 ScrollSpeed = new Vector2(10.0f, 0.0f);

    GameObject backgroundGO;
    GameObject pathGO;

    // Start is called before the first frame update
    void Start()
    {
        backgroundGO = GameObject.Find("Background");
        pathGO = GameObject.Find("Path");
    }

    // Update is called once per frame
    void Update()
    {
        backgroundGO.GetComponent<SpriteRenderer>().material.SetVector("_ScrollSpeed", ScrollSpeed);
    }

    private void FixedUpdate()
    {
        Vector4 pathPos = pathGO.transform.position;
        pathPos.x -= speed;
        pathGO.transform.position = pathPos;
    }
}
