using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public float speed = 0.1f;

    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x = position.x + speed * h;
        position.y = position.y + speed * v;
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("collision!!!!");
    }
}
