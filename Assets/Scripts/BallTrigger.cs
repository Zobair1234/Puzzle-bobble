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
        GameObject ValidParents = collision.transform.gameObject;

        if (ValidParents.gameObject.CompareTag("ValidParent") )
        {
            //gameObject.transform.parent.GetComponent<Ballbounce>().collidedObjects.Add(collision.transform.parent.gameObject);

            Ballbounce CollidedObjectwithParent = collision.gameObject.transform.parent.GetComponent<Ballbounce>();

            Ballbounce ParentofGameObject = gameObject.transform.parent.GetComponent<Ballbounce>();

            //var collidelist = gameObject.transform.parent.GetComponent<Ballbounce>().collidedObjects;

            foreach (var co in  ParentofGameObject.collidedObjects.ToList())
            {
                if (co != null && co == collision.transform.parent.gameObject)
                {
                    Included = true;
                }
            }

            if(Included == false && CollidedObjectwithParent.DoesPreExist)
            {
                
                ParentofGameObject.collidedObjects.Add(collision.transform.parent.gameObject);

                

                var bb = CollidedObjectwithParent;


               
                if (bb.ColorOfBall == ParentofGameObject.ColorOfBall)
                {

                    //Debug.Log(gameObject.transform.parent.GetComponent<Ballbounce>().ColorOfBall);

                    if (!ParentofGameObject.DoesPreExist && bb.AdjSameColor >= 2)
                    {
                        ParentofGameObject.DestroyonCollision();
                        GameManager.Instance.DestroyOccurance = true;
                        Debug.Log(GameManager.Instance.DestroyOccurance);
                    }
                    else
                    {
                        ParentofGameObject.AdjSameColor++;
                    }


                }

                ParentofGameObject.stickComplete = true;

               
                //gameObject.transform.parent.GetComponent<Ballbounce>().RegisterOperationdone = true;
            }
            else
            {
                Included = false;
            }

            ParentofGameObject.DoesPreExist = true;

            if (ParentofGameObject.DoesPreExist && CollidedObjectwithParent.AdjSameColor > 2)
            {
                Debug.Log("triggered");
                ParentofGameObject.DestroyonCollision();
                GameManager.Instance.DestroyOccurance = true;
                Debug.Log(GameManager.Instance.DestroyOccurance);
            }




            //Debug.Log(gameObject.transform.parent.GetComponent<Ballbounce>().AdjSameColor);
        }


        
    }
}

