using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
 



public class ChainReactionRoot : MonoBehaviour
{
     

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.ChainReaction)
        {
            GameManager.Instance.ChainReaction = false;

            GameManager.Instance.StartNaturalDestroy();

           

            if(GameManager.Instance.NaturalDestroyComplete)
            {
                Debug.Log("go");
                GameManager.Instance.NaturalDestroyComplete = false;
                RootTrackDestroy();

              
               
            }
          
        }

       
        
    }


    void RootTrackDestroy()
    {
        //Debug.Log(GameManager.Instance.DestroyOccurance);
        
        foreach (var co in GameManager.Instance.Roots.ToList())
        {


            
            if (co != null && !co.GetComponent<Ballbounce>().RegisterForDestruction)
            {
                Debug.Log("Root Name " + co.name);
                GameManager.Instance.TraverseBalls(co);
            }
            else if (co == null)
            {
                
            }

        }
        //Debug.Log("RootTrackDestroy accessed");

        //GameManager.Instance.DestroyDisconnectedBalls();
        GameManager.Instance.DestroyOccurance = true;

        //Debug.Log(GameManager.Instance.DestroyOccurance);
    }
}
