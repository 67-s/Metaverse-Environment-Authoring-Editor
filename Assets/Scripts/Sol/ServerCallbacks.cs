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
    public string password;
    public int limit;
    public int currentPlayerNumber;

    public override void Connected(BoltConnection connection)
    {
        Debug.Log("connected!");
        ++currentPlayerNumber;
    }

    public override void Disconnected(BoltConnection connection)
    {
        /*
        var log = LogEvent.Create();
        log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
        log.Send();
        */
        Debug.Log("disconnected!");
        --currentPlayerNumber;
    }

    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
    {
        base.ConnectRequest(endpoint, token);
        Debug.Log("connect request!!!");
        authenticationToken at = token as authenticationToken;

        if (password.Equals(at.password) && currentPlayerNumber < limit)
        {
            BoltNetwork.Accept(endpoint);
            Debug.Log("accept!");
        }
        else
        {
            BoltNetwork.Refuse(endpoint);
            Debug.Log("refuse!");
            Debug.Log(password.Length + ";" + at.password.Length);
        }
    }

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        base.SceneLoadLocalDone(scene, token);
    }

    public override void SessionCreatedOrUpdated(UdpSession session)
    {
        base.SessionCreatedOrUpdated(session);
        PhotonSession photonSession = session as PhotonSession;
        password = photonSession.Properties["password"].ToString();
        limit = int.Parse(photonSession.Properties["connectionLimit"].ToString());
        currentPlayerNumber = 1;
    }
}