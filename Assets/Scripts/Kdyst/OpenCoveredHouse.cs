using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCoveredHouse : BuilderBase
{
    [SerializeField] private int wallPrefab = 10;
    [SerializeField] private int cornerPrefab = 11;
    [SerializeField] private int floorPrefab = 12;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.one * unit / 6;
        //spawn corner
        Spawn(cornerPrefab, 6 * new Vector3(xWidth, 0, 0), 0);
        Spawn(cornerPrefab, 6 * new Vector3(0, 0, 0), 90);
        Spawn(cornerPrefab, 6 * new Vector3(0, 0, zWidth), 180);
        Spawn(cornerPrefab, 6 * new Vector3(xWidth, 0, zWidth), 270);

        //spawn wall x
        for(int x = 1; x < 2 * xWidth; x++)
        {
            Spawn(wallPrefab, new Vector3(3 * x, 0, 0), 0);
            Spawn(wallPrefab, new Vector3(3 * x, 0, 6 * zWidth), 180);
        }

        //spawn wall z
        for(int z = 1; z < 2 * zWidth; z++)
        {
            Spawn(wallPrefab, new Vector3(0, 0, 3 * z), 90);
            Spawn(wallPrefab, new Vector3(6 * xWidth, 0, 3 * z), 270);
        }

        //spawn tile
        for (int x = 0; x < xWidth; x++)
            for (int z = 0; z < zWidth; z++)
                Spawn(floorPrefab, new Vector3(
                    3f + 6 * x,
                    0.01f,
                    3f + 6 * z), 2 * Vector3.one, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
