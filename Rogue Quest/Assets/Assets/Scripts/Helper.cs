using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    
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

        var hit = Physics2D.Linecast(posIni, endPos, mask);

        if (hit.transform != null)
        {
            return hit.transform.tag;
        }

        return null;
    }

    public static string RaycastDiagonal(Transform transform, BoxCollider2D col, int lookdirection)
    {
        var halfColliderSizeY = (col.size.y / 3);
        var halfColliderSizeX = (col.size.x / 3);
        var lookOffset = 0.4f;
        var looksize = 0.4f;
        var posIni = new Vector2(transform.position.x + (lookdirection * (halfColliderSizeX + lookOffset)), transform.position.y + (-1 * halfColliderSizeY));
        var endPos = new Vector2(transform.position.x + (lookdirection * (halfColliderSizeX + looksize)), transform.position.y + (-1 * (halfColliderSizeY + looksize)));
        Debug.DrawLine(posIni, endPos);

        int mask = 1 << LayerMask.NameToLayer("WALL");
        mask = mask | 1 << LayerMask.NameToLayer("LADDER");
        mask = mask | 1 << LayerMask.NameToLayer("Water");

        var hit = Physics2D.Linecast(posIni, endPos, mask);

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

        var hit = Physics2D.Linecast(posIni, endPos, mask);

        if (hit.transform != null)
        {
            return hit.transform.tag;
        }

        return null;
    }

    // Require serializable objects
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

    public static T DeepCopy<T>(T obj)
    {
        if (obj == null)
            throw new ArgumentNullException("Object cannot be null");
        return (T)Process(obj);
    }

    static object Process(object obj)
    {
        if (obj == null)
            return null;
        Type type = obj.GetType();
        if (type.IsValueType || type == typeof(string))
        {
            return obj;
        }
        else if (type.IsArray)
        {
            Type elementType = Type.GetType(
                 type.FullName.Replace("[]", string.Empty));
            var array = obj as Array;
            Array copied = Array.CreateInstance(elementType, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                copied.SetValue(Process(array.GetValue(i)), i);
            }
            return Convert.ChangeType(copied, obj.GetType());
        }
        else if (type.IsClass)
        {
            object toret = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                        BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object fieldValue = field.GetValue(obj);
                if (fieldValue == null)
                    continue;
                field.SetValue(toret, Process(fieldValue));
            }
            return toret;
        }
        else
            throw new ArgumentException("Unknown type");
    }
}