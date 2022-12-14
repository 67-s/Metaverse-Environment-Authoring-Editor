using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Bson;
using JetBrains.Annotations;

public class Installer : MonoBehaviour
{
    private const int NONE = -1;
    /*
     * Belows are the variables
     *  which are related to tiles of the world.
     */
    //default tile: It means that the tile data is broken.
    [SerializeField]
    private GameObject tileDefault;

    //the sequence of the tiles
    [SerializeField]
    private GameObject[] tileSet;

    //The length of one side of tile.
    [SerializeField]
    [Range(0, 100)]
    private float tileLength;

    //The number of rows/columns of the map
    [Range(0, 33)]
    public int defaultXSize, defaultZSize;
    
    //This is the map.
    private GameObject[,] tileMap = null;

    /*
     * The variables of the Building.
     */
    [SerializeField]
    private GameObject[] buildSet;

    //The sparse table of buildings
    private readonly Dictionary<int, (int x, int z, int xWidth, int zWidth, int seed, GameObject gameObject)> buildList = new();
    private int buildListID = 0;

    //Maps for locating buildings
    private int[,] buildMap = null;

    //Count of tiles and buildings
    public int NumberOfTileTypes => tileSet.Length;
    public int NumberOfBuildingTypes => buildSet.Length;



    //Make a clone of the gameObject and place it
    GameObject CloneTile(int x, int z, int key)
    {
        GameObject tile = (0 <= key && key < tileSet.Length) ? tileSet[key] : tileDefault;
        GameObject target = Instantiate(tile, gameObject.transform);
        Vector3 vec = new Vector3(x, 0, z) * tileLength;
        target.transform.localScale = tileLength * target.transform.localScale;
        target.transform.Translate(vec);
        target.SetActive(true);
        return target;
    }

    //Resize the map(may be the map will shrink or expand)
    public void Resize(int xSize, int zSize, int key = 0)
    {
        if(xSize <= 0 || zSize <= 0)
        {
            Debug.Log("kdyst/Installer.cs: Installer.Resize(): (rowSize, colSize) == (" + xSize + ", " + zSize + ") is not proper.");
            return;
        }

        //clear all the previous buildings
        buildMap = new int[xSize, zSize];
        buildListID = 0;
        foreach (var elem in buildList)
            Destroy(elem.Value.gameObject);
        buildList.Clear();

        //ready to allocate new tiles
        GameObject[,] temporary = new GameObject[xSize, zSize];
        tileMap ??= new GameObject[0, 0];
        
        //loop for each new tiles.
        for(int x = 0; x < xSize; x++)
            for(int z = 0; z < zSize; z++)
            {
                buildMap[x, z] = NONE;
                temporary[x, z] = CloneTile(x, z, key);
            }
        foreach (var gameObject in tileMap)
            Destroy(gameObject);
        (_, tileMap) = (tileMap, temporary);
    }

    // Verify that it points to the correct space
    private bool Verify(int x, int z, int xWidth, int zWidth)
    {
        if (x < 0 || z < 0
            || xWidth <= 0 || zWidth <= 0
            || x + xWidth > tileMap.GetLength(0) || z + zWidth > tileMap.GetLength(1))
        {
            static string foo(int a, int b) => "(" + a + ", " + b + ")";
            string front = "kdyst/Installer.cs: Installer.Verify():";
            Debug.Log(front
                + " (x, z) == " + foo(x, z)
                + ", (xWidth, zWidth) == " + foo(xWidth, zWidth)
                + "(xSize, ySize) == " + foo(tileMap.GetLength(0), tileMap.GetLength(1))
                );
            return false;
        }
        return true;
    }

    // Change the tile data
    public bool ChangeTile(int x, int z, int xWidth, int zWidth, int key)
    {
        //exceptions
        if (!Verify(x, z, xWidth, zWidth))
            return false;

        for(int xDiff = 0; xDiff < xWidth; xDiff++)
            for (int zDiff = 0; zDiff < zWidth; zDiff++)
            {
                GameObject temp = tileMap[x + xDiff, z + zDiff];
                tileMap[x + xDiff, z + zDiff] = CloneTile(x + xDiff, z + zDiff, key);
                Destroy(temp);
            }
        return true;
    }

    //construct new building
    public bool Build(int x, int z, int xWidth, int zWidth, int key)
    {
        //exceptions
        if (!Verify(x, z, xWidth, zWidth))
            return false;
        for(int dx = 0; dx < xWidth; dx++)
            for(int dz = 0; dz < zWidth; dz++)
                if (buildMap[x + dx, z + dz] != NONE)
                {
                    string front = "kdyst/Installer.cs: Installer.Build():";
                    static string foo(int a, int b) => "(" + a + ", " + b + ")";
                    Debug.Log(front + "There is an another building in " + foo(x + dx, z + dz) + ", the index " + buildMap[x + dx, z + dz]);
                    return false;
                }

        if (0 <= key && key < buildSet.Length)
        {
            //get seed
            int seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

            //spawn building
            GameObject target = Instantiate(buildSet[key], gameObject.transform);
            target.GetComponent<BuilderBase>()
                .SetUnit(tileLength)
                .SetGrid(xWidth, zWidth)
                .SetSeed(seed)
                .SetPrefabCatalog(GameObject.Find("Prefab Catalog").GetComponent<PrefabCatalog>());
            target.transform.Translate(new Vector3(x, 0, z) * tileLength);
            target.SetActive(true);

            //write on map and register
            for (int dx = 0; dx < xWidth; dx++)
                for (int dz = 0; dz < zWidth; dz++)
                    buildMap[x + dx, z + dz] = buildListID;
            buildList.Add(buildListID++, (x, z, xWidth, zWidth, seed, target));
        }
        return true;
    }

    //remove all buildings in the area.(fails <==> ret < 0) (not found <==> ret == 0) (success <==> ret > 0)
    public int Remove(int x, int z, int xWidth, int zWidth)
    {
        //exceptions
        if (!Verify(x, z, xWidth, zWidth))
            return -1;
        int count = 0;
        for(int dx = 0; dx < xWidth; dx++)
            for(int dz = 0; dz < zWidth; dz++)
                if (buildMap[x + dx, z + dz] != NONE)
                {
                    int key = buildMap[x + dx, z + dz];
                    var elem = buildList[key];
                    Destroy(elem.gameObject);
                    for (int i = elem.x; i < elem.x + elem.xWidth; i++)
                        for (int j = elem.z; j < elem.z + elem.zWidth; j++)
                            buildMap[i, j] = NONE;
                    buildList.Remove(key);
                    count++;
                }
        Debug.Log("kdyst/Installer.cs: Installer.Remove(): remove " + count + " building(s).");
        return count;
    }


    /*
     * Manage Additional GameObject In this field
     */
    
    //Additional Data of Objects
    private readonly Dictionary<int, CreationData> additionalData = new();
    private int additionalDataID = 0;
    public int AdditionalData_Add(CreationData creationData)
    {
        additionalData.Add(additionalDataID, creationData);
        return additionalDataID++;
    }
    public void AdditionalData_Remove(int key)
    {
        if (additionalData.ContainsKey(key))
            additionalData.Remove(key);
    }


    // extract all of the CreationData.
    public List<CreationData> ExtractData()
    {
        List<CreationData> list = new();

        //extract copied objects from buildings
        foreach (var KeyValue in buildList)
            list.AddRange(KeyValue.Value.gameObject.GetComponent<BuilderBase>().CreationDatas);

        list.AddRange(additionalData.Values);//additional data

        return list;
    }


    // Start is called before the first frame update
    void Start()
    {
        if(tileMap == null)
            Resize(defaultXSize, defaultZSize, 1);
    }

    public bool actionResize = false;
    [Range(0, 33)]
    public int actionResizeXSize, actionResizeZSize;
    public int actionResizeKey = 0;

    public bool actionChangeTile = false;
    public int actionChangeTileX, actionChangTileZ;
    public int actionChangeTileXWidth, actionChangTileZWidth;
    public int actionChangeTileKey;

    public bool actionBuild = false;
    public int actionBuildX, actionBuildZ;
    public int actionBuildXWidth, actionBuildZWidth;
    public int actionBuildKey;

    public bool actionRemove = false;
    public int actionRemoveX, actionRemoveZ;
    public int actionRemoveXWidth, actionRemoveZWidth;

    public bool actionInclude = false;
    public GameObject actionIncludeObject;

    public bool actionExclude = false;
    public int actionExcludeKey;

    public bool actionExtract = false;

    // Update is called once per frame
    void Update()
    {
        if (actionResize)
        {
            actionResize = false;
            Resize(actionResizeXSize, actionResizeZSize, actionResizeKey);
        }
        if(actionChangeTile)
        {
            actionChangeTile = false;
            ChangeTile(actionChangeTileX, actionChangTileZ, actionChangeTileXWidth, actionChangTileZWidth, actionChangeTileKey);
        }
        if(actionBuild)
        {
            actionBuild = false;
            Build(actionBuildX, actionBuildZ, actionBuildXWidth, actionBuildZWidth, actionBuildKey);
        }
        if(actionRemove)
        {
            actionRemove = false;
            Remove(actionRemoveX, actionRemoveZ, actionRemoveXWidth, actionRemoveZWidth);
        }
        if (actionInclude)
        {
            actionInclude = false;
            int key = AdditionalData_Add(new CreationData() { Target = actionIncludeObject, Origin = -additionalDataID });
            Debug.Log("Kdyst/Installer.cs: Testmode ActionInclude read (key = " + key + ")");
        }
        if(actionExclude)
        {
            actionExclude = false;
            AdditionalData_Remove(actionExcludeKey);
        }
        if(actionExtract)
        {
            actionExtract = false;
            foreach(var cd in ExtractData())
            {
                Debug.Log("Kdyst/Installer.cs: Extract Mode read (Origin = " + cd.Origin + ", pos.x = " + cd.Target.transform.position.x + ")");
            }
        }
    }
}
