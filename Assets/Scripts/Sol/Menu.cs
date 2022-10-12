using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;
using UdpKit.Platform.Photon;

public class Menu : GlobalEventListener
{
    string roomName;
    string password;
    int connectionLimit;

    public override void BoltStartBegin()
    {
        base.BoltStartBegin();
    }
    /*
    void OnGUI()
    {
        
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));

        roomName = GUILayout.TextField(roomName);
        password = GUILayout.TextField(password);
        connectionLimit = Int32.Parse(GUILayout.TextField(connectionLimit.ToString()));

        if (GUILayout.Button("Make a room", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            BoltLauncher.StartServer();
        }
        
        if (GUILayout.Button("Start Client", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            BoltLauncher.StartClient(); 
        }

        GUILayout.EndArea();
    }
    */
    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            //string matchName = Guid.NewGuid().ToString();
            PhotonRoomProperties props = new PhotonRoomProperties();
            props.AddRoomProperty("roomName", roomName,true);
            props.AddRoomProperty("password", password);
            props.AddRoomProperty("connectionLimit", connectionLimit);
            
            BoltMatchmaking.CreateSession(
                sessionID: roomName,
                sceneToLoad: "Sol",
                token: props
            );
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

        foreach (var session in sessionList)
        {
            UdpSession udpSession = session.Value as UdpSession;
            PhotonSession photonSession = udpSession as PhotonSession;

            if(photonSession.Properties.ContainsKey("roomName"))
            {
                object Value = photonSession.Properties["roomName"];
                Debug.Log("@@@@@roomName@@@@@: " + Value.ToString());
            }
            if (photonSession.Properties.ContainsKey("password"))
            {
                object Value = photonSession.Properties["password"];
                Debug.Log("@@@@@password@@@@@: " + Value.ToString());
                if (password != Value.ToString())
                    continue;
            }
            if (photonSession.Properties.ContainsKey("connectionLimit"))
            {
                object Value = photonSession.Properties["connectionLimit"];
                Debug.Log("@@@@@connectionLimit@@@@@: " + Value.ToString());
            }


            if (udpSession.HostName.Equals(roomName))
            {
                if (udpSession.Source == UdpSessionSource.Photon)
                {
                    //BoltMatchmaking.JoinSession(udpSession);
                }
            }
        }        
    }

    public void SetRoomName(string roomName)
    {
        this.roomName = roomName;
    }

    public void SetPassword(string password)
    {
        this.password = password;
    }

    public void SetConnectionLimit(int connectionLimit)
    {
        this.connectionLimit = connectionLimit;
    }

    public void StartAsServer()
    {
        BoltLauncher.StartServer();
    }

    public void StartAsClient()
    {
        BoltLauncher.StartClient();
    }

    public void JoinRoom(string roomName)
    {
        BoltMatchmaking.JoinSession(roomName);
    }

    public List<string> GetRoomNames()
    {
        var sessionList = BoltNetwork.SessionList;

        List<string> roomNames = new List<string>();

        foreach(var session in sessionList)
        {
            PhotonSession photonSession = session.Value as PhotonSession;

            roomNames.Add(photonSession.HostName);
        }

        return roomNames;
    }
}
