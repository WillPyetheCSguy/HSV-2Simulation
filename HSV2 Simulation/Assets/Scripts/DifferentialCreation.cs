using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifferentialCreation : MonoBehaviour {


    //Population Variables
    private float primaryInfected;
    private float maturedInfected;
    private float susceptible;
    private float vacinated;
    private float N = 30000f;

    //Starting values
    public int startPrimaryInfected = 27;
    public int startMaturedInfected = 900;
    public int startSusceptible = 29073;
    public int startVacinated = 0;

    //Time variables
    public int tN = 1825;
    public int tI = 18;

    //Infection variables
    public float pI = .9f;
    public float pM = 0.122f;

    //Vaccination variables
    public float u = .0f;

    //Contact variables
    public float c = 2f/365f;

    //Time variables
    private int stepCount;
    private float lastUpdate;
    public float timeBetweenSteps = 1.5f;

    //Integer Values For Displaying
    private int intPI;
    private int intMI;
    private int intS;
    private int intV;
    private int intN;

    //Display Objects
    public Text stats;
    public Text init;
    public Text param;

	// Use this for initialization
	void Start () {
        primaryInfected = startPrimaryInfected;
        maturedInfected = startMaturedInfected;
        susceptible = startSusceptible;
        vacinated = startVacinated;
        stepCount = 0;
        lastUpdate = Time.time;
        init.text = "Initial Values:\nSusceptible: " + startSusceptible + "\nPrimary Infected: " + startPrimaryInfected + "\nMatured Infected: " + startMaturedInfected + "\nVacinated: " + startVacinated;
        param.text = "Unique Partners per Day: " + c + "\nPercent of Susceptible vacinated per Day: " + u * 100 + "%\nPrimary Infection Chance: " + pI + "\nMatured Infection Chance: "+ pM + "\nTime in Primary: " + tI + " days\nTime in Population: " + tN + " days";
        UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > lastUpdate + timeBetweenSteps)
        {
            CalcTimeStep();
            stepCount++;
           // Debug.Log(stepCount);
           // Debug.Log("I");
            //Debug.Log(primaryInfected);
            //Debug.Log("M");
            //Debug.Log(maturedInfected);
            //Debug.Log("S");
            //Debug.Log(susceptible);
            //Debug.Log("V");
            //Debug.Log(vacinated);
            lastUpdate = Time.time;
            intPI = (int)primaryInfected;
            intMI = (int)maturedInfected;
            intS = (int)susceptible;
            intV = (int)vacinated;
            intN = intV + intS + intMI + intPI;
            UpdateText();
        }
    }

    void UpdateText()
    {
        stats.text = "Day: " + stepCount + "\nSusceptible: " + intS + "\nPrimary Infected: " + intPI + "\nMatured Infected: " + intMI + "\nVacinated: " + intV;
        
    }


    void CalcTimeStep()
    {

        float tempSus = susceptible + (.992f * N / tN) - (susceptible * ((primaryInfected * pI + maturedInfected * pM) * (c / N) + (1f / tN) + u));
        //float tempSus = susceptible + (N / tN) - (susceptible * ((primaryInfected * pI + maturedInfected * pM) * c / N + (1f / tN) + u));
        float tempInf = primaryInfected + (susceptible * (primaryInfected * pI + maturedInfected * pM) * c / N) - primaryInfected * (1f / tI + 1f / tN);
        float tempMat = maturedInfected + (.008f * N / tN) + primaryInfected * (1f / tI) - maturedInfected * (1f / tN);
        //float tempMat = maturedInfected + primaryInfected * (1f / tI) - maturedInfected * (1f / tN);
        float tempVac = vacinated + (susceptible * u) - (vacinated / tN);
        susceptible = tempSus;
        primaryInfected = tempInf;
        maturedInfected = tempMat;
        vacinated = tempVac;
    }
}
