using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : BuilderBase
{
    private const int floorPrefab = 300;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.one * unit / 6;
        //spawn tile
        for (int x = 0; x < 2 * xWidth; x++)
            for (int z = 0; z < 2 * zWidth; z++)
                Spawn(floorPrefab, new Vector3(1.5f + 3 * x, 0.01f, 1.5f + 3 * z), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
