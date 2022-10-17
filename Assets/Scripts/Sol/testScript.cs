using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

[RequireComponent(typeof(Menu))]
public class testScript : MonoBehaviour
{
    // Start is called before the first frame update
    Menu m;
    List<string> roomNames;

    void Start()
    {
        m = gameObject.GetComponent<Menu>();

        m.SetRoomName("myFirstRoom");
        m.SetPassword("101010");
        m.SetConnectionLimit(15);

        roomNames = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            m.StartAsServer();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            m.StartAsClient();
        }
        else if(Input.GetKeyDown(KeyCode.G))
        {
            roomNames = m.GetRoomNames();
            foreach (var roomName in roomNames)
            {
                Debug.Log(roomName);
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            m.JoinRoom("myFirstRoom");
        }
    }

}
