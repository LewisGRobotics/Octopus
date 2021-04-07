using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathFloor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.collider.name.Contains("Octopus"))
      {
         var scene = SceneManager.GetActiveScene();
         SceneManager.LoadScene(scene.name);
      }
      if (collision.collider.name.Contains("FemaleScientist") || collision.collider.name.Contains("MaleScientist"))
      {
      }
   }
}
