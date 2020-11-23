using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject toFollow;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(toFollow.transform.position.x, toFollow.transform.position.y, -10), 0.5f);
    }
}
