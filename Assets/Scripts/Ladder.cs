using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // viittaus tikkaiden yläpäähän
    public Transform topHandler;
    // viittaus tikkaiden alapäähän
    public Transform bottomHandler;

    // void Awake() tapahtuu kerran ennen voidia Start()
    void Awake() {
        // viittaus tikapuiden pituuteen
        float height = GetComponent<SpriteRenderer>().size.y;

        // asetetaan tikkaiden yläpää oikeaan paikkaan
        topHandler.position = new Vector3(transform.position.x, transform.position.y + (height/2), 0);
        //asetetaan tikkaiden alapää oikeaan paikkaan
        bottomHandler.position = new Vector3(transform.position.x, transform.position.y - (height/2), 0);
    }
}
