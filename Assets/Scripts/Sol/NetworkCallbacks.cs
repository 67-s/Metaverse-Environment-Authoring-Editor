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
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    /*
    void OnGUI()
    {
        // only display max the 5 latest log messages
        int maxMessages = Mathf.Min(5, logMessages.Count);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
        
        for (int i = 0; i < maxMessages; ++i)
        {
            GUILayout.Label(logMessages[i]);
        }

        GUILayout.EndArea();
    }
    */
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
            for (int i = 0; i < mt.mapInfos.Length; i += 12)
            {
                  
                byte[] bytesX = { mt.mapInfos[i], mt.mapInfos[i + 1], mt.mapInfos[i + 2], mt.mapInfos[i + 3] };
                byte[] bytesY = { mt.mapInfos[i+4], mt.mapInfos[i + 5], mt.mapInfos[i + 6], mt.mapInfos[i + 7] };
                byte[] bytesZ = { mt.mapInfos[i+8], mt.mapInfos[i + 9], mt.mapInfos[i + 10], mt.mapInfos[i + 11] };

                float x = System.BitConverter.ToSingle(bytesX, 0);
                float y = System.BitConverter.ToSingle(bytesY, 0);
                float z = System.BitConverter.ToSingle(bytesZ, 0);

                var cubePosition = new Vector3(x,y,z);

                Instantiate(prefab1, cubePosition, Quaternion.identity);

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