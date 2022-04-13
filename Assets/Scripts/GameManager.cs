using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public List<int> ballAmount = new List<int>();



    public bool ChainReaction;

    public int CounterThrow;

    public bool RootHasBeenDestroied;

    public bool NaturalDestroyComplete;

    public GameObject gameoverScreen;


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

         CheckBalls();
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

        for (int i = 0; i < ballAmount.Count; i++)
        {
            ballAmount[i] = 0;
        }

        if (Bhtr.childCount == 0)
        {
            gameoverScreen.SetActive(true);
        }

        else if (BallHolder != null && Bhtr.childCount > 0)
        {
            for (int i = 0; i < Bhtr.childCount; i++)
            {
                var ChildScript = Bhtr.GetChild(i).GetComponent<Ballbounce>();


                if(ChildScript.ColorOfBall == BallColor.Red)
                {
                    ballAmount[0] += 1;
                }
                else if(ChildScript.ColorOfBall == BallColor.White)
                {
                    ballAmount[1] += 1;
                }
                else if (ChildScript.ColorOfBall == BallColor.Yellow)
                {
                    ballAmount[2] += 1;
                }
                else if (ChildScript.ColorOfBall == BallColor.Brown)
                {
                    ballAmount[3] += 1;
                }
                else if (ChildScript.ColorOfBall == BallColor.Blue)
                {
                    ballAmount[4] += 1;
                }
                else if (ChildScript.ColorOfBall == BallColor.Green)
                {
                    ballAmount[5] += 1;
                }

            }
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


}
