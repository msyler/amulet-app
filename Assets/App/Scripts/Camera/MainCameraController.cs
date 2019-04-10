using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{

    public Transform TargetToFollow;
    public float smoothSpeed = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(TargetToFollow.position.x, transform.position.y, TargetToFollow.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed ) ;
    }
}
