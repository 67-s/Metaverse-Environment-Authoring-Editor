using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenScript : BuilderBase
{
    [SerializeField] private int wallPrefab = 40;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.one * unit / 14;
        
        for(int x = 0; x < xWidth; x++)
        {
            Spawn(wallPrefab, new Vector3(14 * x + 7, 0, 1), 0);
            Spawn(wallPrefab, new Vector3(14 * x + 7, 0, 14 * zWidth - 1), 180);
        }
        
        for (int z = 0; z < zWidth; z++)
        {
            Spawn(wallPrefab, new Vector3(1, 0, 14 * z + 7), 90);
            Spawn(wallPrefab, new Vector3(14 * xWidth - 1, 0, 14 * z + 7), 270);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
