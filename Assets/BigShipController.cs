using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShipController : MonoBehaviour
{
    [SerializeField] private Transform endPoint;

    //This script will move the big ship from the start point(its actual point) to the end point at a decreasing speed
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator MoveShip()
    {
        while (Vector3.Distance(transform.position, endPoint.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, endPoint.position, Time.deltaTime * 0.5f);
            yield return null;
        }
    }
}
