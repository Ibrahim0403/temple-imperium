using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class StoneChargeScript : MonoBehaviour
{

    public float poisonCharge;
    public float speedCharge;
    public float snareCharge;
    public float floatCharge;
    public float highestCharge;
    public float maxCharge = 30f;

    public static bool enemyChargePoison = false;
    public static bool enemyChargeSpeed = false;
    public static bool enemyChargeSnare = false;
    public static bool enemyChargeFloat = false;

    public string ChargeText;

    private TextMeshProUGUI textMesH;

    // Start is called before the first frame update
    void Start()
    {
        maxCharge = 30f;
        //poisonCharge = 50f;
        //speedCharge = 50f;
        //snareCharge = 50f;
        textMesH = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mathf.Max(Mathf.Round(poisonCharge), Mathf.Round(speedCharge), Mathf.Round(snareCharge)));
        highestCharge = Mathf.Max(Mathf.Round(poisonCharge), Mathf.Round(speedCharge), Mathf.Round(snareCharge), Mathf.Round(floatCharge));

        //all code below makes all the starstone charges work
        if (poisonCharge >= maxCharge && !enemyChargeSpeed && !enemyChargeSnare && !enemyChargeFloat) //gets highest charged stone for enemy to use
        {
            enemyChargePoison = true;
        }
        else if (speedCharge >= maxCharge && !enemyChargePoison && !enemyChargeSnare && !enemyChargeFloat)
        {
            enemyChargeSpeed = true;
            EnemyController.speedChanged = true;

        }
        else if (snareCharge >= maxCharge && !enemyChargePoison && !enemyChargeSpeed && !enemyChargeFloat)
        {
            enemyChargeSnare = true;
        }
        else if (floatCharge >= maxCharge && !enemyChargePoison && !enemyChargeSpeed && !enemyChargeSnare)
        {
            enemyChargeFloat = true;
        }

        if (!PlayerMovement.increasePoisonCharge && !enemyChargePoison)
        {
            if (poisonCharge <= maxCharge) //increase charge
            {
                poisonCharge += Time.deltaTime;
            }
        }
        else
        {
            if (poisonCharge >= 0f) 
            {
                poisonCharge -= Time.deltaTime;
            }
            else
            {
                enemyChargePoison = false;
            }

            if (PlayerMovement.increasePoisonCharge && poisonCharge <= 0f)
            {
                PlayerMovement.selectedNothing = true;
            }
        }

        if (!PlayerMovement.increaseSpeedCharge && !enemyChargeSpeed)
        {
            if (speedCharge <= maxCharge)
            {
                speedCharge += Time.deltaTime;
            }
        }
        else
        {
            if (speedCharge > 0f)
            {
                speedCharge -= Time.deltaTime;
            }
            else
            {
                enemyChargeSpeed = false;
                EnemyController.hasOtherStarStone = true;
            }

            if (PlayerMovement.increaseSpeedCharge && speedCharge <= 0f)
            {
                PlayerMovement.selectedNothing = true;
            }
        }

        if (!PlayerMovement.increaseSnareCharge && !enemyChargeSnare)
        {
            if (snareCharge <= maxCharge)
            {
                snareCharge += Time.deltaTime;
            }
        }
        else
        {
            if (snareCharge >= 0f)
            {
                snareCharge -= Time.deltaTime;
            }
            else
            {
                enemyChargeSnare = false;
            }

            if (PlayerMovement.increaseSnareCharge && snareCharge <= 0f)
            {
                PlayerMovement.selectedNothing = true;
            }
        }

        if (!PlayerMovement.increaseFloatCharge && !enemyChargeFloat)
        {
            if (floatCharge <= maxCharge)
            {
                floatCharge += Time.deltaTime;
            }
        }
        else
        {
            if (floatCharge >= 0f)
            {
                floatCharge -= Time.deltaTime;
            }
            else
            {
                enemyChargeFloat = false;
            }

            if (PlayerMovement.increaseFloatCharge && floatCharge <= 0f)
            {
                PlayerMovement.selectedNothing = true;
            }
        }

        ChargeText = (Mathf.Round(poisonCharge).ToString("00") + "    " + Mathf.Round(speedCharge).ToString("00") + "    " + Mathf.Round(snareCharge).ToString("00") + "    " + Mathf.Round(floatCharge).ToString("00"));
        textMesH.text = ChargeText;
    }
}
