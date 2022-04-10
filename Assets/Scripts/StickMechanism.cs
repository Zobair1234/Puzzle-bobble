using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMechanism : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
     /*   if(collision.gameObject.CompareTag("Ball"))
        {
            //collision.gameObject.GetComponent<Ballbounce>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            GameManager.Instance.isstuck = true;
            //Debug.Log("Collided");
        }*/
    }
}
