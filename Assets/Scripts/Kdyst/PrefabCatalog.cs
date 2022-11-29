using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct KeyValue
{
    public int key;
    public GameObject gameObject;
}

public class PrefabCatalog : MonoBehaviour
{
    [SerializeField] private List<KeyValue> prefabs = new();
    private readonly Dictionary<int, GameObject> mapper = new();

    public GameObject Find(int key)
    {
        return mapper[key];
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(var kv in prefabs)
        {
            try
            {
                mapper.Add(kv.key, kv.gameObject);
            }
            catch(ArgumentException)
            {
                Debug.LogError("kdyst/ObjectManager.cs: The key " + kv.key + " alreay exists.");
            }
        }
    }

    public bool actionSpawn = false;
    public int actionSpawnKey;

    // Update is called once per frame
    void Update()
    {
        if(actionSpawn)
        {
            actionSpawn = false;
            Instantiate(this.Find(actionSpawnKey), gameObject.transform);
        }
    }
}
