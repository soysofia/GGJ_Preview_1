using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    Player player;

    
   HealthBar healthBar;


    void Awake()
    {

        if (!instance)
        {
            instance=this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }

       
    }



    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
         healthBar = GameObject.FindWithTag("health").GetComponent<HealthBar>();
    }

    public Player GetPlayer => player;

    public HealthBar GetHealthBar => healthBar ;

}


//8:46