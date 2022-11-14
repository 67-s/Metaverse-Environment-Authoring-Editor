using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class PersonBehaviour : EntityEventListener<IPersonState>
{
    Rigidbody rigid;
    Animator animator;
    Camera cam;
    //yhchon11
    [SerializeField] float xVelocity = 2f;
    [SerializeField] float yForce = 10f;
    [SerializeField] float zVelocity = 2f;
    [SerializeField] float overAir = 1.7f;
    private bool isJump = false;
    //
    public override void Attached()
    {
        state.SetTransforms(state.PersonTransform, transform);
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cam = GameObject.FindObjectOfType<Camera>();

    }

    public override void SimulateOwner()
    {
        var speed = 4f;
        var movement = Vector3.zero;
        //yhchon11
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * xVelocity;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * zVelocity;
        float zump = Input.GetAxis("Jump") * Time.deltaTime * yForce;
        transform.Translate(new Vector3(horizontal, 0, vertical));
        //
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

        //yhchon11
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJump)
            {
                StartCoroutine(Jump());
            }
        }

        IEnumerator Jump()
        {
            isJump = true;
            rigid.AddForce(Vector3.up * yForce, ForceMode.Impulse);
            yield return new WaitForSeconds(overAir);//받는 파트
            isJump = false;
        }//coroutine: n초 동안 cpu에게 권한을 넘긴다, 그후 받는다
        //

        cam.transform.position.Set(transform.position.x, 30.0f, transform.position.z);
    }

}