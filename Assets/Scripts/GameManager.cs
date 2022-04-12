using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isstuck;

    public bool IsDestroied;

    public List<GameObject> Roots = new List<GameObject>();

    public bool DestroyOccurance;

    public GameObject BallHolder;

    //public GameObject GameOverWall;

    public List<GameObject> DifferentColorofBalls = new List<GameObject>();


    public List<GameObject> ToDestroy = new List<GameObject>();



    public bool ChainReaction;

    public int CounterThrow;

    public bool RootHasBeenDestroied;

    public bool NaturalDestroyComplete;


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
        if (DestroyOccurance)
        {
            DestroyOccurance = false;
            //   Debug.Log("ASD");
            DestroyDisconnectedBalls();
        }

    }

    public void StartNaturalDestroy()
    {
        if(ToDestroy.Count>0)
        {
            foreach(var co in ToDestroy.ToList())
            {
                if (co != null)
                {
                    if (co.GetComponent<Ballbounce>().IsRoot)
                    {
                        foreach (var Rco in Roots.ToList())
                        {
                            if(Rco == co)
                            {
                                Roots.Remove(Rco);
                            }
                        }
                    }
                    Destroy(co);
                }
            }
        }

        ToDestroy.Clear();

        ToDestroy = new List<GameObject>();


        //IsDestroied = true;
        NaturalDestroyComplete = true;
    }




    public void TraverseBalls(GameObject newRoot)
    {

       // Debug.Log(newRoot.name + "is being Traversed");
        var bb = newRoot.GetComponent<Ballbounce>();

        bb.hasVisited = true;

        foreach (var co in bb.collidedObjects)
        {
            if (co != null && !co.GetComponent<Ballbounce>().hasVisited && !co.GetComponent<Ballbounce>().RegisterForDestruction)
            {
              //  Debug.Log(co.name + " being Traversed");
                TraverseBalls(co);


            }


        }

    }

    public void DestroyDisconnectedBalls()
    {
        //Debug.Log("Child Count ");

        Transform Bhtr = BallHolder.transform;

        
        if (BallHolder != null && Bhtr.childCount > 0)
        {
            for (int i = 0; i < Bhtr.childCount; i++)
            {
                var ChildScript = Bhtr.GetChild(i).GetComponent<Ballbounce>();

               


                if (!ChildScript.hasVisited)
                {
                    Debug.Log("Destroy " + Bhtr.GetChild(i).gameObject.name);
                    Destroy(Bhtr.GetChild(i).gameObject);
                }

                else
                {
                    Debug.Log("not Destroy " + Bhtr.GetChild(i).gameObject.name);
                    ChildScript.hasVisited = false;
                }
            }
        }
    }


    public void CheckBalls()
    {
        Transform Bhtr = BallHolder.transform;


        if (BallHolder != null && Bhtr.childCount > 0)
        {
            for (int i = 0; i < Bhtr.childCount; i++)
            {
                var ChildScript = Bhtr.GetChild(i).GetComponent<Ballbounce>();

                if (ChildScript.hasVisited)
                {
                    Debug.Log("Error " + Bhtr.GetChild(i).gameObject.name);

                }


            }
        }
    }


}
