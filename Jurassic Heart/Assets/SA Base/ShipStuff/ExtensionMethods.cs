using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods 
{
    public static void DestroyAllChildren(this Transform obj)
    {
        int numChildren = obj.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            UnityEngine.Object.Destroy(obj.GetChild(i).gameObject);
        }
    }
    public static void SetAllChildrenActive(this GameObject obj, bool value)
    {
        obj.SetActive(value);
        for(int i=0; i<obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetAllChildrenActive(true);
        }
    }
    //Returns true if we keep it
    public delegate bool FilterClause(GameObject g);
    public static Bounds MakeBoundingBoxForObjectColliders(this GameObject rootObject, bool includeInactive = false, FilterClause filter = null)
    {
        Collider[] colliders = rootObject.GetComponentsInChildren<Collider>(includeInactive);
        Debug.Log("colliders.length: " + colliders.Length);
        if (colliders.Length == 0)
        {
            return new Bounds(rootObject.transform.position, Vector3.zero);
        }

        Bounds bounds = new Bounds(colliders[0].bounds.center, colliders[0].bounds.size);
        foreach (Collider c in colliders)
        {
            if(filter == null || filter(c.gameObject))
                bounds.Encapsulate(c.bounds);
        }
        return bounds;
    }
    public static Bounds MakeBoundingBoxForObjectRenderers(this GameObject rootObject, bool includeInactive = false, FilterClause filter = null)
    {
        Renderer[] renderers = rootObject.GetComponentsInChildren<Renderer>(includeInactive);
        Debug.Log("renderers.length: " + renderers.Length);
        if (renderers.Length == 0)
        {
            return new Bounds(rootObject.transform.position, Vector3.zero);
        }

        Bounds bounds = new Bounds(renderers[0].bounds.center, renderers[0].bounds.size);
        foreach (Renderer r in renderers)
        {
            if(filter == null || filter(r.gameObject))
                bounds.Encapsulate(r.bounds);
        }
        return bounds;
    }
    public static bool ContainsAnyPoint(this Bounds boundsToCheck, IEnumerable<Vector3> points)
    {
        foreach(Vector3 point in points)
        {
            if (boundsToCheck.Contains(point))
                return true;
        }
        return false;
    }

    public static void SetLayerRecursively(this GameObject objectToSet, int layer)
    {
        objectToSet.layer = layer;
        foreach (Transform transform in objectToSet.transform)
        {
            if(transform.gameObject != objectToSet)
                transform.gameObject.SetLayerRecursively(layer);
        }
    }
    //Multiplies x by x, y by y, and z by z, returning the resultant value
    public static Vector3 Multiply(this Vector3 v1, Vector3 v2)
    {
        Vector3 retval = new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        return retval;
    }
    public static Vector3 Divide(this Vector3 v1, Vector3 v2)
    {
        Vector3 retval = new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        return retval;
    }

    public static Vector3 Average(this List<Vector3> vector3s)
    {
        Vector3 sum = Vector3.zero;

        foreach (Vector3 vector3 in vector3s)
        {
            sum += vector3;
        }

        int numVectors = vector3s.Count;

        sum = new Vector3(sum.x / numVectors, sum.y / numVectors, sum.z / numVectors);

        return sum;
    }

    public static float GreatestDimension(this Vector3 vector)
    {
        if (vector.x > vector.y && vector.x > vector.z)
            return vector.x;
        //At this point we know x is not the greatest, so it must be y or z
        return vector.y > vector.z ? vector.y : vector.z;
    }
    
}
