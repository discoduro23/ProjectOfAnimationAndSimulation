using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class PerlinTerrainGenerator : MonoBehaviour
{
    public int width = 256; //x-axis of the terrain
    public int height = 256; //z-axis

    public float scale = 20f;

    public int seed = 0; // Seed for the Perlin noise function

    private Terrain terrain;
    private TerrainCollider terrainCollider;
    private float[,] heights; // "Cached" array for storing height values

    private void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainCollider = GetComponent<TerrainCollider>();
        heights = new float[width, height]; // Initialize the heights array

        RequestTerrainChange(seed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RequestTerrainChange(seed);
        }
    }

    private void GenerateHeights()
    {
        if (seed == 0)
        {
            seed = (int)System.DateTime.Now.Ticks; // Set the seed for the Perlin noise function
        }

        Debug.Log("Seed: " + seed);
        Random.InitState(seed); // Set the seed for the Perlin noise function

        float offsetX = Random.Range(0f, 9999f);
        float offsetY = Random.Range(0f, 9999f);

        // Update the heights array with new height values
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + offsetX; // x-axis of the Perlin noise function
                float yCoord = (float)y / height * scale + offsetY; // y-axis of the Perlin noise function

                heights[x, y] = Mathf.PerlinNoise(xCoord, yCoord); // Get the height value from the Perlin noise function
            }
        }
    }

    private void UpdateTerrain()
    {
        terrain.Flush();

        terrainCollider.terrainData.SetHeights(0, 0, heights);
        terrain.terrainData.SetHeights(0, 0, heights);
    }


    public void RequestTerrainChange(int mSeed = 0)
    {
        if (mSeed != -1) this.seed = mSeed; // Set the seed for the Perlin noise function only if it's not -1 (to not override the seed)
        GenerateHeights();
        UpdateTerrain();
    }
}