using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallTrigger : MonoBehaviour
{

    public bool Included;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject InvalidParents = collision.transform.gameObject;

        if (InvalidParents.gameObject.CompareTag("InvalidParent") )
        {
            //gameObject.transform.parent.GetComponent<Ballbounce>().collidedObjects.Add(collision.transform.parent.gameObject);

            var collidelist = gameObject.transform.parent.GetComponent<Ballbounce>().collidedObjects;

            foreach (var co in collidelist.ToList())
            {
                if (co != null && co == collision.transform.parent.gameObject)
                {
                    Included = true;
                }
            }

            if(Included == false && collision.gameObject.transform.parent.GetComponent<Ballbounce>().DoesPreExist)
            {
                
                gameObject.transform.parent.GetComponent<Ballbounce>().collidedObjects.Add(collision.transform.parent.gameObject);

                

                var bb = collision.transform.parent.gameObject.GetComponent<Ballbounce>();


               
                if (bb.ColorOfBall == gameObject.transform.parent.GetComponent<Ballbounce>().ColorOfBall)
                {

                    //Debug.Log(gameObject.transform.parent.GetComponent<Ballbounce>().ColorOfBall);

                    if (!gameObject.transform.parent.GetComponent<Ballbounce>().DoesPreExist && bb.AdjSameColor >= 2)
                    {
                        gameObject.transform.parent.GetComponent<Ballbounce>().DestroyonCollision();
                    }
                    else
                    {
                        gameObject.transform.parent.GetComponent<Ballbounce>().AdjSameColor++;
                    }


                }

                gameObject.transform.parent.GetComponent<Ballbounce>().stickComplete = true;

               
                //gameObject.transform.parent.GetComponent<Ballbounce>().RegisterOperationdone = true;
            }
            else
            {
                Included = false;
            }

            gameObject.transform.parent.GetComponent<Ballbounce>().DoesPreExist = true;

            if (gameObject.transform.parent.GetComponent<Ballbounce>().DoesPreExist && collision.transform.parent.gameObject.GetComponent<Ballbounce>().AdjSameColor > 2)
            {
                Debug.Log("triggered");
                gameObject.transform.parent.GetComponent<Ballbounce>().DestroyonCollision();
            }




            //Debug.Log(gameObject.transform.parent.GetComponent<Ballbounce>().AdjSameColor);
        }


        
    }
}

