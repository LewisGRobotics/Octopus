using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenSpawner : MonoBehaviour
{
    public GameObject token;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(token);
    }
}
