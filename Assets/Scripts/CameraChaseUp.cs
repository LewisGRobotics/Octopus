using UnityEngine;

public class CameraChaseUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var octopus = GameObject.Find("Octopus");
        if(octopus.transform.position.y > gameObject.transform.position.y + 20)
        gameObject.transform.position += new Vector3(0, 1f, 0);
    }
}
