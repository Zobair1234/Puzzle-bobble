using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Ballbounce : MonoBehaviour
{
    // Start is called before the first frame update

    public bool IsDestroing;

    public bool DoesPreExist;

    public List<GameObject> collidedObjects = new List<GameObject>();
    
    public bool stickComplete = false;

    private Rigidbody2D rb;

    public BallColor ColorOfBall;

    Vector3 lastVelocity;

    public int AdjSameColor;

    public bool RegisterOperationdone;

   

     
    private void Start()
    {

        IsDestroing = false;

        DoesPreExist = true;
        AdjSameColor = 1;
         
        rb = GetComponent<Rigidbody2D>();
        stickComplete = false;

        //Debug.Log("J " + AdjSameColor);
        //var templistcolor = collidedObjects.Where(co => co.GetComponent<Ballbounce>().ColorOfBall == this.ColorOfBall).ToList().Count;

    }

    // Update is called once per frame
    void Update()
    {
        
        lastVelocity = rb.velocity;


        
    }

    public void DestroyonCollision()
    {
       // Debug.Log(gameObject.name);
        


        IsDestroing = true;

        foreach (var co in collidedObjects.ToList())
        {
            if (co != null) { 
            var bb = co.GetComponent<Ballbounce>();

            if (co != null && !bb.IsDestroing && bb.ColorOfBall == this.ColorOfBall )
            { 
                co.GetComponent<Ballbounce>().DestroyonCollision(); 
                
            }
            }
        }
        GameManager.Instance.IsDestroied = true;

        Destroy(gameObject);
    }    






    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("StickWall") && !collision.gameObject.CompareTag("Ball") && !stickComplete) {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);

             
        }
        
        //call when collided with another ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            

            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            if (collision.gameObject.transform.childCount > 0)
            { collision.gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; }

            GameManager.Instance.isstuck = true;

            


            //collidedObjects.Add(collision.gameObject);

          /*  var bb = collision.gameObject.GetComponent<Ballbounce>();

            if (bb.ColorOfBall == this.ColorOfBall)
            {
                

                if(!DoesPreExist && bb.AdjSameColor>=2 )
                {
                    DestroyonCollision();
                }
                else
                {
                    AdjSameColor++;
                }


            }
*/
//            DoesPreExist = true;

            //gameObject.transform.GetChild(0).gameObject.SetActive(true);

            //collision.gameObject.GetComponent<Ballbounce>().enabled = false;
           


            


            //Debug.Log("Collided");
        }

        if (collision.gameObject.CompareTag("StickWall"))
        {
            //collision.gameObject.GetComponent<Ballbounce>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            GameManager.Instance.isstuck = true;

            stickComplete = true;

            DoesPreExist = true;
            //Debug.Log("Collided");
        }
        
    }
}
