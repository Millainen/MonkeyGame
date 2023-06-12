using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // viittaus pelaajan fysiikkaominaisuuksiin
    private Rigidbody2D player;

    // nuolinäppäimet vasemmalle ja oikealle / A ja D painetaanko
    float horizontalInput;

    private Vector2 movement;

    // nopeus
    public float moveSpeed = 5f;

    // hyppyvoima
    public float jumpForce = 5f;

    // onko pelaaja maassa
    private bool grounded;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //haetaan se, painetaanko nuolinäppäimiä/AD näppäimiä
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown("space")){
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
        if (collision.gameObject.CompareTag("Platform")){
            grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = false;
        }
    }
}
