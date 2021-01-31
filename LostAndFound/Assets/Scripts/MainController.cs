using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public float speed = 0.1f;

    private Rigidbody2D body2D;
    private float Life = 4.0f;
    private float isGroundedRayLength = 0.1f;
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
        if (isGround() && Input.GetKeyDown(KeyCode.Space))
        {
            if (absVelY < maxVelocity.y)
            {
                forceY = jetSpeed;
            }

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
        gameManager.RemoveHeart();
    }
    public void heal()
    {
        this.Life += 0.5f;
        gameManager.AddHeart();
    }
    public void printLife()
    {
        print("Player life:" + this.Life);
    }

    public void addPoint(int amount)
    {
        gameManager.AddPoints(amount);
    }

}
