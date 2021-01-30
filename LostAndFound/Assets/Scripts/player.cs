using System.Collections;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 150f;
    public Vector2 maxVelocity = new Vector2 (60,100);
    public float jetSpeed = 50f;
    public bool standing;
    public float standingThreshold = 4f;
    private Rigidbody2D body2D;
    private SpriteRenderer renderer2D;

  
   


    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D> ();
        renderer2D = GetComponent<SpriteRenderer> ();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        var absVelX = Mathf.Abs(body2D.velocity.x);
        var absVelY = Mathf.Abs(body2D.velocity.y);
        
        
        if( absVelY <= standingThreshold){
            standing = true;
        }
        else{
            standing = false;
        }
        var forceX = 0f;
        var forceY = 0f;
        
        if (Input.GetKey ("up")){
            if(absVelY<maxVelocity.y){
                forceY = jetSpeed;
            }

        }
        

        body2D.AddForce(new Vector2(forceX, forceY));

    }

    
}
