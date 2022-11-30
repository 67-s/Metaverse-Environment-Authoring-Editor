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
    string roomIntro;

    public override void BoltStartBegin()
    {
        base.BoltStartBegin();
        BoltNetwork.RegisterTokenClass<MapInfoToken>();
        BoltNetwork.RegisterTokenClass<authenticationToken>();
    }
    
    public override void BoltStartDone()
    {
        roomName = GameObject.Find("RmNameInputField").GetComponent<RoomData>().GetRoomName();
        password = GameObject.Find("RmPwdInputField").GetComponent<RoomData>().GetPassword();
        connectionLimit = GameObject.Find("RmLimitNumberInputField").GetComponent<RoomData>().GetNumberOfPeople();
        roomIntro = GameObject.Find("RmIntroInputField").GetComponent<RoomData>().GetIntro();

        if (BoltNetwork.IsServer)
        {
            //string matchName = Guid.NewGuid().ToString();
            PhotonRoomProperties props = new PhotonRoomProperties();
            MapInfoToken mt = new MapInfoToken();
            List<CreationData> maps = GameObject.Find("Mediator").GetComponent<Mediator>().element.GetComponent<Installer>().ExtractData();

            if(maps != null)
            {
                foreach(var map in maps)
                {
                    mt.addByte((byte)map.Origin);
                    mt.add(map.Target.transform.position.x);
                    mt.add(map.Target.transform.position.y);
                    mt.add(map.Target.transform.position.z);
                    mt.add(map.Target.transform.rotation.x);
                    mt.add(map.Target.transform.rotation.y);
                    mt.add(map.Target.transform.rotation.z);
                    mt.add(map.Target.transform.rotation.w);
                }
            }

            props.AddRoomProperty("roomName", roomName,true);
            props.AddRoomProperty("password", password);
            props.AddRoomProperty("connectionLimit", connectionLimit);
            props.AddRoomProperty("roomIntro", roomIntro);
            
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

        RoomListScrollView roomList = GameObject.Find("RoomList").GetComponentInChildren<RoomListScrollView>();
        roomList.ClearList();

        foreach (var session in sessionList)
        {
            UdpSession udpSession = session.Value as UdpSession;
            PhotonSession photonSession = udpSession as PhotonSession;

            string rm = null;
            string ri = null;

            if(photonSession.Properties.ContainsKey("roomName"))
            {
                object Value = photonSession.Properties["roomName"];
                rm = Value.ToString();
            }
            if (photonSession.Properties.ContainsKey("password"))
            {
                object Value = photonSession.Properties["password"];
            }
            if (photonSession.Properties.ContainsKey("connectionLimit"))
            {
                object Value = photonSession.Properties["connectionLimit"];
            }
            if (photonSession.Properties.ContainsKey("roomIntro"))
            {
                object Value = photonSession.Properties["roomIntro"];
                ri = Value.ToString();
            }

            /*
            if (udpSession.HostName.Equals(roomName))
            {
                if (udpSession.Source == UdpSessionSource.Photon)
                {
                    BoltMatchmaking.JoinSession(udpSession);
                }
            }
            */
			roomList.WhenRoomCreated(rm,ri);
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

    public void ShutDown()
    {
        BoltLauncher.Shutdown();
    }


    public void JoinRoom(string pw)
    {
        authenticationToken at = new authenticationToken();
        at.roomName = roomName;
        at.password = pw;
        BoltMatchmaking.JoinSession(roomName,(IProtocolToken)at);
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
