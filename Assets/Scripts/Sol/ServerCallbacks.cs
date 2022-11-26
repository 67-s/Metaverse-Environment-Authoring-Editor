using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using UdpKit;
using Photon.Bolt.Utils;
using UdpKit.Platform.Photon;


[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : GlobalEventListener
{
    //public Map<string, string> roomNameToPassword = new Map<string,string>();
    public Dictionary<string, string> roomNameToPassword = new Dictionary<string, string>();
    //public string roomName;
    //public string password;

    public override void Connected(BoltConnection connection)
    {
        var log = LogEvent.Create();
        log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
        log.Send();
    }

    public override void Disconnected(BoltConnection connection)
    {
        var log = LogEvent.Create();
        log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
        log.Send();
    }
    
    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
    {
        base.ConnectRequest(endpoint, token);
        Debug.Log("connect request!!!");
        authenticationToken at = token as authenticationToken;

        //if(roomName.Equals(at.roomName) && password.Equals(at.password))
        if (roomNameToPassword[at.roomName].Equals(at.password))
            BoltNetwork.Accept(endpoint);
        

        BoltNetwork.Refuse(endpoint);
    }

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        base.SceneLoadLocalDone(scene, token);

        //맵 생성 부분

        /*
        MapInfoToken mt = token as MapInfoToken;

        if (mt != null)
        {
            for (int i = 0; i < 100; ++i)
            {
                var cubePosition = new Vector3(mt.mapInfos[i*2], 0.0f, mt.mapInfos[i*2+1]);
                BoltNetwork.Instantiate(BoltPrefabs.Cube, cubePosition, Quaternion.identity);
            }
        }
        */
    }

    public override void SessionCreatedOrUpdated(UdpSession session)
    {
        base.SessionCreatedOrUpdated(session);
        PhotonSession photonSession = session as PhotonSession;
        string rm = photonSession.Properties["roomName"].ToString();
        string pw = photonSession.Properties["password"].ToString();

        roomNameToPassword.Add(rm, pw);
    }
}