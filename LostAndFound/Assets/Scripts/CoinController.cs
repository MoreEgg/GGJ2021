using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : Token
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void PlayerTouched()
    {
        //make sound
        //destroy coin itself
        //if (other.gameObject.tag == "Player"){
            
        // }
        Destroy (gameObject);
    }
}
