using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Token
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        print("collision obstacle!!!!");
        //other.GetComponent<Token>().PlayerTouched();
//        if(other.gameObject.name == "Player"){
//            other.gameObject
//        }
    }
    public override void PlayerTouched()
    {
       gameObject.GetComponent<AudioSource>().enabled = true;
       
    }
}
