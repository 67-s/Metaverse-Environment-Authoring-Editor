using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Rigidbody[] rigidBodys = gameObject.GetComponentsInChildren<Rigidbody>();
		foreach(Rigidbody rigidBody in rigidBodys)
		{
			rigidBody.useGravity = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
