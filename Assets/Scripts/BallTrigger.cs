using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class BallTrigger : MonoBehaviour
{


    public bool Included;
    int TriggerCount = 0;
    int Count = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Debug.Log("triggered");

        GameObject ValidParents = collision.transform.gameObject;

        if (ValidParents.gameObject.CompareTag("ValidParent"))
        {

            //gameObject.transform.parent.GetComponent<Ballbounce>().collidedObjects.Add(collision.transform.parent.gameObject);


            Ballbounce CollidedObjectwithParent = collision.gameObject.transform.parent.GetComponent<Ballbounce>();

            Ballbounce ParentofGameObject = gameObject.transform.parent.GetComponent<Ballbounce>();

            ParentofGameObject.stickComplete = true;

            GameManager.Instance.isstuck = true;

          //  gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
           // gameObject.transform.parent.GetComponent<Rigidbody2D>().mass = 100000f ;

            



            //gameObject.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().angularVelocity = 0;


            //gameObject.transform.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;



            if (Count == 0)
            { 
                StartCoroutine(ChangeBodyType());
            }

            //gameObject.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;




            //var collidelist = gameObject.transform.parent.GetComponent<Ballbounce>().collidedObjects;

            foreach (var co in ParentofGameObject.collidedObjects)
            {
                if (co != null && co == collision.transform.parent.gameObject)
                {
                    Included = true;
                }
            }





            if (Included == false && (CollidedObjectwithParent.DoesPreExist|| ParentofGameObject.DoesPreExist ))
            {

                ParentofGameObject.collidedObjects.Add(collision.transform.parent.gameObject);

                var bb = CollidedObjectwithParent;



                if (bb.ColorOfBall == ParentofGameObject.ColorOfBall)
                {



                    ParentofGameObject.AdjSameColor++;




                    if (!ParentofGameObject.DoesPreExist && (bb.AdjSameColor >= 2 || ParentofGameObject.AdjSameColor >= 3))
                    {



                        if (TriggerCount == 0)
                        {
                            ParentofGameObject.DestroyonCollision();

                            GameManager.Instance.IsDestroied = true;



                            GameManager.Instance.ChainReaction = true;
                            TriggerCount++;
                        }


                       




                        return;

                        //Debug.Log("Name                    " + gameObject.transform.parent.name);



                        

                        Debug.Log("WHYYYY");

                        /*  Task.Run(() =>
                          {
                              Task.Delay(100).Wait();
                              Debug.Log(" Root Directory ");
                              RootTrackDestroy();

                          });*/







                        //Debug.Log(GameManager.Instance.DestroyOccurance);
                    }
                    /*   else
                       {
                           ParentofGameObject.AdjSameColor++;
                       }
   */

                }

                ParentofGameObject.stickComplete = true;


                //gameObject.transform.parent.GetComponent<Ballbounce>().RegisterOperationdone = true;
            }
            else
            {
                Included = false;
            }



            /*    if (!ParentofGameObject.DoesPreExist && CollidedObjectwithParent.AdjSameColor > 2 && ParentofGameObject.AdjSameColor>=3)
                {
                    Debug.Log("triggered");
                    ParentofGameObject.DestroyonCollision();
                    GameManager.Instance.DestroyOccurance = true;
                    // Debug.Log(GameManager.Instance.DestroyOccurance);
                }*/
            Task.Run(() =>
            {
                Task.Delay(100).Wait();
                //Debug.Log(" Setting true ");
                ParentofGameObject.DoesPreExist = true;

            });



            //GameManager.Instance.DestroyBalls();

            GameManager.Instance.isstuck = true;


            
            //Debug.Log(gameObject.transform.parent.GetComponent<Ballbounce>().AdjSameColor);
        }


        IEnumerator ChangeBodyType()
        {

            yield return new WaitForSeconds(.0f);
            Debug.Log("body changed");

            //  gameObject.transform.GetComponent<Ballbounce>().posX = gameObject.transform.parent.transform.position.x;
            // gameObject.transform.GetComponent<Ballbounce>().posY = gameObject.transform.parent.transform.position.y;

            collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().angularVelocity = 0;


            gameObject.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;


        }



       


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("GameOverWall"))
        {
            if (gameObject.transform.parent.GetComponent<Ballbounce>().DoesPreExist)
            {

               // Debug.Log("Game Over");
            }

        }
    }



}

