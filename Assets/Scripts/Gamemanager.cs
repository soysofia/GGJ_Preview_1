using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    Player player;

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
    }

    public Player GetPlayer => player;

}


//47:19