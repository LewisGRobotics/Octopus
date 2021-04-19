using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMusic : MonoBehaviour
{
    public AK.Wwise.State musicState;
    public AK.Wwise.State silenceState;
    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown("up") || Input.GetKeyDown("joystick button 0"))
        {
                musicState.SetValue();
       }
    }
}
