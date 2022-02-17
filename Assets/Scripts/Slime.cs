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

    float idlingTime =2f;

    IEnumerator patroling;
    IEnumerator idling;
    IEnumerator lastRoutine;

    IEnumerator attack;

    [SerializeField, Range(0.1f,5f)]
    float rayDistance = 2f;

    [SerializeField]
    Color rayColor =Color.red;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    Vector3 rayOrigin;

    [SerializeField]
    AnimationClip attackClip;

    bool isAttacking = false;

    [SerializeField]
    int damage = 2;


    void Awake()
    {
        rb2D= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        spr= GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        
        StartIA();
    }

    IEnumerator IdlingRoutine()
    
    {
        anim.SetFloat("patrol",0f);
        yield return new WaitForSeconds(idlingTime);
        StartPatrolling();

    }



    void StartAttack()
    {
        attack = AttackingRoutine();
        StartCoroutine(attack);
    }

    IEnumerator AttackingRoutine()
    {
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(attackClip.length);
        StartCoroutine(lastRoutine);
        isAttacking = false;
    }

    IEnumerator PatrolingRoutine()
    {
        
        anim.SetFloat("patrol",1f);

        while (true) 
        {
            if(Attack && !isAttacking)
            {
                isAttacking =true;
                lastRoutine= patroling;
                StartAttack();
              yield break;

            }
            Debug.Log("patroling");
            rb2D.position += direction * moveSpeed* Time.deltaTime;
            timer+= Time.deltaTime;


            if (timer>= patrolTimeLimit)
             {   
                 
                StartIdling();

                yield break;

            }
            yield return null;

        }
    }

 


    void StartIdling()
    {

        idling = IdlingRoutine();
        StartCoroutine(idling);
        
        
    }

    void StartPatrolling()

    {  
        timer = 0f;
        direction = direction == Vector2.right ? Vector2.left: Vector2.right;
        spr.flipX =FlipSpriteX;
        anim.SetBool("patrol",true);
        patroling =PatrolingRoutine();
        StartCoroutine(patroling);
    }


    void StartIA()
    {
        patroling = PatrolingRoutine();
        StartCoroutine(patroling);
    }

    void Update()
    {
        if (Attack)
        {
            Debug.Log("attack");
        }
    }
    
    /*void FixedUpdate()
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
*/
    bool FlipSpriteX => direction == Vector2.right ? false : true;

    bool Attack => Physics2D.Raycast(transform.position+rayOrigin,Vector2.right, rayDistance, playerLayer) || 
    Physics2D.Raycast(transform.position+rayOrigin,Vector2.left, rayDistance, playerLayer);

    void OnDrawGizmosSelected()
    {
        Gizmos.color =rayColor;
        Gizmos.DrawRay(transform.position + rayOrigin,Vector3.right *rayDistance);
        Gizmos.DrawRay(transform.position+ rayOrigin, Vector3.left*rayDistance);

    }



    public int GetDamage => damage;

    public  void MakeDamage( )=>  Gamemanager.instance.GetPlayer.Health-=damage;



}
