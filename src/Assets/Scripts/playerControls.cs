using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    public AK.Wwise.Event jumpSound;
    bool isPlayerJumping = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("joystick button 0"))
        {
            if (isPlayerJumping == false)
            {
                jumpSound.Post(this.gameObject);
            }
            
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isPlayerJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isPlayerJumping = false;
    }
}
