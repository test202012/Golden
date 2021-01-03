using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;
    public float horizontalmove;
     
    public float speed;
    public float facedirection;
    public float jumpforce;
    public LayerMask ground;
   
    public int Cherry = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
        if(Input.GetButtonDown("Jump")){
            rb.velocity = new Vector2(rb.velocity.x,jumpforce);
            anim.SetBool("jumping",true);
        }
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (horizontalmove * speed,rb.velocity.y);
        float facedirection = Input.GetAxisRaw("Horizontal");

        if(horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed , rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(facedirection));
        }
        if(facedirection != 0){
            transform.localScale = new Vector3(facedirection,1,1);

        }
        
    }

    void SwitchAnim()
    {
        anim.SetBool("idle",false);
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }  
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
        
    }

    private void OntriggerEnter2D(Collider2D collision)
    {  
        if(collision.tag =="Collection")
        { 
            Destroy(collision.gameObject);
            Cherry += 1;
        }
    }
}
