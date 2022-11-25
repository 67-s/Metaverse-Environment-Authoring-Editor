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


    bool chatActivate = false;
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
        newPosition.z -= 10.0f;
        cam.transform.position = newPosition;
    }

    public override void SimulateOwner()
    {
        if (chatActivate) return;
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
            chat.input.ActivateInputField();
            chatActivate = true;
        }

        if (movement.x != 0.0f || movement.z != 0.0f)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        /*
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 8.0f, transform.position.z - 10.0f);
        //Debug.Log("x"+newPosition.x);
        //Debug.Log("y"+newPosition.y);
        //Debug.Log("z"+newPosition.z);
        
        cam.transform.position = newPosition;

        //Vector3 TargetPos = newPosition;
        //transform.position = Vector3.Lerp(transform.position,TargetPos, Time.deltaTime*2f);
        */
        //yhchon11
        
        float temp =15f;
        Vector3 cam_position = new Vector3(cam.transform.position.x-transform.position.x,cam.transform.position.y-transform.position.y,cam.transform.position.z-transform.position.z);
        
        if (cam_position.sqrMagnitude > temp)
        {
            Debug.Log(cam_position.sqrMagnitude);

            Vector3 TargetPos = new Vector3(transform.position.x, transform.position.y+8.0f, transform.position.z-10.0f);
            cam.transform.position = Vector3.Lerp(cam.transform.position, TargetPos, Time.deltaTime * 2f);
            
        }
        else
        {
            Debug.Log(cam_position.sqrMagnitude);

        }
       

    }
    /*
    //yhchon11
    private GameObject Target;
    public float CameraZ = -10;
    private void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(transform.position.x, transform.position.y, CameraZ);
        cam.transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 2f);
    }
    */
}