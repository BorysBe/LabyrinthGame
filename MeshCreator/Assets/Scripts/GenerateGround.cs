using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGround : MonoBehaviour
{
    [SerializeField] bool generateNoiseTrigger = false;

    public GameObject _blockGameObject;
    public GameObject _player;
    public GameObject _objectToSpawn;

    private int worldSizeX = 20;

    private int worldSizeZ = 20;

    private int noiseHeight = 3;

    private float gridOffset = 1.1f;

    private Vector3 startPosition;

    private List<Vector3> _blockPositions = new List<Vector3>(); 

    private Hashtable blockContainer = new Hashtable();

    void Start()
    {
        for(int x = -worldSizeX; x < worldSizeX; x++)
        {
            for(int z = -worldSizeZ; z < worldSizeZ; z++)
            {
                Vector3 _pos = new Vector3(x * 1 + startPosition.x, yPositionGenerator(x, z, noiseHeight), z * 1 + startPosition.z);

                GameObject _block = Instantiate(_blockGameObject, _pos, Quaternion.identity) as GameObject;

                blockContainer.Add(_pos, _block);
                _block.transform.SetParent(this.transform);
            }
        }
        //SpawnObject();
    }
    float yPositionGenerator(int x, int z, int noiseHeight)
    {
        float yPositionGenerator;
        if (generateNoiseTrigger == false)
        {
            return 0;
        }
        else
        {
            return GenerateNoise(x, z, 8f) * noiseHeight;
        }
    }

    void Update()
    {
        if (Mathf.Abs(xPlayerMove) >= 1 || Mathf.Abs(zPlayerMove) >= 1)
        {
            for (int x = -worldSizeX; x < worldSizeX; x++)
            {
                for (int z = -worldSizeZ; z < worldSizeZ; z++)
                {
                    Vector3 _pos = new Vector3(x * 1 + xPlayerLocation, 
                        yPositionGenerator(x + xPlayerLocation, z + zPlayerLocation, noiseHeight),
                        z * 1 + zPlayerLocation);

                    GameObject _block = Instantiate(_blockGameObject, _pos, Quaternion.identity) as GameObject;

                    blockContainer.Add(_pos, _block);
                    _blockPositions.Add(_block.transform.position);
                    _block.transform.SetParent(this.transform);
                }
            }
        }
    }

    public int xPlayerMove
    {
        get
        {
            return (int)(_player.transform.position.x - startPosition.x);
        }
    }

    public int zPlayerMove
    {
        get
        {
            return (int)(_player.transform.position.z - startPosition.z);
        }
    }

    private int xPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(_player.transform.position.x);
        }
    }

    private int zPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(_player.transform.position.z);
        }
    }

    private void SpawnObject()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject _toPlaceObject = Instantiate(_objectToSpawn, ObjectSpawnLocation(), Quaternion.identity);
        }
    }

    private Vector3 ObjectSpawnLocation ()
    {
        int _rndIndex = Random.Range(0, _blockPositions.Count);

        Vector3 newPos = new Vector3(
                _blockPositions[_rndIndex].x,
                _blockPositions[_rndIndex].y + 0.5f,
                _blockPositions[_rndIndex].z
            );
        _blockPositions.RemoveAt(_rndIndex);
        return newPos;
    }

    private float GenerateNoise (int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}
