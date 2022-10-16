using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tileSet;

    public SerializableMatrix<int> tileMap;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < tileMap.rows.Count; x++)
            for(int z = 0; z < tileMap.rows[0].values.Count; z++)
            {
                GameObject target = Instantiate(tileSet[tileMap[x, z]], gameObject.transform);
                target.transform.Translate(Vector3.right * x + Vector3.forward * z);
                target.SetActive(true);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
