using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class PersonBehaviour : EntityEventListener<IPersonState>
{
    public override void Attached()
    {
        state.SetTransforms(state.RobotTransform, transform);
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
    }

}