using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using UnityEngine;

[Serializable]
struct KeyValue
{
    public int key;
    public GameObject gameObject;
}

public class PrefabCatalog : MonoBehaviour
{
    [SerializeField] private List<GameObject> childCatalogs;
    [SerializeField] private List<KeyValue> prefabs = new();
    private readonly Dictionary<int, GameObject> mapper = new();
    public Dictionary<int, GameObject>.KeyCollection Keys 
    {
        get
        {
            Init();
            return mapper.Keys;
        }
    }
    /*
     * Init(): initializer
     */
    private bool isModified = false;

    public void Init()
    {
        if (!isModified)
        {
            isModified = true;

            foreach (var kv in prefabs)
            {
                try
                {
                    mapper.Add(kv.key, kv.gameObject);
                }
                catch (ArgumentException)
                {
                    Debug.LogError("kdyst/ObjectManager.cs: The key " + kv.key + " alreay exists.");
                }
            }

            foreach(var obj in childCatalogs)
            {
                if (obj == null) continue;
                var catalog = obj.GetComponent<PrefabCatalog>();
                foreach(int key in catalog.Keys)
                {
                    try
                    {
                        mapper.Add(key, catalog.Find(key));
                    }
                    catch(ArgumentException)
                    {
                        Debug.LogError("kdyst/ObjectManager.cs: The key " + key + " crashes with another catalog.");
                    }
                }
            }
        }
    }

    public GameObject Find(int key)
    {
        Init();
        return mapper[key];
    }

    // Start is called before the first frame update
    void Start()
    {
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
