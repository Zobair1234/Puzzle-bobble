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

    [SerializeField]
    GameObject now;

    [SerializeField]
    GameObject next;


    private void Start()
    {
        canClick = true;

        child = gameObject.transform.GetChild(0);

        grandChild = gameObject.transform.GetChild(0).GetChild(0);

        GameManager.Instance.CheckBalls();


    }

    private void Update()
    {


        if (grandChild != null && !grandChild.GetComponent<Ballbounce>().stickComplete)
        {
            grandChild.GetComponent<Ballbounce>().DoesPreExist = false;

            if (transform != null && Input.GetMouseButtonDown(0) && transform.GetChild(0).childCount > 0 && canClick)
            {


                grandChild.GetComponent<Rigidbody2D>().AddForce(transform.right * forceamount);

                //gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = -.2f;

                grandChild.transform.parent = AfterLaunchParent;

                canClick = false;

                StartCoroutine(Wait());


                /*canClick = true;*/



            }


        }

        if (GameManager.Instance.IsDestroied)
        {

            // GameManager.Instance.CheckBalls();
            SpawnNewBall();




        }

        else if (grandChild != null && grandChild.GetComponent<Ballbounce>().stickComplete)
        {
            GameManager.Instance.CheckBalls();
            SpawnNewBall();
        }


    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(.5f);
        //Debug.Log("ASD");
        canClick = true;
        GameManager.Instance.CheckBalls();

    }



    void SpawnNewBall()
    {
        if (GameManager.Instance.BallHolder.transform.childCount > 0)
        {

            int RandomOutput = Random.Range(0, 6);

            if (GameManager.Instance.ballAmount[RandomOutput] == 0)
            {
                for (int i = 0; i < GameManager.Instance.ballAmount.Count; i++)
                {
                    if (GameManager.Instance.ballAmount[i] > 0)
                    {
                        RandomOutput = i;
                    }
                }
            }


            var ToExist = GameManager.Instance.DifferentColorofBalls.ElementAt(RandomOutput);

            newGrandChild = Instantiate(ToExist, child.transform.position, ball.transform.rotation);

            newGrandChild.transform.parent = child;

            newGrandChild.name = GameManager.Instance.CounterThrow.ToString();

            GameManager.Instance.CounterThrow++;

            GameManager.Instance.isstuck = false;
            GameManager.Instance.IsDestroied = false;

            grandChild = gameObject.transform.GetChild(0).GetChild(0);
        }
    }

}
