using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UdpKit;
using Photon.Bolt.Utils;

[BoltGlobalBehaviour]
public class NetworkCallbacks : GlobalEventListener
{
    List<string> logMessages = new List<string>();
    List<string> infos = new List<string>();

    void OnGUI()
    {
        // only display max the 5 latest log messages
        int maxMessages = Mathf.Min(5, logMessages.Count);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
        
        for (int i = 0; i < maxMessages; ++i)
        {
            GUILayout.Label(logMessages[i]);
        }

        for (int i = 0; i < infos.Count; ++i)
            GUILayout.Label(infos[i]);

        GUILayout.EndArea();
    }
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        base.SceneLoadLocalDone(scene, token);
        // randomize a position
        
        var spawnPosition = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));

        // instantiate Person
        BoltNetwork.Instantiate(BoltPrefabs.Person, spawnPosition, Quaternion.identity);
    }

    public override void OnEvent(LogEvent evnt)
    {
        logMessages.Insert(0, evnt.Message);
    }

    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
    {
        base.ConnectRequest(endpoint, token);
        BoltNetwork.Accept(endpoint);
        infos.Add(endpoint.ToString());
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
}