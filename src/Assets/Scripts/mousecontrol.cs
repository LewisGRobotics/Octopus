using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    public AK.Wwise.Event mouseclick;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp ))
        {
            
            {
                mouseclick.Post(this.gameObject);
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
