using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    public GameObject plane;
    public GameObject player;

    private int radius = 5;

    private int planeOffset = 10;

    private Vector3 startPos = Vector3.zero;

    private int XplayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZplayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset;

    private Hashtable tilePlane = new Hashtable();

    
    void Update()
    {
        if(startPos == Vector3.zero)
        {
            for(int x = -radius; x < radius; x++)
            {
                for(int z = -radius; z< radius; z++)
                {
                    Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation), 0, (z * planeOffset + ZPlayerLocation));

                    if (!tilePlane.Contains(pos))
                    {
                        GameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                        tilePlane.Add(pos, _plane);
                    }
                }
            }
        }

        if(hasPlayerMoved())
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation), 0, (z * planeOffset + ZPlayerLocation));

                    if (!tilePlane.Contains(pos))
                    {
                        GameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                        tilePlane.Add(pos, _plane);
                    }
                }
            }
        }
    }

    bool hasPlayerMoved ()
    {
        if(Mathf.Abs(XplayerMove) >= planeOffset || Mathf.Abs(ZplayerMove) >= planeOffset)
        {
            return true;
        }
        return false;
    }
}
