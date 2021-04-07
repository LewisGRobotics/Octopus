using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToken : MonoBehaviour
{    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        Invoke("SetActive", 1f);
    }

    private void SetActive()
    {
        gameObject.SetActive(true);
    }
}
