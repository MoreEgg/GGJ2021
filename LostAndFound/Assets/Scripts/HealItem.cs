using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Token
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void PlayerTouched(){
        gameObject.GetComponent<AudioSource>().enabled = true;
       gameObject.GetComponent<SpriteRenderer>().enabled = false;
       Destroy (gameObject, 1.0f);
    }
}
