using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : Token
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
       //make coin sound.
//       SoundPlayer player = new SoundPlayer("../Zero Rare/Retro Sound Effects/Audio/coin_15.wav");
//       player.Play();
//       player.Stop();
       gameObject.GetComponent<AudioSource>().enabled = true;
       gameObject.GetComponent<SpriteRenderer>().enabled = false;
       Destroy (gameObject, 0.6f);
    }
}
