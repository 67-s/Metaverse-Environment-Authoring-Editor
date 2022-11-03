using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        BoltNetwork.RegisterTokenClass<MapInfoToken>();
    }
    
    public override void BoltStartDone()
    {
        roomName = GameObject.Find("RmNameInputField").GetComponent<RoomData>().getRoomName();
        password = GameObject.Find("RmPwdInputField").GetComponent<RoomData>().getPassword();
        connectionLimit = GameObject.Find("RmLimitNumberInputField").GetComponent<RoomData>().getNumberOfPeople();

        if (BoltNetwork.IsServer)
        {
            //string matchName = Guid.NewGuid().ToString();
            PhotonRoomProperties props = new PhotonRoomProperties();
            MapInfoToken mt = new MapInfoToken();
            mt.mapInfos = new byte[10];
            mt.mapInfos[0] = 5;
            mt.mapInfos[1] = 0;
            mt.mapInfos[2] = 5;


            props.AddRoomProperty("roomName", roomName,true);
            props.AddRoomProperty("password", password);
            props.AddRoomProperty("connectionLimit", connectionLimit);
            
            BoltMatchmaking.CreateSession(
                sessionID: roomName,
                sceneToLoad: "Scene2",
                token: props,
                sceneToken : mt //이 토큰이 scene로드할 때 올라감
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

    public void JoinRoom()
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
