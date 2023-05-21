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
    private float[,] heights; // "Cached" array for storing height values

    private void Start()
    {
        terrain = GetComponent<Terrain>();
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
        TerrainData terrainData = terrain.terrainData;

        // Set the terrain data with the updated heights array
        terrainData.SetHeights(0, 0, heights);
        
        // Update the terrain with the new data
        terrain.terrainData = terrainData;
    }

    public void RequestTerrainChange(int mSeed = 0){
        if (mSeed != -1) this.seed = mSeed; // Set the seed for the Perlin noise function only if it's not -1 (to not override the seed)
        GenerateHeights();
        UpdateTerrain();
        StoreLowest10Points();
    }

    private void StoreLowest10Points()
    {
        int count = 0;
        int maxCount = 10;
        float minValue = 1;
        List<Vector2> points = new List<Vector2>();
        List<Vector3> points3D = new List<Vector3>();

        // Set the minimum distance between points
        float minDistance = 50f;

        while (count < maxCount)
        {
            minValue = 1;
            Vector2 minPoint = new Vector2(0, 0);

            // Find the lowest point that is at least minDistance away from existing points
            for (int x = 0; x < width; x += 10)
            {
                for (int y = 0; y < height; y += 10)
                {
                    bool isValidPoint = true;

                    foreach (Vector2 point in points)
                    {
                        float distance = Vector2.Distance(point, new Vector2(x, y));
                        if (distance < minDistance)
                        {
                            isValidPoint = false;
                            break;
                        }
                    }

                    if (isValidPoint && heights[x, y] < minValue)
                    {
                        minValue = heights[x, y];
                        minPoint = new Vector2(x, y);
                    }
                }
            }

            points.Add(minPoint);
            points3D.Add(new Vector3(minPoint.x, minValue, minPoint.y));
            heights[(int)minPoint.x, (int)minPoint.y] = 1;
            count++;
        }

        // Generate cubes
        foreach (Vector3 point in points3D)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = point;
            cube.transform.localScale = new Vector3(10, 10, 10);
        }
    }

}