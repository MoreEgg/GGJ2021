using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouch : MonoBehaviour
{
    public float time = 0.5f;
    public float amount = 4;
    public Color normalColor = Color.white;
    public Color hitColor = new Color(0.5f, 0.5f, 0.5f);
    SpriteRenderer spriteRenderer;

    float nowTime;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nowTime >= 0.0f)
        {
            nowTime -= Time.deltaTime;
            if ((int)(nowTime / (time / amount / 2.0f)) / 2 == 0)
                spriteRenderer.color = normalColor;
            else
                spriteRenderer.color = hitColor;
        }
        else
        {
            spriteRenderer.color = normalColor;
        }
    }

    public void Hit()
    {
        nowTime = time;
    }
}
