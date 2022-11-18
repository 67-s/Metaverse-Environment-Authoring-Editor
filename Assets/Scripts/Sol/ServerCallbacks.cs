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
}