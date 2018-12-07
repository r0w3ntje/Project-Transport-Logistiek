using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIInfoInput : MonoBehaviour {
  
    public string name_holder;
    public string age_holder;
    public string wage_holder;
    public string job_holder;
    public string Goingto_Holder;

    public List<string> names;
    public List<string> ages;
    public List<string> wages;
    public List<string> jobs;   

    public AIInfo aIInfo;


    void Awake(){
        name_holder = names[Random.Range(0, names.Count)];
        age_holder = ages[Random.Range(0, ages.Count)];
        wage_holder = wages[Random.Range(0, wages.Count)];
        job_holder = jobs[Random.Range(0, jobs.Count)];
    }
}
