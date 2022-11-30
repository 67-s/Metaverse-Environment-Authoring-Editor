using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UdpKit;
using Photon.Bolt.Utils;

//[BoltGlobalBehaviour]
public class NetworkCallbacks : GlobalEventListener
{
    List<string> logMessages = new List<string>();
    public List<GameObject> prefabs;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        base.SceneLoadLocalDone(scene, token);

        //캐릭터 생성 부분
        
        var spawnPosition = new Vector3(Random.Range(-8, 8), 0.0f, Random.Range(-8, 8));

        BoltNetwork.Instantiate(BoltPrefabs.Ch_01, spawnPosition, Quaternion.identity);

        MapInfoToken mt = token as MapInfoToken;

        if (mt != null)
        {
            Debug.Log("+++++++"+mt.mapInfos.Length);
            for (int i = 0; i < mt.mapInfos.Length; i += 29)
            {
                byte prefab = mt.mapInfos[i];  
                byte[] bytesX = { mt.mapInfos[i+1], mt.mapInfos[i + 2], mt.mapInfos[i + 3], mt.mapInfos[i + 4] };
                byte[] bytesY = { mt.mapInfos[i+5], mt.mapInfos[i + 6], mt.mapInfos[i + 7], mt.mapInfos[i + 8] };
                byte[] bytesZ = { mt.mapInfos[i+9], mt.mapInfos[i + 10], mt.mapInfos[i + 11], mt.mapInfos[i + 12] };
                byte[] bytesRX = { mt.mapInfos[i + 13], mt.mapInfos[i + 14], mt.mapInfos[i + 15], mt.mapInfos[i + 16] };
                byte[] bytesRY = { mt.mapInfos[i + 17], mt.mapInfos[i + 18], mt.mapInfos[i + 19], mt.mapInfos[i + 20] };
                byte[] bytesRZ = { mt.mapInfos[i + 21], mt.mapInfos[i + 22], mt.mapInfos[i + 23], mt.mapInfos[i + 24] };
                byte[] bytesRW = { mt.mapInfos[i + 25], mt.mapInfos[i + 26], mt.mapInfos[i + 27], mt.mapInfos[i + 28] };

                float x = System.BitConverter.ToSingle(bytesX, 0);
                float y = System.BitConverter.ToSingle(bytesY, 0);
                float z = System.BitConverter.ToSingle(bytesZ, 0);
                float rx = System.BitConverter.ToSingle(bytesRX, 0);
                float ry = System.BitConverter.ToSingle(bytesRY, 0);
                float rz = System.BitConverter.ToSingle(bytesRZ, 0);
                float rw = System.BitConverter.ToSingle(bytesRW, 0);

                var position = new Vector3(x,y,z);
                var rotation = new Quaternion(rx, ry, rz,rw);

                switch(prefab)
                {
                    case 0:
                        Instantiate(prefabs[0], position, rotation);
                        break;
                    case 100:
                        Instantiate(prefabs[1], position, rotation);
                        break;
                    case 101:
                        Instantiate(prefabs[2], position, rotation);
                        break;
                    case 102:
                        Instantiate(prefabs[3], position, rotation);
                        break;
                    case 200:
                        Instantiate(prefabs[4], position, rotation);
                        break;
                    case 201:
                        Instantiate(prefabs[5], position, rotation);
                        break;
                    case 202:
                        Instantiate(prefabs[6], position, rotation);
                        break;
                    default:
                        Instantiate(prefabs[7], position, rotation);
                        break;
                }
                /*
                if(mt.mapInfos[i] == 0)
                    Instantiate(prefab1, cubePosition, Quaternion.identity);
                else if (mt.mapInfos[i] == 1)
                    Instantiate(prefab2, cubePosition, Quaternion.identity);
                else
                    Instantiate(prefab3, cubePosition, Quaternion.identity);
                */
            }
            
        }
    }

    public override void OnEvent(LogEvent evnt)
    {
        logMessages.Insert(0, evnt.Message);
    }
   
    public override void ConnectRefused(UdpEndPoint endpoint, IProtocolToken token)
    {
        base.ConnectRefused(endpoint, token);
        Application.Quit();
    }

    public override void ConnectAttempt(UdpEndPoint endpoint, IProtocolToken token)
    {
        base.ConnectAttempt(endpoint, token);
    }
    /*
    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
    {
        base.ConnectRequest(endpoint, token);
        BoltNetwork.Accept(endpoint);
    }*/
}