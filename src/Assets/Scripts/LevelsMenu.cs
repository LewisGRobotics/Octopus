using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
   List<GameObject> levelMenus;

   private void Start()
   {
      levelMenus = new List<GameObject>();
      levelMenus.Add(gameObject.transform.Find("TutorialLevels").gameObject);
      levelMenus.Add(gameObject.transform.Find("LaserAreaLevels").gameObject);
      levelMenus.Add(gameObject.transform.Find("LightsOutLevels").gameObject);
      levelMenus.Add(gameObject.transform.Find("TowerAreaLevels").gameObject);
   }
   public void Next()
   {
      int index = 0;
      foreach(var lvlmenu in levelMenus)
      {
         index++;
         if (lvlmenu.activeSelf)
         {
            lvlmenu.SetActive(false);
            break;
         }
      }
      if (index > levelMenus.Count - 1) levelMenus[0].SetActive(true);
      else levelMenus[index].SetActive(true);
      Debug.Log(levelMenus.Count);
      Debug.Log(index);
      
   }

   public void Previous()
   {
      int index = 0;
      foreach (var lvlmenu in levelMenus)
      {
         if (lvlmenu.activeSelf)
         {
            lvlmenu.SetActive(false);
            break;
         }
         index++;
      }
      if (index == 0) levelMenus[levelMenus.Count -1].SetActive(true);
      else levelMenus[index - 1].SetActive(true);
   }

   public void Load(string level)
   {
      SceneManager.LoadScene(level);
   }
}
