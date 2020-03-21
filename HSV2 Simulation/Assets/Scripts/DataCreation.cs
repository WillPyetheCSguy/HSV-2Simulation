using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCreation : MonoBehaviour {

    public float femaleToMaleInfectionRate = 2.5f;
    public float maleToFemaleInfectionRate = 2.82f;
    public int newFemaleStudents = 5500;
    public int newMaleStudents = 4500;
    //public float propFemaleVacinatedPre = .02f;
    //public float efficacyOfVaccine = .07f;
    // .010556 seropostive females .0054438 seropositive males
    public float propSeroNegativeFemalePre = .98944f;
    public float propSeroNegativeMalePre = .99455f;
    //public float propFemalsVacinated = .03f;
    public float propFemalesLeaving = 0.2f;
    public float propMalesLeaving = 0.2f;
    public int startingMalePop = 15000;
    public int startingFemalePop = 20000;
    public float propStartingMaleInfected = .0f;
    public float propStartingFemaleInfected = .0f;
    public int numStartingMaleInfected = 108;
    public int numStartingFemaleInfected = 254;
    public float avgInfectedMaleToFemale = .4f;
    public float avgInfectedFemaleToMale = .3f;

    private int infectedFemales;
    private int infectedMales;
    private int susceptibleFemales;
    private int susceptibleMales;
    //private int vacinatedFemales

    private float lastUpdate;
    private int step;

    // Use this for initialization
    void Start () {
        infectedFemales = (int)(propStartingFemaleInfected * startingFemalePop);
        infectedMales = (int)(propStartingMaleInfected * startingMalePop);
        //infectedMales = numStartingMaleInfected;
        //infectedFemales = numStartingFemaleInfected;
        susceptibleFemales = startingFemalePop - infectedFemales;
        susceptibleMales = startingMalePop - infectedMales;
        lastUpdate = Time.time;
        step = 0;
        Debug.Log(step);
        Debug.Log("UF");
        Debug.Log(susceptibleFemales);
        Debug.Log("UM");
        Debug.Log(susceptibleMales);
        Debug.Log("IF");
        Debug.Log(infectedFemales);
        Debug.Log("IM");
        Debug.Log(infectedMales);
        //


    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time > lastUpdate + 2)
        {
            CalcTimestep();
            step++;
            Debug.Log(step);
            Debug.Log("UF");
            Debug.Log(susceptibleFemales);
            Debug.Log("UM");
            Debug.Log(susceptibleMales);
            Debug.Log("IF");
            Debug.Log(infectedFemales);
            Debug.Log("IM");
            Debug.Log(infectedMales);
            Debug.Log("%IF");
            Debug.Log((float)infectedFemales / ((float)susceptibleFemales + (float)infectedFemales));
            Debug.Log("%IM");
            Debug.Log((float)infectedMales / ((float)susceptibleMales + (float)infectedMales));
            lastUpdate = Time.time;
        }
	}

    void CalcTimestep()
    {
        //int tempSusceptibleFemales = (int)(propSeroNegativeFemalePre * newFemaleStudents + susceptibleFemales * (1 - ((maleToFemaleInfectionRate * infectedMales) / (susceptibleFemales + infectedFemales))) - propFemalesLeaving * susceptibleFemales);
        //int tempSusceptibleMales = (int)(propSeroNegativeMalePre * newMaleStudents + susceptibleMales * (1 - ((femaleToMaleInfectionRate * infectedFemales) / (susceptibleMales + infectedMales))) - propMalesLeaving * susceptibleMales);
        //int tempInfectedMales = (int)(newMaleStudents * (1 - propSeroNegativeMalePre) + infectedMales * (1 - propMalesLeaving) + femaleToMaleInfectionRate * infectedFemales * (susceptibleMales / (infectedMales + susceptibleMales)));
        //int tempInfectedFemales = (int)(newFemaleStudents * (1 - propSeroNegativeFemalePre) + infectedFemales * (1 - propFemalesLeaving) + maleToFemaleInfectionRate * infectedMales * (susceptibleFemales / (infectedFemales + susceptibleFemales)));
        int tempSusceptibleFemales = (int)(propSeroNegativeFemalePre * newFemaleStudents + susceptibleFemales  - ((avgInfectedMaleToFemale * infectedMales)) - propFemalesLeaving * susceptibleFemales);
        int tempSusceptibleMales = (int)(propSeroNegativeMalePre * newMaleStudents + susceptibleMales -  ((avgInfectedFemaleToMale * infectedFemales)) - propMalesLeaving * susceptibleMales);
        int tempInfectedMales = (int)(newMaleStudents * (1 - propSeroNegativeMalePre)  + infectedMales * (1 - propMalesLeaving) + avgInfectedFemaleToMale * infectedFemales);
        int tempInfectedFemales = (int)(newFemaleStudents * (1 - propSeroNegativeFemalePre) + infectedFemales * (1 - propFemalesLeaving) + avgInfectedMaleToFemale * infectedMales);
        infectedFemales = tempInfectedFemales;
        susceptibleMales = tempSusceptibleMales;
        susceptibleFemales = tempSusceptibleFemales;

    }
}
