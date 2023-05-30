using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerControler : MonoBehaviour
{
    //Terrain for the heights
    [SerializeField] Terrain terrain = null;

    //The prefab to spawn
    [SerializeField] GameObject prefab = null;

    //The number of prefabs to spawn
    [SerializeField] int numberOfPrefabs = 4;

    //The dimension of the cube to spawn (will be the same as terrain but a little bit smaller)
    [SerializeField] Vector3 dimensions = Vector3.zero;

    //The height of the cube to spawn
    [SerializeField] float height = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Get the terrain data
        TerrainData terrainData = terrain.terrainData;

        //Get the terrain size
        Vector3 terrainSize = terrainData.size;

        //Set the dimensions of the cube to spawn
        dimensions = new Vector3(terrainSize.x - 3, terrainSize.y - 3, terrainSize.z - 3);

        //Set the height of the cube to spawn
        height = terrainSize.y;

        //Spawn the prefabs
        SpawnPrefabs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Spawn the prefabs
    void SpawnPrefabs()
    {
        //Spawn the prefabs
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            //Get a random position
            Vector3 randomPosition = new Vector3(Random.Range(0, dimensions.x), Random.Range(0, dimensions.y), Random.Range(0, dimensions.z));

            //Get the height of the terrain at the random position
            float heightTerrain = terrain.SampleHeight(randomPosition);

            //Set the position of the prefab
            randomPosition.y = heightTerrain + height;

            //Instantiate the prefab
            Instantiate(prefab, randomPosition, Quaternion.identity);
        }
    }

    //Draw the cube
    private void OnDrawGizmos()
    {
        //Set the color
        Gizmos.color = Color.red;

        //Draw the cube
        Gizmos.DrawWireCube(transform.position, dimensions);
    }





}
