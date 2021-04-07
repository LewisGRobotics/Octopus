using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
   public void PlayGame()
   {
      string sceneToLoad = PlayerPrefs.GetString("CurrentLevel");
      if (string.IsNullOrEmpty(sceneToLoad)) sceneToLoad = "Level 0";
      SceneManager.LoadScene(sceneToLoad);
      Debug.Log("Play");
   }

   public void Quit()
   {
      Application.Quit();
      Debug.Log("Quit");
   }
}
