using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float waitBeforeStart = 2.0f;
    public float speed = 0.2f;
    float nowSpeed = 0.0f;

    public float midSpeed = 0.4f;

    GameObject backgroundGO;
    GameObject pathGO;

    public GameObject openGO;
    public GameObject startGO;
    public GameObject endGO;
    public GameObject targetGO;

    public float targetMoveBackSpeed = 0.05f;


    GameObject leftBackgroundGO, rightBackgroundGO;
    float bgWidth;
    float nowTime;

    public float endingTime = 3.0f;
    public string nextScene;

    GameManager gameManager;

    public enum State
    {
        Open,
        Start,
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

        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();

        nowTime = waitBeforeStart;
        startGO.SetActive(false);
        endGO.SetActive(false);
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
                if (openGO.activeSelf == false)
                    openGO.SetActive(true);

                nowTime -= Time.deltaTime;
                if (nowTime <= 0.0f)
                {
                    nowTime = waitBeforeStart;
                    nowState = State.Start;
                    openGO.SetActive(false);
                    if (gameManager != null)
                        gameManager.ShowStatus();
                }
                break;

            case State.Start:
                if (startGO.activeSelf == false)
                    startGO.SetActive(true);

                nowTime -= Time.deltaTime;
                if (nowTime <= 0.0f)
                    nowState = State.Running;
                break;

            case State.Running:
                if (!isMid)
                {
                    if (nowSpeed < speed)
                        nowSpeed += speed * (Time.deltaTime / 2.0f);
                    else
                        nowSpeed = speed;
                }
                else
                {
                    if (nowSpeed < midSpeed)
                        nowSpeed += (midSpeed - speed) * (Time.deltaTime / 2.0f);
                    else
                        nowSpeed = midSpeed;
                }

                updateTarget();
                break;

            case State.End:
                if (endGO.activeSelf == false)
                    endGO.SetActive(true);
                nowSpeed = 0;
                nowTime -= Time.deltaTime;
                //Debug.LogError(nowTime);
                //Debug.LogError(Input.GetKeyDown(KeyCode.Space));
                if (nowTime <= 0.0f && gameManager.isKeyDown())
                    if (gameManager != null)
                        gameManager.EnterNextScene(nextScene);
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
        if (nowState != State.End)
            nowTime = endingTime;
        nowState = State.End;
        if (gameManager != null)
            gameManager.HideStatus();
    }
}
