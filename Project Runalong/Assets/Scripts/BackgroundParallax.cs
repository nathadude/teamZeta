using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float spriteLength, spriteStartPosition;
    public GameObject MainCamera;
    public float parallax; // Between 0 and 1, where 1 = bg moves exactly with camera and 0 = bg is static in world space
    // Start is called before the first frame update
    void Start()
    {
        spriteStartPosition = transform.position.x;
        spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (MainCamera.transform.position.x * (1 - parallax));  // How far bg moves relative to camera
        float dist = (MainCamera.transform.position.x * parallax); // How far bg needs to move in world space

        transform.position = new Vector3(spriteStartPosition + dist, transform.position.y, transform.position.z);
        
        // If bg is past its bounds on the camera, move the bg forwards
        if (temp > spriteStartPosition + spriteLength) 
            spriteStartPosition += spriteLength;

    }
}
