using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBell : MonoBehaviour
{
    private void Update()
    {
        var animator = gameObject.GetComponent<Animator>();
        if (animator.GetBool("IsPicked"))
        {
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var animator = gameObject.GetComponent<Animator>();
        animator.SetBool("IsPicked", true);
    }
}
