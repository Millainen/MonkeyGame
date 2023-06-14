using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // viittaus pelaajan fysiikkaominaisuuksiin
    private Rigidbody2D player;

    // nuolinäppäimet vasemmalle ja oikealle / A ja D painetaanko
    float horizontalInput;
    // nuolinäppäimet ylös ja alas / W ja S painetaanko
    float verticalInput;

    private Vector2 movement;

    // nopeus
    public float moveSpeed = 5f;

    // hyppyvoima
    public float jumpForce = 5f;

    // onko pelaaja maassa
    private bool grounded;

    // voiko pelaaja kiivetä
    private bool canClimb;

    // kiipeääkö pelaaja
    private bool isClimbing;

    // tikkaat paikka
    private Transform ladder; ///////////

    // pelaajan pituus
    private float playerHeight; ///////////

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerHeight = GetComponent<SpriteRenderer>().size.y; 
    }

    void Update()
    {
        //haetaan se, painetaanko nuolinäppäimiä/AD näppäimiä
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // haetaan se, painetaanko nuolinäppäimiä ylös alas/WS näppäimiä
        verticalInput = Input.GetAxisRaw("Vertical"); ////////////

        if (Input.GetKeyDown("space") && grounded && !isClimbing){
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        movement.y = 0; //////

        if(canClimb){ 
            if(verticalInput != 0 && grounded && horizontalInput == 0){ ///
                isClimbing = true;
            }
        } else { 
            isClimbing = false; 
        } 


        if(isClimbing){ 
            if(player.position.y <= ladder.transform.GetChild(0).transform.position.y + playerHeight/2 
            && player.position.y >= ladder.transform.GetChild(1).transform.position.y - 0.1f){ 
                player.velocity = Vector2.zero; //////
                player.isKinematic = true; ///////
                movement.y = verticalInput * moveSpeed; ///////
                player.position = new Vector2(ladder.transform.position.x, player.position.y); ///////
            } else { 
                isClimbing = false; 
            }
        } else{ 
            player.isKinematic = false;
        }

        movement.x = horizontalInput * moveSpeed;
    }

    void FixedUpdate()
    {
        //aaltosulkeet {} saa painamalla AltGr + 7

        //liikutetaan pelaajaa
        player.position += movement * Time.fixedDeltaTime;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Platform")){
            grounded = true;
        }

        if(collision.gameObject.CompareTag("Ladder")){ 
            canClimb = true; 
            ladder = collision.transform;
        } 
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = false;
        }

        if(collision.gameObject.CompareTag("Ladder")){ 
            canClimb = false;
        } 
    }
}
