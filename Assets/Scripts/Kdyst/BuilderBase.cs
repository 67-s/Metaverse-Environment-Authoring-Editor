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

    [SerializeField] protected EBuildDirection direction;
    [SerializeField] protected int seed;

    /*
     * This is the list of GameObjects spawned by BuilderBase.Spawn().
     * It will be returned;
     */
    private readonly List<GameObject> ingredients = new();
    public IList<GameObject> Ingredients { 
        get {
            return ingredients.AsReadOnly();
        }
    }
    
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

    public BuilderBase SetDirection(EBuildDirection direction)
    {
        this.direction = direction;
        return this;
    }

    public BuilderBase SetSeed(int seed)
    {
        this.seed = seed;
        return this;
    }

    /*
     * Spawn(): copy $(prefab) and place on $(position) from BuilderBase.
     * The object will be managed in arrays.
     */
    protected void Spawn(GameObject prefab, Vector3 position, float angle)
    {
        GameObject target = Instantiate(prefab, transform);
        target.transform.localPosition = position;
        target.transform.Rotate(Vector3.up, angle);
        ingredients.Add(target);
    }
}
