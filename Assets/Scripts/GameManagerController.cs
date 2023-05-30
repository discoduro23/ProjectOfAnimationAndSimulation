using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerController : Singleton<GameManagerController>
{
    public int life = 100;
    
    public int antenasRepared = 0;
    public int antenasToRepare = 4;

    public int materialPicked = 0;
    public int materialToPickForAntena = 4;

    public float percentage = 0;
    public bool isPercentage = false;

    public TextMeshProUGUI adviseZonnite = null;
    private bool isAdviseShow = false;

    private void Start()
    {
        adviseZonnite.text = "";
    }
    
    public bool RequestRepair()
    {
        if(materialPicked >= materialToPickForAntena)
        {
            return true;
        }
        else
        {
            if (!isAdviseShow)
            {
                Debug.Log("Opps, no material");
                isAdviseShow = true;
                StartCoroutine(adviseTimer());
            }

            return false;
        }
    }

    public void RepairCompleted()
    {
        isPercentage = false;
        antenasRepared++;
        materialPicked -= materialToPickForAntena;
    }

    IEnumerator adviseTimer()
    {
        adviseZonnite.text = "You need " + (materialToPickForAntena - materialPicked) + " Zonnites to repare the antena";
        yield return new WaitForSeconds(2);
        adviseZonnite.text = "";
        isAdviseShow = false;

    }



}
