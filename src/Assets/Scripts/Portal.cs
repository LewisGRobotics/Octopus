using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	string nextLevel;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var scene = SceneManager.GetActiveScene();

		if (collision.gameObject.name.Contains("Octopus"))
		{
			switch (scene.name)
			{
				// Tutorials
				case "GameDynamics":
					nextLevel = "Level 0";
					break;
				case "Level 0":
					nextLevel = "Level 1";
					break;
				case "Level 1":
					nextLevel = "Level 2";
					break;
				case "Level 2":
					nextLevel = "Level 3";
					break;

				//Laser Area
				case "Level 3":
					nextLevel = "LaserArea0";
					break;
				case "LaserArea0":
					nextLevel = "LaserArea1";
					break;
				case "LaserArea1":
					nextLevel = "LaserArea2";
					break;
				case "LaserArea2":
					nextLevel = "LaserArea3";
					break;

				//Lights out
				case "LaserArea3":
					nextLevel = "LightsOut1";
					break;
				case "LightsOut1":
					nextLevel = "LightsOut2";
					break;
				case "LightsOut2":
					nextLevel = "LightsOut3";
					break;
				case "LightsOut3":
					nextLevel = "LightsOut4";
					break;

				//Tower area
				case "LightsOut4":
					nextLevel = "TowerArea0";
					break;
				case "TowerArea0":
					nextLevel = "TowerArea1";
					break;
				case "TowerArea1":
					nextLevel = "TowerArea2";
					break;
				case "TowerArea2":
					nextLevel = "TowerArea3";
					break;
			}

			PlayerPrefs.SetString("CurrentLevel", nextLevel);
			PlayerPrefs.Save();
			Invoke("LoadScene", 1f);
		}
	}

	private void LoadScene()
	{
		SceneManager.LoadScene(nextLevel);
	}
}
