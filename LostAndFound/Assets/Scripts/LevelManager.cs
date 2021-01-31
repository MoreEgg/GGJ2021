using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float waitBeforeStart = 2.0f;
    public float speed = 0.05f;
    float nowSpeed = 0.0f;
    GameObject backgroundGO;
    GameObject pathGO;

    public GameObject openGO;
    public GameObject endGO;
    public GameObject targetGO;

    public float targetMoveBackSpeed = 0.05f;


    GameObject leftBackgroundGO, rightBackgroundGO;
    float bgWidth;
    float nowTime;

    public enum State
    {
        Open,
        StartRun,
        Running,
        End
    }

    public State nowState = State.Open;

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

        nowTime = waitBeforeStart;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        switch (nowState)
        {
            case State.Open:
                nowTime -= Time.deltaTime;
                if (nowTime <= 0.0f)
                    nowState = State.Running;
                break;
            case State.Running:
                if (nowSpeed < speed)
                    nowSpeed += speed * (Time.deltaTime / 2.0f);
                else
                    nowSpeed = speed;

                updateTarget();
                break;
            case State.End:
                nowSpeed = 0;
                break;
        }

        updateScene();
    }

    private void updateScene()
    {
        Vector4 bgPos = backgroundGO.transform.position;
        bgPos.x -= nowSpeed;
        if (bgPos.x + bgWidth < 0.0f)
        {
            bgPos.x += bgWidth;
        }
        backgroundGO.transform.position = bgPos;

        Vector4 pathPos = pathGO.transform.position;
        pathPos.x -= nowSpeed;
        pathGO.transform.position = pathPos;
    }

    bool isMid = false, isGoal = false;
    private void updateTarget()
    {
        if ((isMid && targetGO.transform.position.x > 7.9f) || isGoal)
        {
            Vector4 targetPos = targetGO.transform.position;
            targetPos.x -= targetMoveBackSpeed;
            targetGO.transform.position = targetPos;

        }

    }

    public void ReachMid()
    {
        isMid = true;
    }

    public void ReachGoal()
    {
        isGoal = true;
    }

    public void TouchTarget()
    {
        nowState = State.End;
    }
}
