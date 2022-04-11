using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
  /*  public float speed = 5f;
    public float rotz;
  */

    public float rotationspeed;
    public Vector3 _rotation;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        

        if (Input.GetKey(KeyCode.A))
        {
            _rotation = Vector3.forward;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            _rotation = Vector3.back;
        }
        else
            _rotation = Vector3.zero;



       
            transform.Rotate(_rotation * Time.deltaTime * rotationspeed);
        

        if (transform.eulerAngles.z >= 170)
        {
            transform.eulerAngles = new Vector3(0,0,169);
        }
        else if (transform.eulerAngles.z <= 14)
        {
            transform.eulerAngles = new Vector3(0, 0, 15);
        }

        //Debug.Log(transform.eulerAngles.z);

    }
}
