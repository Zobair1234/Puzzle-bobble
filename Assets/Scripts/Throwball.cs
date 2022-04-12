using System.Collections;
using System.Linq;
using UnityEngine;

public class Throwball : MonoBehaviour
{
    public float forceamount;

    public GameObject ball;

    public Transform AfterLaunchParent;
    
    Transform child;

    Transform grandChild;

    GameObject newGrandChild;

    bool canClick;
   

    private void Start()
    {
        canClick = true;

        child = gameObject.transform.GetChild(0);

        grandChild = gameObject.transform.GetChild(0).GetChild(0);



    }

    private void Update()
    {


        if (grandChild != null && !grandChild.GetComponent<Ballbounce>().stickComplete)
        {
            grandChild.GetComponent<Ballbounce>().DoesPreExist = false;

            if (transform!=null && Input.GetMouseButtonDown(0) && transform.GetChild(0).childCount > 0 && canClick)
            {
                 

                grandChild.GetComponent<Rigidbody2D>().AddForce(transform.right * forceamount);

                //gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = -.2f;

                grandChild.transform.parent = AfterLaunchParent;

                canClick = false;

                StartCoroutine(Wait());

                /*canClick = true;*/



            }


        }

        if (GameManager.Instance.IsDestroied )
        {


            SpawnNewBall();
          



        }

        else if(grandChild!=null && grandChild.GetComponent<Ballbounce>().stickComplete)
        {

            SpawnNewBall();
        }

        
    }

    IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(.8f);
        //Debug.Log("ASD");
        canClick = true;

    }

    void SpawnNewBall()
    {

        newGrandChild = Instantiate(GameManager.Instance.DifferentColorofBalls.ElementAt(Random.Range(0, 6)), child.transform.position, ball.transform.rotation);

        newGrandChild.transform.parent = child;

        newGrandChild.name = GameManager.Instance.CounterThrow.ToString();

        GameManager.Instance.CounterThrow++;

        GameManager.Instance.isstuck = false;
        GameManager.Instance.IsDestroied = false;

        grandChild = gameObject.transform.GetChild(0).GetChild(0);
    }

}
