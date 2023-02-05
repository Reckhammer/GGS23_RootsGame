using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTarget;

    private float cameraFollowSpeed = 10f;
    private Vector3 offset = new Vector3 (0,0,-10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(cameraTarget.position + offset, transform.position + offset, Time.deltaTime * cameraFollowSpeed);
    }


    public void ChangeCameraTarget(Transform transform)
    {
        cameraTarget = transform;
        this.transform.position = cameraTarget.position;
    }
}
