using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    void start()
    {

    }
    void LateUpdate()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, dir.z * cameraSpeed * Time.deltaTime);
        this.transform.Translate(moveVector);
    }
}