using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerControler : MonoBehaviour
{

    //The prefab to spawn
    [SerializeField] GameObject prefab = null;

    //The number of prefabs to spawn
    [SerializeField] int numberOfPrefabs = 4;

    //The dimensions of the cube where the prefabs will be spawned
    [SerializeField] Vector3 cubeDimensions = Vector3.one;

    //When spawning an object it's needed to throw a raycast to the -y direction to know where to spawn it, getting the normals of that point and spawning acordingly
    [SerializeField] float raycastDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn the prefabs
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            //Get a random position inside the cube
            Vector3 randomPosition = new Vector3(Random.Range(-cubeDimensions.x / 2, cubeDimensions.x / 2), Random.Range(-cubeDimensions.y / 2, cubeDimensions.y / 2), Random.Range(-cubeDimensions.z / 2, cubeDimensions.z / 2));

            //Throw a raycast to the -y direction to know where to spawn the prefab
            RaycastHit hit;
            if (Physics.Raycast(randomPosition, Vector3.down, out hit, raycastDistance))
            {
                //Instantiate the prefab
                GameObject go = Instantiate(prefab, hit.point, Quaternion.identity);

                //Set the rotation of the prefab to the normal of the point where it was spawned
                go.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, cubeDimensions);
    }
    

}
