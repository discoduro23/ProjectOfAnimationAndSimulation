using QuantumTek.QuantumTravel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMineBehaveour : MonoBehaviour
{

    // Mina de mineral
    public GameObject mineralMine;

    public float timeToMine = 15f;
    private float timeMining = 0f;
    private float percentage = 0f;

    [SerializeField] private float initialSize = 0.2f;

        
    private void Start()
    {
    }

    public void Mine()
    {
        if (timeMining > timeToMine)
        { 
            GameManagerController.Instance.MiningCompleted();
            Destroy(gameObject);
        }
        else
        {
            timeMining += Time.deltaTime;

            // Reducir el tamaño de la mina de mineral
            float scalePercentage = initialSize - (timeMining / timeToMine*0.2f) * initialSize;
            mineralMine.transform.localScale = new Vector3(scalePercentage, scalePercentage, scalePercentage);

            GameManagerController.Instance.isPercentage = true;
            percentage = timeMining / timeToMine * 100;
            GameManagerController.Instance.percentage = percentage;
        }

    }
}
