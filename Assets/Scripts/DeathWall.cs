using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : MonoBehaviour
{
   GameObject octopus;
   bool laserStart = false;
   float wallSpeed = 0.3f;
   float startPosition;

   private void Start()
   {
      octopus = GameObject.Find("Octopus");
      startPosition = octopus.transform.position.x;
   }

   private void Update()
   {
      if (octopus.transform.position.x < gameObject.transform.position.x)
      {
         var scene = SceneManager.GetActiveScene();
         SceneManager.LoadScene(scene.name);
      }
   }

   private void FixedUpdate()
   {
      // Chase
      if (laserStart)
      {
         float penaltyFactor = 1f + 0.01f * (octopus.transform.position.x - gameObject.transform.position.x);
         gameObject.transform.position += new Vector3(wallSpeed * penaltyFactor, 0, 0);
      }
      else
      {
         if (octopus.transform.position.x > startPosition + 1f) laserStart = true;
      }
            

        // Wall of death
        //var octopus = GameObject.Find("Octopus");
        //if (octopus.transform.position.x > gameObject.transform.position.x + 122)
        //    gameObject.transform.position += new Vector3(0.5f, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
