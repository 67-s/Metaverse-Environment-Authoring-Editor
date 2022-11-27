using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using System.Net;

public class PersonBehaviour : EntityEventListener<IPersonState>
{
    Rigidbody rigid;
    Animator animator;
    Camera cam;
    Chatting chat;


    public static bool chatActivate = false;
    public override void Attached()
    {
        
        state.SetTransforms(state.PersonTransform, transform);
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        state.SetAnimator(animator);
        cam = GameObject.FindObjectOfType<Camera>();
        chat = GameObject.Find("Canvas").GetComponent<Chatting>();

        Vector3 newPosition = transform.position;
        newPosition.y += 8.0f;
        newPosition.z -= 5.0f;
        cam.transform.position = newPosition;
    }

    public override void SimulateOwner()
    {
        
        if (chatActivate) { 
            //Debug.Log("ccccccccccccc");
            return; 
        }
        var speed = 4f;
        var movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { movement.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
            cam.transform.position = cam.transform.position + (movement.normalized * 8.0f * BoltNetwork.FrameDeltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Return))
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
            animator.SetBool("isRun", false);
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

    }

}