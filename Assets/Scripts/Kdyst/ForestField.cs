using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestField : BuilderBase
{
    private static readonly int[] treeSet = {200, 201, 202};
    [SerializeField] private int numberOfTreePerTile = 3;

    // Start is called before the first frame update
    void Start()
    {
        static int gcd(int x, int y)
        {
            while (y > 0)
            {
                int r = x % y;
                (x, y) = (y, r);
            }
            return x;
        }
        static float randf(System.Random random)
        {
            return (float)random.NextDouble();
        }
        gameObject.transform.localScale = Vector3.one * unit / 10;
        System.Random random = new(seed);
        
        int totalTreeCount = numberOfTreePerTile * xWidth * zWidth + random.Next(1);
        int gcdWidth = gcd(xWidth, zWidth);
        int xDiv = xWidth / gcdWidth, zDiv = zWidth / gcdWidth;
        while(xDiv * zDiv < totalTreeCount)
        {
            xDiv *= 2;
            zDiv *= 2;
        }

        int areaCount = xDiv * zDiv;
        for(int x = 0; x < xDiv; x++)
        {
            for(int z = 0; z < zDiv; z++, areaCount--)
            {
                if (totalTreeCount == 0)
                    return;
                if(random.Next(areaCount) < totalTreeCount)
                {
                    totalTreeCount--;
                    //spawning object randomly in (xDiv, zDiv)
                    float xConst = 10f * xWidth / xDiv;
                    float zConst = 10f * zWidth / zDiv;
                    var target = treeSet[random.Next(treeSet.Length)];
                    Spawn(target,
                        new Vector3(
                                xConst * (x + randf(random)),
                                0,
                                zConst * (z + randf(random))
                            ),
                            360 * randf(random));
                }
            }
        }
        if(totalTreeCount > 0)
        {
            Debug.Log("kdyst/ForestField.cs: totalTreeCount(" + totalTreeCount + ")"); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
