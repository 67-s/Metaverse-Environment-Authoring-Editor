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

        Vector3 newPosition = transform.position;
        newPosition.y += 8.0f;
        newPosition.z -= 10.0f;
        cam.transform.position = newPosition;
    }

    public override void SimulateOwner()
    {
        var speed = 4f;
        var movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { 
            movement.z += 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
            //animator.SetBool("hi", false);
        }
        if (Input.GetKey(KeyCode.S)) {
            movement.z -= 1; 
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.A)) { 
            movement.x -= 1;
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        if (Input.GetKey(KeyCode.D)) { 
            movement.x += 1;
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 315, 0);
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0, 225, 0);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 45, 0);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0, 135, 0);
        }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
            cam.transform.position = cam.transform.position + (movement.normalized * 8.0f * BoltNetwork.FrameDeltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }

        if(input.GetKeyDown(KeyCode.Return)
        {
            //chat.input.ActivateInputField();
            //chatActivate = true;
            //Debug.Log("pppppppppppp");
        }

        if (movement.x != 0.0f || movement.z != 0.0f)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            //animator.SetBool("hi", false);
            animator.SetBool("isRun", false);
            //animator.SetBool("hi", false);
        }
        //yhchon11
        if (input.GetKeyDown(KeyCode.H))
        {
            animator.SetBool("hi", true);
        }
        if (input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("dance", true);
        }

        //yhchon11
        
        float temp =15f;
        Vector3 cam_position = new Vector3(cam.transform.position.x-transform.position.x,cam.transform.position.y-transform.position.y,cam.transform.position.z-transform.position.z);
        
        if (cam_position.sqrMagnitude > temp)
        {
            //Debug.Log(cam_position.sqrMagnitude);

            Vector3 TargetPos = new Vector3(transform.position.x, transform.position.y+8.0f, transform.position.z-5.0f);
            cam.transform.position = Vector3.Lerp(cam.transform.position, TargetPos, Time.deltaTime * 2f);
            
        }
        else
        {
            //Debug.Log(cam_position.sqrMagnitude);

        }

        cam.transform.position = newPosition;
        */
    }

}