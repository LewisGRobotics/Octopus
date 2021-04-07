using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    bool rang = false;

    private void Destroy()
    {
        Destroy(gameObject);
    }
    private void ResetRang()
    {
        rang = false;
    }

    void BellRang(string color)
    {
        switch (color)
        {
            case "Red":
                if (rang) Invoke("Destroy", 1f);
                else rang = true;                
                break;

            case "Green":
                rang = false;
                break;

            case "Blue":
                rang = false;
                break;

            case "Yellow":
                rang = false;
                break;
        }
        Invoke("ResetRang", 2f);
    }
}
