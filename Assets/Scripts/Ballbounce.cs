using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballbounce : MonoBehaviour
{
    // Start is called before the first frame update

    
    public bool stickComplete = false;

    private Rigidbody2D rb;

    Vector3 lastVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stickComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("StickWall") && !collision.gameObject.CompareTag("Ball")) {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);

            Debug.Log("collided");
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Ballbounce>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            GameManager.Instance.isstuck = true;

            stickComplete = true;
            //Debug.Log("Collided");
        }

        if (collision.gameObject.CompareTag("StickWall"))
        {
            //collision.gameObject.GetComponent<Ballbounce>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            GameManager.Instance.isstuck = true;

            stickComplete = true;
            Debug.Log("Collided");
        }
        
    }
}
