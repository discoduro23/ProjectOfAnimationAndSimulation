using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerController : SingletonNotPersistent<GameManagerController>
{
    [SerializeField] private bool isEndCongratulations = false;

    public int life = 100;

    public int antenasRepared = 0;
    public int antenasToRepare = 4;

    public int materialPicked = 0;
    public int materialToPickForAntena = 4;

    public float percentage = 0;
    public bool isPercentage = false;

    public TextMeshProUGUI adviseZonnite = null;
    public TextMeshProUGUI infoTxt = null;
    private bool isAdviseShow = false;

    [SerializeField] private GameObject circularProgressBar = null;
    [SerializeField] private GameObject horizontalProgressBar = null;

    [SerializeField] private BigShipController bigShip = null;

    private void Start()
    {
        adviseZonnite.text = "";
    }

    public bool RequestRepair()
    {
        if (materialPicked >= materialToPickForAntena)
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

    public void MiningCompleted()
    {
        isPercentage = false;
        materialPicked++;
    }

    IEnumerator adviseTimer()
    {
        adviseZonnite.text = "You need " + (materialToPickForAntena - materialPicked) + " Zonnites to repare the antena";
        yield return new WaitForSeconds(2);
        adviseZonnite.text = "";
        isAdviseShow = false;

    }

    private void Update()
    {
        if (antenasRepared < antenasToRepare)
        {
            if (isPercentage)
            {
                circularProgressBar.SetActive(true);
                circularProgressBar.GetComponent<ProgressBarPro>().SetValue(percentage, 100);
            }
            else
            {
                circularProgressBar.SetActive(false);
                circularProgressBar.GetComponent<ProgressBarPro>().SetValue(0, 100);
            }

            horizontalProgressBar.GetComponent<ProgressBarPro>().SetValue(life, 100);

            infoTxt.text = "Antenas Repared: " + antenasRepared + "/" + antenasToRepare + "\n" +
                "Zonnites: " + materialPicked + "/" + materialToPickForAntena;
        }
        else
        {
            if (!isEndCongratulations)
            {
                // CAMERA CHANGE
               isEndCongratulations = true;
               bigShip.StartCoroutine(bigShip.MoveShip());
            }
        }
    }

}
