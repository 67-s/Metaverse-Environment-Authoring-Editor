using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumScript : BuilderBase
{
    [SerializeField] private int[] benchPrefabs = { 50, 51 };
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.one * unit / 16;

        for (int x = 1; x < 2 * xWidth; x++)
        {
            Spawn(benchPrefabs[0], new Vector3(8 * x, 0, 2), 0);
            Spawn(benchPrefabs[0], new Vector3(8 * x, 0, 16 * zWidth - 2), 180);
        }

        for (int z = 1; z < 2 * zWidth; z++)
        {
            Spawn(benchPrefabs[1], new Vector3(2, 0, 8 * z), 90);
            Spawn(benchPrefabs[1], new Vector3(16 * xWidth - 2, 0, 8 * z), 270);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
