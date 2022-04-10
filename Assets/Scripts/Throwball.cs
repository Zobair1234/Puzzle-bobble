using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwball : MonoBehaviour
{
    public float forceamount;

    public GameObject ball;

    Transform child;

    Transform grandChild;

    GameObject newGrandChild;

    private void Start()
    {

        child = gameObject.transform.GetChild(0);

        grandChild = gameObject.transform.GetChild(0).GetChild(0);

    }

    private void Update()
    {
            

        if (!grandChild.GetComponent<Ballbounce>().stickComplete) 
            {
                if (Input.GetMouseButtonDown(0) && transform.GetChild(0).childCount >0)
                {
            

                    grandChild.GetComponent<Rigidbody2D>().AddForce(transform.right * forceamount);

                    //gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = -.2f;

                     grandChild.parent = null;

    
                }

        
            }

        if (grandChild.GetComponent<Ballbounce>().stickComplete)
        {
            newGrandChild = Instantiate(ball, child.transform.position, ball.transform.rotation);

            newGrandChild.transform.parent = child;

            GameManager.Instance.isstuck = false;

            grandChild = gameObject.transform.GetChild(0).GetChild(0);
        }

        else
        {
            Debug.Log(grandChild.GetComponent<Ballbounce>().stickComplete);
        }
    }
}
