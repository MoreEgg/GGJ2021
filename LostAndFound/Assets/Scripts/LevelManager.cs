using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float speed = 0.05f;
    public Vector2 ScrollSpeed = new Vector2(10.0f, 0.0f);

    GameObject backgroundGO;
    GameObject pathGO;

    GameObject leftBackgroundGO, rightBackgroundGO;
    float bgWidth;

    // Start is called before the first frame update
    void Start()
    {
        backgroundGO = GameObject.Find("Background");
        bgWidth = backgroundGO.GetComponent<SpriteRenderer>().bounds.size.x;
        leftBackgroundGO = GameObject.Instantiate(backgroundGO, backgroundGO.transform);
        rightBackgroundGO = GameObject.Instantiate(leftBackgroundGO, backgroundGO.transform);
        leftBackgroundGO.transform.position = backgroundGO.transform.position - new Vector3(bgWidth, 0f);
        rightBackgroundGO.transform.position = backgroundGO.transform.position + new Vector3(bgWidth, 0f);

        pathGO = GameObject.Find("Path");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector4 pathPos = pathGO.transform.position;
        pathPos.x -= speed;
        pathGO.transform.position = pathPos;

        Vector4 bgPos = backgroundGO.transform.position;
        bgPos.x -= speed;
        if (bgPos.x + bgWidth < 0.0f) {
            bgPos.x += bgWidth;
        }
        backgroundGO.transform.position = bgPos;

    }
}
