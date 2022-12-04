using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuilderBase : MonoBehaviour
{
    /*
     * These are properties of the BuilderBase.
     * They will be initialized by Installer
     * , and be used by it's child classes.
     */
    [SerializeField] protected float unit;
    [SerializeField] protected int xWidth;
    [SerializeField] protected int zWidth;

    [SerializeField] protected int seed;

    /*
     * Object Management: Contains prefab.
     * Finds them using int32 type key.
     */
    private PrefabCatalog prefabCatalog = null;
    private readonly List<CreationData> creationDatas = new();
    public IList<CreationData> CreationDatas => creationDatas.AsReadOnly();

    /*
     * These are setter of properties.
     * They returns the BuilderBase itself.
     */
    public BuilderBase SetUnit(float unit)
    {
        this.unit = unit;
        return this;
    }

    public BuilderBase SetGrid(int x, int z)
    {
        this.xWidth = x;
        this.zWidth = z;
        return this;
    }


    public BuilderBase SetSeed(int seed)
    {
        this.seed = seed;
        return this;
    }

    public BuilderBase SetPrefabCatalog(PrefabCatalog prefabCatalog)
    {
        this.prefabCatalog = prefabCatalog;
        return this;
    }


    /*
     * Spawn(): copy $(prefab) and place on $(position) from BuilderBase.
     * The object will be managed in arrays.
     */
    protected void Spawn(int key, Vector3 position, float angle)
    {
        Spawn(key, position, Vector3.one, angle);
    }

    protected void Spawn(int key, Vector3 position, Vector3 scale, float angle)
    {
        GameObject prefab = prefabCatalog.Find(key);
        GameObject target = Instantiate(prefab, transform);
        target.transform.localPosition = position;
        target.transform.localScale = scale;
        target.transform.Rotate(Vector3.up, angle);

        /*insert data*/
        CreationData data = new()
        {
            Target = target,
            Origin = key
        };
        creationDatas.Add(data);
    }
}
