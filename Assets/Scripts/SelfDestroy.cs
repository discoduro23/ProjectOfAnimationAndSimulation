using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 0.2f);

    }

    void Destroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
}
