using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Helper : MonoBehaviour {

    public static string RaycastDown(Transform transform, BoxCollider2D col)
    {
        var startingMargin = 0.03f;
        var halfColliderSizeY = (col.size.y / 3);
        var looksize = 0.4f;
        var lookdirection = -1; // -1 Down, Left
        var posIni = new Vector2(transform.position.x, transform.position.y + (lookdirection * halfColliderSizeY));
        var endPos = new Vector2(transform.position.x, transform.position.y + (lookdirection * (halfColliderSizeY + looksize)));
        Debug.DrawLine(posIni, endPos);

        int mask = 1 << LayerMask.NameToLayer("WALL");
        var hit = Physics2D.Linecast(posIni,endPos,mask);

        if (hit.transform != null)
        {
            return hit.transform.tag;
        }

        return null;
    }

    public static string RaycastHorizontal(Transform transform, BoxCollider2D col, int mask, float looksize = 0.4f)
    {
        var startingMargin = 0.0f;
        var halfColliderSizeX = (col.size.x / 3) + startingMargin;
        //var halfColliderSizeY = (col.size.y / 2) + startingMargin;
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
}