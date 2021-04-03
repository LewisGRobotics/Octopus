using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistController : MonoBehaviour
{
   Rigidbody2D rigidBody;
   private void OnCollisionEnter2D(Collision2D collision)
   {
      if(collision.collider.name == "DeathFloor")
      {
         Destroy(gameObject.GetComponent<BoxCollider2D>());
         var ps = gameObject.GetComponentInChildren<ParticleSystem>();
         if (ps) 
         {
            ps.Play();
         }

         //rigidBody.gravityScale = 0f;
      }
   }

   // Start is called before the first frame update
   void Start()
   {
      rigidBody = gameObject.GetComponent<Rigidbody2D>();
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
