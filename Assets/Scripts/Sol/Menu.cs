using System;
using UnityEngine;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;

public class Menu : GlobalEventListener
{
    string RoomName;

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));

        RoomName = GUILayout.TextField(RoomName);

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

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            //string matchName = Guid.NewGuid().ToString();
            
            BoltMatchmaking.CreateSession(
                sessionID: RoomName,
                sceneToLoad: "Sol"
            );
        }
    }
    
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);
        
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.HostName.Equals(RoomName))
            {
                if (photonSession.Source == UdpSessionSource.Photon)
                {
                    BoltMatchmaking.JoinSession(photonSession);
                }
            }
        }
        
    }
}