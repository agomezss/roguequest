using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class Helper : MonoBehaviour {

    public static string RaycastDown(Transform transform, BoxCollider2D col)
    {
        var halfColliderSizeY = (col.size.y / 3);
        var looksize = 0.2f;
        var lookdirection = -1; // -1 Down, Left
        var posIni = new Vector2(transform.position.x, transform.position.y + (lookdirection * halfColliderSizeY));
        var endPos = new Vector2(transform.position.x, transform.position.y + (lookdirection * (halfColliderSizeY + looksize)));
        Debug.DrawLine(posIni, endPos);

        int mask = 1 << LayerMask.NameToLayer("WALL");
        mask = mask | 1 << LayerMask.NameToLayer("LADDER");
        mask = mask | 1 << LayerMask.NameToLayer("Water");

        var hit = Physics2D.Linecast(posIni,endPos,mask);

        if (hit.transform != null)
        {
            return hit.transform.tag;
        }

        return null;
    }

    public static string RaycastHorizontal(Transform transform, BoxCollider2D col, int mask, float looksize = 0.4f)
    {
        var halfColliderSizeX = (col.size.x / 3);
        var lookdirection = Mathf.Sign(transform.localScale.x);
        var posIni = new Vector2(transform.position.x + (lookdirection * halfColliderSizeX), transform.position.y);
        var endPos = new Vector2(transform.position.x + (lookdirection * (halfColliderSizeX + looksize)), transform.position.y);
        Debug.DrawLine(posIni, endPos);

        var hit = Physics2D.Linecast(posIni,endPos,mask);

        if (hit.transform != null)
        {
            return hit.transform.tag;
        }

        return null;
    }

    public static T Clone<T>(T source)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new ArgumentException("The type must be serializable.", "source");
        }

        // Don't serialize a null object, simply return the default for that object
        if (System.Object.ReferenceEquals(source, null))
        {
            return default(T);
        }

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream();
        using (stream)
        {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }
}