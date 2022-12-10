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
    PrefabCatalog prefabList;
    List<Material> materialList = new List<Material>();
    IndexToMaterial colorObject;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        base.SceneLoadLocalDone(scene, token);
        prefabList = GameObject.Find("Prefab Catalog").GetComponent<PrefabCatalog>();
        colorObject = GameObject.Find("IndexToMaterial").GetComponent<IndexToMaterial>();
        //캐릭터 생성 부분
        var spawnPosition = new Vector3(Random.Range(-8, 8), 0.0f, Random.Range(-8, 8));

        switch (CharacterModelCamMove.GetCharacterIdx())
        {
            case 0:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_01, spawnPosition, Quaternion.identity);
                break;
            case 1:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_02, spawnPosition, Quaternion.identity);
                break;
            case 2:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_03, spawnPosition, Quaternion.identity);
                break;
            case 3:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_04, spawnPosition, Quaternion.identity);
                break;
            case 4:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_05, spawnPosition, Quaternion.identity);
                break;
            case 5:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_06, spawnPosition, Quaternion.identity);
                break;
            case 6:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_07, spawnPosition, Quaternion.identity);
                break;
            case 7:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_08, spawnPosition, Quaternion.identity);
                break;
            case 8:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_09, spawnPosition, Quaternion.identity);
                break;
            case 9:
                BoltNetwork.Instantiate(BoltPrefabs.Ch_10, spawnPosition, Quaternion.identity);
                break;

        }

        MapInfoToken mt = token as MapInfoToken;

        if (mt != null)
        {
            Debug.Log("+++++++"+mt.mapInfos.Length);
            for (int i = 0; i < mt.mapInfos.Length; i += 30)
            {
                byte prefab = mt.mapInfos[i];
                byte[] bytesX = { mt.mapInfos[i+1], mt.mapInfos[i + 2], mt.mapInfos[i + 3], mt.mapInfos[i + 4] };
                byte[] bytesY = { mt.mapInfos[i+5], mt.mapInfos[i + 6], mt.mapInfos[i + 7], mt.mapInfos[i + 8] };
                byte[] bytesZ = { mt.mapInfos[i+9], mt.mapInfos[i + 10], mt.mapInfos[i + 11], mt.mapInfos[i + 12] };
                byte[] bytesRX = { mt.mapInfos[i + 13], mt.mapInfos[i + 14], mt.mapInfos[i + 15], mt.mapInfos[i + 16] };
                byte[] bytesRY = { mt.mapInfos[i + 17], mt.mapInfos[i + 18], mt.mapInfos[i + 19], mt.mapInfos[i + 20] };
                byte[] bytesRZ = { mt.mapInfos[i + 21], mt.mapInfos[i + 22], mt.mapInfos[i + 23], mt.mapInfos[i + 24] };
                byte[] bytesRW = { mt.mapInfos[i + 25], mt.mapInfos[i + 26], mt.mapInfos[i + 27], mt.mapInfos[i + 28] };
                byte color = mt.mapInfos[i + 29];

                float x = System.BitConverter.ToSingle(bytesX, 0);
                float y = System.BitConverter.ToSingle(bytesY, 0);
                float z = System.BitConverter.ToSingle(bytesZ, 0);
                float rx = System.BitConverter.ToSingle(bytesRX, 0);
                float ry = System.BitConverter.ToSingle(bytesRY, 0);
                float rz = System.BitConverter.ToSingle(bytesRZ, 0);
                float rw = System.BitConverter.ToSingle(bytesRW, 0);

                var position = new Vector3(x,y,z) * 3.0f;
                var rotation = new Quaternion(rx, ry, rz,rw);

                GameObject instance = Instantiate(prefabList.Find(prefab), position, rotation);
                if(instance.name.Equals("SM_School_Env_Floor10(Clone)") || instance.name.Equals("SM_School_Env_Floor2(Clone)"))
                    instance.GetComponent<Transform>().localScale = new Vector3(5.0f, 5.0f, 5.0f);
                else
                    instance.GetComponent<Transform>().localScale = new Vector3(3.0f, 3.0f, 3.0f);
                if (color != 11)
                {
                    materialList.Clear();
                    materialList.Add(colorObject.IndexToColor(color));
                    instance.GetComponent<Renderer>().materials = materialList.ToArray();
                }
                Destroy(instance.GetComponent<PrefabObj>());
                
            }
            
        }
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