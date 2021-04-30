using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraParallax : MonoBehaviour
{
    public float scaleFactor = 100f;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // void Update() {
        //mainCamera.transform.position += Vector3.right * Time.deltaTime * 3f;
    // }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.transform.position / scaleFactor;
        transform.rotation = mainCamera.transform.rotation;
    }
}
