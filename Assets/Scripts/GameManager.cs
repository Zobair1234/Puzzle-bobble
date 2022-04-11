using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isstuck;

    public bool IsDestroied;

    public List<GameObject> Roots = new List<GameObject>();

    public bool DestroyOccurance;

    public GameObject BallHolder;


    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        //DontDestroyOnLoad(gameObject);


    }

    private void Update()
    {
        if(DestroyOccurance)
        {
            DestroyOccurance = false;

            foreach (var co in Roots.ToList())
            {
                if (co != null )
                {

                    var bb = co.GetComponent<Ballbounce>().collidedObjects;

                    TraverseBalls(co);

                    
                }
            }

            DestroyDisconnectedBalls();
        }
    }

    void TraverseBalls(GameObject newRoot)
    {
        var bb = newRoot.GetComponent<Ballbounce>();

        bb.hasVisited = true;

        foreach (var co in bb.collidedObjects.ToList())
        {
            if (co != null && !co.GetComponent<Ballbounce>().hasVisited)
            {

                TraverseBalls(co);
              
               
            }
            
        }

    }

    void DestroyDisconnectedBalls()
    {
        Transform Bhtr = BallHolder.transform;

       
        if (BallHolder!=null && Bhtr.childCount>0)
        {
            for(int i=0;i< Bhtr.childCount;i++)
            {
                var ChildScript = Bhtr.GetChild(i).GetComponent<Ballbounce>();
               
                if (!ChildScript.hasVisited)
                {
                   
                    Destroy(Bhtr.GetChild(i).gameObject);
                }

                else
                {
                    Debug.Log("Working");
                    ChildScript.hasVisited = false;
                }
            }
        }
    }
}
