using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : SingletonNotPersistent<GameManagerController>
{
    [SerializeField] private bool isEndCongratulations = false;
    [SerializeField] private bool isLoosing = false;

    public int life = 100;

    public int antenasRepared = 0;
    public int antenasToRepare = 4;

    public int materialPicked = 0;
    public int materialToPickForAntena = 4;

    public float percentage = 0;
    public bool isPercentage = false;

    public TextMeshProUGUI adviseZonnite = null;
    public TextMeshProUGUI infoTxt = null;
    public TextMeshProUGUI bigtitle = null;
    private bool isAdviseShow = false;

    [SerializeField] private GameObject circularProgressBar = null;
    [SerializeField] private GameObject horizontalProgressBar = null;

    [SerializeField] private BigShipController bigShip = null;

    public CameraTransitionController cameras;

    private void Start()
    {
        adviseZonnite.text = "";
        bigtitle.text = "";
        Cursor.lockState = CursorLockMode.Locked;
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

            infoTxt.text = "Antennas Repaired: " + antenasRepared + "/" + antenasToRepare + "\n" +
                "Zonnites: " + materialPicked + "/" + materialToPickForAntena;
        }
        else
        {
            if (!isEndCongratulations)
            {
                cameras.endgame = true;
                isEndCongratulations = true;
                StartCoroutine(EndGame());
            }
        }

        if( life <= 0 && !isLoosing)
        {
            cameras.endgame = true;
            isLoosing = true;
            StartCoroutine(Loser());
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);
        bigShip.MoveShip();
        bigtitle.text = "<b>Congratulations!</b> You repaired all the antennas and the ship is ready to take you home!";
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Loser()
    {
        yield return new WaitForSeconds(2);
        bigtitle.text = "<b>Game Over</b> The drones killed you!";
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }

}
