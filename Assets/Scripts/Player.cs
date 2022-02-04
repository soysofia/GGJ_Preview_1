using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Hace q se mueva el player
public class Player : MonoBehaviour
{
    [SerializeField]
    float speed =3.0f;
    [SerializeField]
    float jumpForce =7.0f;
    Rigidbody2D rb2D;
    SpriteRenderer spr;
    Animator anim;
    [SerializeField, Range(0.01f,10f)]
    float rayDistance =2f;
    [SerializeField]
    Color rayColor = Color.red;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Vector3 rayOrigin;
    [SerializeField]
    Score score;

    Gameinputs gameInputs;

   

    ////////
    void Awake()
    {
        gameInputs = new Gameinputs();
    }
    
    void OnEnable()
    {
        gameInputs.Enable();
    }
   
    void OnDisable()
    {
        gameInputs.Disable();
    }

    ////////

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameInputs.Gameplay.Jump.performed+= _ => Jump();
        gameInputs.Gameplay.Jump.canceled+= _ => JumpCanceled();

    }

    //update de fisica
    void FixedUpdate()
    {
        rb2D.position += Vector2.right * Axis.x * speed * Time.fixedDeltaTime ;
    }

    void Update()
    {
        //transform.Translate(Vector2.right * AxisRaw.x * speed * Time.deltaTime);
       /* if(JumpButton && IsGrounding)
        {
            rb2D.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }*/
       spr.flipX = FlipSprite;

     

    }

    void Jump()
    {
        if(IsGrounding)
        {
            rb2D.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            
        }
    }

    void JumpCanceled()
    {
       rb2D.velocity = new Vector2(rb2D.velocity.x,0f);
        
    }


    void LateUpdate()
    {
        anim.SetFloat("AxisX" , Mathf.Abs(Axis.x) );   
        anim.SetBool("ground" , IsGrounding );   

    }

    ///////

    Vector2 Axis => new Vector2(gameInputs.Gameplay.AxisX.ReadValue<float>(), gameInputs.Gameplay.AxisY.ReadValue<float>());

    // depreciated (bien pero mal juasjuas) 
   // Vector2 Axis => new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
   // Vector2 AxisRaw  => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //


    //bool JumpButton => Input.GetButtonDown("Jump");
    bool FlipSprite => Axis.x > 0 ? false : Axis.x < 0 ? true : spr.flipX;
    bool IsGrounding => Physics2D.Raycast(transform.position + rayOrigin, Vector2.down, rayDistance, groundLayer);

    void OnTriggerEnter2D(Collider2D col)
    {

        if(col.CompareTag("coin"))
        {
            Coin coin = col.GetComponent<Coin>();
            score.AddPoints(coin.GetPoints);
            Destroy(col.gameObject);
        }

    }

    void OnDrawGizmoSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position +  rayOrigin, Vector2.down * rayDistance );
    }

}
