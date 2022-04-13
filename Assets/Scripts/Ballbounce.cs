using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Ballbounce : MonoBehaviour
{
    // Start is called before the first frame update

    public bool IsDestroing;

    public bool DoesPreExist = false;

    public List<GameObject> collidedObjects = new List<GameObject>();

    public bool stickComplete = false;

    private Rigidbody2D rb;

    public BallColor ColorOfBall;

    Vector3 lastVelocity;

    public int AdjSameColor;

    public bool RegisterOperationdone;

    public bool hasVisited =false;

    public bool IsRoot;

    public bool RegisterForDestruction;

    public float posX;

    public float posY;

    





    private void Start()
    {

        posX = transform.position.x;
        posY = transform.position.y;

        IsDestroing = false;

      //  DoesPreExist = false;
        AdjSameColor = 1;

        rb = GetComponent<Rigidbody2D>();
        stickComplete = false;

        //Debug.Log("J " + AdjSameColor);
        //var templistcolor = collidedObjects.Where(co => co.GetComponent<Ballbounce>().ColorOfBall == this.ColorOfBall).ToList().Count;

    }

    // Update is called once per frame
    void Update()
    {

        if (DoesPreExist)
        { 
            //transform.position = new Vector3(posX, posY, 0); 
        }
        lastVelocity = rb.velocity;

        //gameObject.transform.position.y>3f 

        if (gameObject.transform.position.y < 0.5f && DoesPreExist)
        {
            Debug.Log("game over");

            GameManager.Instance.gameoverScreen.SetActive(true);


            // Debug.Log(gameObject.transform.position.y);
        }



    }

    public void DestroyonCollision()
    {
        // Debug.Log(gameObject.name);
       // Debug.Log("working");


        IsDestroing = true;

        foreach (var co in collidedObjects.ToList())
        {
            if (co != null)
            {

                

                var bb = co.GetComponent<Ballbounce>();

                if (!bb.IsDestroing && bb.ColorOfBall == this.ColorOfBall)
                {
                    co.GetComponent<Ballbounce>().DestroyonCollision();

                    

                }
            }
        }

        if(IsRoot)
        {
            GameManager.Instance.RootHasBeenDestroied = true;
        }

        RegisterForDestruction = true;

        GameManager.Instance.IsDestroied = true;

        //Debug.Log("Name                    " + gameObject.name);

        GameManager.Instance.ToDestroy.Add(gameObject);
    }






    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BounceWall"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);


        }

        //call when collided with another ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            
        }
        //collision with the celling
        if (collision.gameObject.CompareTag("StickWall"))
        {

            //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //gameObject.GetComponent<Rigidbody2D>().mass = 100f;
            //collision.gameObject.GetComponent<Ballbounce>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;



            GameManager.Instance.isstuck = true;

            GameManager.Instance.Roots.Add(gameObject);

            stickComplete = true;

            DoesPreExist = true;

            IsRoot = true;
            //Debug.Log("Collided");
        }

    }
}
