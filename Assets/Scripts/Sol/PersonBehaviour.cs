using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class PersonBehaviour : EntityEventListener<IPersonState>
{
    Rigidbody rigid;
    Animator animator;
    Camera cam;

   
    public override void Attached()
    {
        state.SetTransforms(state.PersonTransform, transform);
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        state.SetAnimator(animator);
        cam = GameObject.FindObjectOfType<Camera>();
    }

    public override void SimulateOwner()
    {
        var speed = 4f;
        var movement = Vector3.zero;
 
        if (Input.GetKey(KeyCode.W)) { movement.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }

        if(movement.x != 0.0f || movement.z != 0.0f)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        Vector3 newPosition = transform.position;
        newPosition.y += 8.0f;
        newPosition.z -= 10.0f;

        cam.transform.position = newPosition;
        /*
        //yhchon11
        float temp=1f;
        Vector3 cam_position = new Vector3(cam.transform.position.x-transform.position.x,cam.transform.position.y-transform.position.y,cam.transform.position.z-transform.position.z);
        if (cam_position.sqrMagnitude < temp)
        {
            Debug.Log("cam"+cam_position.sqrMagnitude);
            Debug.Log("temp"+temp);
            cam.transform.position = transform.position;
        }
        else
        {
            Debug.Log("cam"+cam_position.sqrMagnitude);
            Debug.Log("temp"+temp);
            cam.transform.position += cam_position * (-0.2f);
        }
        */

    }
    //yhchon11
    private GameObject Target;
    public float CameraZ = -10;
    private void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, CameraZ);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 2f);
    }

}