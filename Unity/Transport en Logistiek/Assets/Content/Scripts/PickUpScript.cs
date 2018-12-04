using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

    // on collision 
    // if press KEY -- +1 of tag to inventory
    // remove object

    int AmountOfBoxes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            Debug.Log("Pickup");
            int amount = AmountOfBoxes + 1;
            Destroy(other.gameObject);

            Debug.Log(AmountOfBoxes);
        }
    }
}
