using System;
using UnityEngine;
using UnityEngine.UI;

public class playerscript : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask groundLayer;
    public float speed;
    public float rotationSpeed;
    public float JumpPower;
    public float bhopPower;
    public float Acceleration;
    public float AirAcceleration;
    public float AirAccelerationMax;
    public bool PlayerIsDead = false;
     public int jumpcount = 0;
    public Rigidbody2D player;
    bool truth = false;
    int jumpcounter = 0;
    public ParticleSystem jumpParticle;
    public AudioClip doublejumpnoise;
    public AudioClip okdesuka;
    public Text debugtext;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }
    bool IsGrounded;//()
    //{
    //    Vector2 position = transform.position;
    //    Vector2 direction = Vector2.down;
    //    float distance = 1.2f;

    //    Debug.DrawRay(position, direction, Color.green);

    //    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
    //    if (hit.collider != null)
    //    {
    //        return true;
    //    }

    //    return false;
    //}
    void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            if (debugtext)
            {
                debugtext.text = "true";
            }
            IsGrounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (debugtext)
        {
            debugtext.text = "false";
        }
        IsGrounded = false;
    }
    bool groundedQmark()
    {
        //if (=)
        //{

        //    return
        //}
        return true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded)
            {
                truth = true;
                jumpcounter = jumpcount;
            }else if (jumpcounter!=0)
            {
                truth = true;
                jumpcounter --;
                jumpParticle.Play();
                AudioSource.PlayClipAtPoint(doublejumpnoise, this.transform.position);
            }
        }
        if (Input.GetKeyDown("o"))
        {
            AudioSource.PlayClipAtPoint(okdesuka, this.transform.position);
        }
        if (Input.GetKeyDown("f7"))
        {
            if (PlayerIsDead)
                reset();
            else
                DIE();
        }
    }
    void DIE()//temorary death "animation," probably going to replace with ragdoll like gmod.
    {
        PlayerIsDead = true;
        player.freezeRotation = false;
        if (player.velocity.x == 0)
        player.AddForce(new Vector2(.5f, 0), ForceMode2D.Impulse);
    }
    public void reset()//hope this all works
                       //╚it does not :/
    {                //  ╚it does now.
        PlayerIsDead = false;
        transform.position = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        player.velocity = new Vector2(1, 2);
        player.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        bool onground = IsGrounded;
        if (!PlayerIsDead)
        {
            if (truth)
            {
                if (Input.GetKey("a"))
                {
                    player.AddForce(new Vector2(-bhopPower, JumpPower), ForceMode2D.Impulse);
                }
                else if (Input.GetKey("d"))
                {
                    player.AddForce(new Vector2(bhopPower, JumpPower), ForceMode2D.Impulse);
                }
                else
                {
                    player.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
                }
                truth = false;
            }

            if (Input.GetKey("w"))// doors&cover? sounds good to me. other keys will be crowded.
            {

            }
            if (Input.GetKey("s"))// drop through one way floors.
            {                    //  ╚you still have not though of a preformance friendly way to do this. 
                                 //   ╚rude.
            }                  //      ╚shut ahup. 
            if (Input.GetKey("a"))//    ╚hey guys, it turns out it's built in!
            {                     //     ╚yeah we figured that out ages ago.
                if (IsGrounded)
                {
                    if (player.velocity.x > -speed)
                    {
                        player.AddForce(new Vector2(-Acceleration, 0), ForceMode2D.Force);// if player is on ground move faster.
                    }
                }
                else
                {
                    if (player.velocity.x > -AirAccelerationMax)
                    {
                        player.AddForce(new Vector2(-Acceleration * AirAcceleration, 0), ForceMode2D.Force);
                    }
                }
            }
            if (Input.GetKey("d"))
            {
                if (IsGrounded)
                {
                    if (player.velocity.x < speed)
                    {
                        player.AddForce(new Vector2(Acceleration, 0), ForceMode2D.Force);// ditto of code above.
                    }
                }
                else
                {
                    if (player.velocity.x <AirAccelerationMax)
                    {
                        player.AddForce(new Vector2(Acceleration * AirAcceleration, 0), ForceMode2D.Force);
                    }
                    
                }
            }
            if (transform.position.y < -6200)
            {
                DIE();
                reset();
            }
        }
        else// think of something funny to put here
        {

        }
    }
}
