using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject blockGameObject;

    public int worldSizeX;
    public int worldSizeZ;

    public float noiseHeight;

    public float gridOffset;
    private Vector3 startPosition;

    private Hashtable blockContainer = new Hashtable();

    void Start()
    {
        //This has been changed/added.
        float upTime = Time.realtimeSinceStartup;

        //This has been changed/added.
        for (int x = -worldSizeX - 2; x < worldSizeX + 2; x++)
        {
            //This has been changed/added
            for (int z = -worldSizeZ - 2; z < worldSizeZ + 2; z++)
            {
                //This has been changed/added.
                Vector3 pos = new Vector3(
                        x * 1 + startPosition.x,
                        generateNoise(x + xPlayerLocation, z + zPlayerLocation, 8f) * noiseHeight,
                        z * 1 + startPosition.z
                    );

                //This has been changed/added.
                string bName = $"Block-{((int)pos.x).ToString()},0,{((int)pos.z).ToString()}";

                GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity) as GameObject;
                block.transform.SetParent(this.transform);
                //This has been changed/added.
                block.name = bName;
                //This has been changed/added.
                BlockData bData = new BlockData(block, upTime);
                blockContainer.Add(block, bData);
            }
        }
    }

    private void Update()
    {
        if (Mathf.Abs(xPlayerMove) >= 1 || Mathf.Abs(zPlayerMove) >= 1)
        {
            //This has been changed/added.
            float upTime = Time.realtimeSinceStartup;
            //This has been changed/added.
            for (int x = -worldSizeX; x < worldSizeX * 8; x++)
            {
                //This has been changed/added.
                for (int z = -worldSizeZ; z < worldSizeZ * 8; z++)
                {
                    //This has been changed/added.
                    Vector3 pos = new Vector3(
                            x * 1 + (xPlayerLocation - 16),
                            generateNoise(x + xPlayerLocation, z + zPlayerLocation, 8f) * noiseHeight,
                            z * 1 + (zPlayerLocation - 16)
                        );

                    //This has been changed/added.
                    string bName = $"Block-{((int)pos.x).ToString()},0,{((int)pos.z).ToString()}";

                    if (!blockContainer.ContainsKey(bName))
                    {
                        GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity) as GameObject;
                        block.transform.SetParent(this.transform);
                        //This has been changed/added.
                        block.name = bName;

                        //This has been changed/added.
                        BlockData bData = new BlockData(block, upTime);
                        blockContainer.Add(block, bData);
                    }
                    //This has been added.
                    else
                        ((BlockData)blockContainer[bName]).upTime = upTime;
                }
            }
            //This has been changed/added.
            Hashtable newBlockContainer = new Hashtable();
            foreach (BlockData bData in blockContainer.Values)
            {
                if (Math.Abs(bData.upTime - upTime) > 0.0)
                    Destroy(bData.blockObj);
                else
                    newBlockContainer.Add(bData.blockObj.name, bData);
            }
            //This has been changed/added.
            blockContainer = newBlockContainer;
            startPosition = player.transform.position;
        }

    }

    public int xPlayerMove
    {
        get
        {
            return (int)(player.transform.position.x - startPosition.x);
        }
    }

    public int zPlayerMove
    {
        get
        {
            return (int)(player.transform.position.z - startPosition.z);
        }
    }
    public int xPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.x);
        }
    }

    public int zPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.z);
        }
    }

    private float generateNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}

//This has been changed/added.
class BlockData
{
    public GameObject blockObj;
    public float upTime;

    public BlockData(GameObject blockObj, float upTime)
    {
        this.blockObj = blockObj;
        this.upTime = upTime;
    }
}
