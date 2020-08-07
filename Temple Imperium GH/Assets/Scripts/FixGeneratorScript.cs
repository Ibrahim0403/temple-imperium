using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixGeneratorScript : MonoBehaviour
{
    public GameObject cogInteractHUD;
    public GameObject gameWonHUD;
    public GameObject roundManager;

    public GameObject poisonGenerator;
    public GameObject speedGenerator;
    public GameObject snareGenerator;
    public GameObject floatGenerator;

    public static bool hasPoisonCOG, pickPoisonCOG;
    public static bool hasSpeedCOG, pickSpeedCOG;
    public static bool hasSnareCOG, pickSnareCOG;
    public static bool hasFloatCOG, pickFloatCOG;

    public static int poisonKills, speedKills, snareKills, floatKills;

    private RoundManagerScript roundManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        roundManagerScript = roundManager.GetComponent<RoundManagerScript>();

        hasPoisonCOG = false;
        hasSpeedCOG = false;
        hasSnareCOG = false;
        hasFloatCOG = false;

        pickPoisonCOG = false;
        pickSpeedCOG = false;
        pickSnareCOG = false;
        pickFloatCOG = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && (pickPoisonCOG)){ //pick up generator parts
            Destroy(GameObject.FindWithTag("PoisonCog"));
            cogInteractHUD.SetActive(false);
            hasPoisonCOG = true;
            poisonGenerator.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) && (pickSpeedCOG))
        {
            Destroy(GameObject.FindWithTag("SpeedCog"));
            cogInteractHUD.SetActive(false);
            hasSpeedCOG = true;
            speedGenerator.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) && (pickSnareCOG))
        {
            Destroy(GameObject.FindWithTag("SnareCog"));
            cogInteractHUD.SetActive(false);
            hasSnareCOG = true;
            snareGenerator.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) && (pickFloatCOG))
        {
            Destroy(GameObject.FindWithTag("FloatCog"));
            cogInteractHUD.SetActive(false);
            hasFloatCOG = true;
            floatGenerator.SetActive(true);
        }

        if (hasPoisonCOG && hasSpeedCOG && hasSnareCOG && hasFloatCOG && roundManagerScript.activeRound >= 8) //must have all generator parts and completed round 7 or above to win
        {
            Time.timeScale = 0f;
            gameWonHUD.SetActive(true);
            StartCoroutine(EndCutscene()); //begin end cutscene
        }
    }

    IEnumerator EndCutscene() {
        yield return new WaitForSecondsRealtime(5f);
        roundManagerScript.hasCompleted = true;
    }
}
