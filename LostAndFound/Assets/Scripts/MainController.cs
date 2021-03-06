using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public float speed = 0.1f;

    private Rigidbody2D body2D;
    private float Life = 4.0f;
    private LayerMask layerMaskForGrounded;
    private float groundHeight = -1.8f;
    public float standingThreshold = 4f;
    public bool standing;
    public float jetSpeed = 150f;
    public Vector2 maxVelocity = new Vector2(60, 100);

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();

        if (GameObject.Find("Canvas") != null)
            gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        var absVelX = Mathf.Abs(body2D.velocity.x);
        var absVelY = Mathf.Abs(body2D.velocity.y);


        if (absVelY <= standingThreshold)
        {
            standing = true;
        }
        else
        {
            standing = false;
        }
        var forceX = 0f;
        var forceY = 0f;
        Debug.Log("absVelY:" + absVelY);
        Debug.Log("absVelX:" + absVelX);
        Transform current = transform;
        current = current.GetChild(0); 
        Debug.Log("subchild:" + current.name);
        var ani = current.GetComponent<Animator>();
        
        if (isGround() && gameManager.isKeyDown())
        {
            if (absVelY < maxVelocity.y)
            {
                forceY = jetSpeed;
            }
            //ani.SetInteger("AnimState",1);
        }
        if( standing){
            ani.SetInteger("AnimState",0);
        }else if(body2D.velocity.y>0&&!standing){
             ani.SetInteger("AnimState",1);
        }else if(body2D.velocity.y<0&&!standing){
             ani.SetInteger("AnimState",2);
        }

        body2D.AddForce(new Vector2(forceX, forceY));

        /*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x = position.x + speed * h;
        position.y = position.y + speed * v;
        transform.position = position;*/

        //finite state machine
        //isground && key press -> jump -> jump2 -> run
        //bool a = isGround();

        if (transform.localPosition.y > 3.2f) {
            transform.localPosition = new Vector3(transform.localPosition.x, 3.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("collision!!!!");
        if (other.GetComponent<Token>() != null)
        {
            other.GetComponent<Token>().PlayerTouched();
            if (other.GetComponent<Obstacle>() != null)
            {
                damage();
                print("damage!");
                printLife();
            }

            if (other.GetComponent<HealItem>() != null)
            {
                heal();
                print("heal");
                printLife();
            }

            if (other.GetComponent<Point>() != null)
            {
                addPoint(other.GetComponent<Point>().pointAmount);
                print("add point");
            }
        }


        string colliderName = other.transform.name;
        switch (colliderName) {
            case "Mid":
                GameObject.Find("LevelManager").GetComponent<LevelManager>().ReachMid();
                break;
            case "Goal":
            GameObject.Find("LevelManager").GetComponent<LevelManager>().ReachGoal();
                break;
            case "TargetChar":
                GameObject.Find("LevelManager").GetComponent<LevelManager>().TouchTarget();
                break;
        }
    }
    private bool isGround()
    {
        Vector3 position = transform.position;
        position.y = this.GetComponent<Collider2D>().bounds.min.y + 0.1f;
        //float length = isGroundedRayLength + 0.1f;
        //bool grounded = Physics2D.Raycast (position, Vector3.down, length, layerMaskForGrounded.value);

        //Debug.Log("isGround positiony:" + position.y);
        bool grounded = true;
        if (position.y >= groundHeight - 0.1f && position.y <= groundHeight + 0.1f)
        {
            Debug.Log("is on the Ground!");
        }
        else
        {
            Debug.Log("not on the ground!");
            grounded = false;
        }

        return grounded;
    }
    private void jump()
    {
        //
    }
    public void damage()
    {
        this.Life -= 0.5f;
        Transform child = transform.GetChild(0);
        child.GetComponent<Ouch>().Hit();
        if (gameManager != null)
            gameManager.RemoveHeart();
    }
    public void heal()
    {
        this.Life += 0.5f;
        if (gameManager != null)
            gameManager.AddHeart();
    }
    public void printLife()
    {
        print("Player life:" + this.Life);
    }

    public void addPoint(int amount)
    {
        if (gameManager != null)
            gameManager.AddPoints(amount);
    }

}
