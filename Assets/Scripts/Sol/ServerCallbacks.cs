using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using UdpKit;
using Photon.Bolt.Utils;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : GlobalEventListener
{
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
        BoltNetwork.Accept(endpoint);
    }

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        base.SceneLoadLocalDone(scene, token);

        //맵 생성 부분

        var mt = token as MapInfoToken;
        /*
        var cubePosition = new Vector3(mt.mapInfos[0], mt.mapInfos[1], mt.mapInfos[2]);

        BoltNetwork.Instantiate(BoltPrefabs.Cube, cubePosition, Quaternion.identity);
        */

        if (mt == null)
            return;

        Debug.Log(mt.mapInfos[0]);
        Debug.Log(mt.mapInfos[1]);
        Debug.Log(mt.mapInfos[2]);
        Debug.Log(mt.mapInfos[3]);

        for (int i = 0; i < 2; ++i)
        {
            var cubePosition = new Vector3(mt.mapInfos[2*i+0],0, mt.mapInfos[2*i+1]);

            BoltNetwork.Instantiate(BoltPrefabs.Cube, cubePosition, Quaternion.identity);
        }


    }
}