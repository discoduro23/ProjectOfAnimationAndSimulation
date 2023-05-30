using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerControler : MonoBehaviour
{

    //The prefab to spawn
    [SerializeField] GameObject prefabMine = null;

    //The number of prefabs to spawn
    [SerializeField] int numberOfPrefabs = 4;

    //The dimensions of the cube where the prefabs will be spawned
    [SerializeField] Vector3 cubeDimensions = Vector3.one;

    // Make the dimensions a little smaller so the prefabs don't spawn on the edges
    [SerializeField] float offset = 10f;

    //When spawning an object it's needed to throw a raycast to the -y direction to know where to spawn it, getting the normals of that point and spawning acordingly
    [SerializeField] float raycastDistance = 50f;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SpawnPrefabs();
        }
        
    }

    private void SpawnPrefabs()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        //Spawn the prefabs
        for (int i = 0; i < numberOfPrefabs; i++)
        {

            //Get a random position inside the cube
            Vector3 randomPosition = new Vector3(
                Random.Range(0 + offset, cubeDimensions.x - offset),
                10,
                Random.Range(0 + offset, cubeDimensions.z - offset));

            Debug.Log("Spawning prefab " + i + " at position " + randomPosition);

            //Throw a raycast to the -y direction to know where to spawn the prefab
            RaycastHit hit;
            if (Physics.Raycast(randomPosition, Vector3.down, out hit, raycastDistance))
            {
                Debug.Log("Raycast hit at " + hit.point + " with normal " + hit.normal + "grounded? " + hit.collider.gameObject.tag);
                
                    //Instantiate the prefab
                    GameObject go = Instantiate(prefabMine, hit.point, Quaternion.identity);

                    //Set the rotation of the prefab to the normal of the point where it was spawned
                    go.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                
            }
            else
            {
                //Try again
                i--;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, cubeDimensions - new Vector3(offset, 0, offset));
    }
    

}
