using UnityEngine;

public class CameraChaseRight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var octopus = GameObject.Find("Octopus");
        if(octopus.transform.position.x > gameObject.transform.position.x + 20)
        gameObject.transform.position += new Vector3(1f, 0, 0);
    }
}
