using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouch : MonoBehaviour
{
    public float time = 0.5f;
    public int amount = 4;
    public Color normalColor = Color.white;
    public Color hitColor = new Color(0.5f, 0.0f, 0.0f);
    SpriteRenderer spriteRenderer;
    private int flashDuration = 25;
    private int currAmount;
    float nowTime;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currAmount = amount * flashDuration;
    }
    // Update is called once per frame
    void Update()
    {
        /*
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
        }*/
        currAmount++;
        if(currAmount < amount * flashDuration){
            int stage =  currAmount/flashDuration;
            if(stage % 2 == 0){
                spriteRenderer.color = hitColor;
            }else{
                spriteRenderer.color = normalColor;
            }
        }else{
            currAmount = amount * flashDuration;
            spriteRenderer.color = normalColor;
        }
    }

    public void Hit()
    {
        currAmount = 0;
        nowTime = time;
    }
}
