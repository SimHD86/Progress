
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask walllayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpCD;
    private float HorizontalInput;
    private bool grounded;

    private void Awake()
    {
        //Animation for rigid body
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
       HorizontalInput = Input.GetAxis("Horizontal");
        //Sprite mirroring 
        if (HorizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (HorizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //animator parameters
        anim.SetBool("Run", HorizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Wall jump
        if (wallJumpCD > 0.3f)
        {
            
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;

            }
            else
                body.gravityScale = 3;

            if (Input.GetKey(KeyCode.Space))
                jump();
        }
        else
            wallJumpCD += Time.deltaTime;
    }
    private void jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }

        else if (onWall() && !isGrounded())
        {
            if (HorizontalInput == 0)
            {
                body.velocity = 
                    new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 10);

                transform.localScale = 
                    new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
               
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCD = 0;


            
        }
        anim.SetTrigger("jump");
    }
   

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, 
            boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null; ;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,
            boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, walllayer);
        return raycastHit.collider != null; ;
    }
    public bool canAttack()
    {
        return HorizontalInput == 0 && isGrounded() && !onWall();
    }
    
}
