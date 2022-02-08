using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    Rigidbody2D rb2D;
    Animator anim;
    SpriteRenderer spr;

    [SerializeField, Range(0.1f, 10f)]
    float moveSpeed =3f;
    [SerializeField]
    Vector2 direction =Vector2.right;
    float timer =0f;
    [SerializeField, Range(0.1f, 10f)]
    float patrolTimeLimit =3f;
    float horizontalDirection =1.0f;



    void Awake()
    {
        rb2D= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        spr= GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        
    }


    
    void FixedUpdate()
    {
        rb2D.position += direction * moveSpeed* Time.deltaTime;
        timer+= Time.deltaTime;


        if (timer>= patrolTimeLimit)
        {   
            spr.flipX =FlipSpriteX;
            timer = 0f;
            direction = direction == Vector2.right ? Vector2.left: Vector2.right;
        }

    }

    bool FlipSpriteX => direction == Vector2.right ? false : true;
}
