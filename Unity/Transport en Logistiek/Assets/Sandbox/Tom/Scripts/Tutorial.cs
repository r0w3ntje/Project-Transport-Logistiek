using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TransportLogistiek;

[System.Serializable]
public enum TutorialState
{
    CLEAR,
    MOVEMENT,
    CAMERAROTATION,
    CAMERASCROLLING,
    INTERACTIE
}

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public Button tutorialButton;

    public GameObject movementText;
    public GameObject cameraRotationText;
    public GameObject cameraScrollingText;
    public GameObject interactieText;

    public GameObject[] _machines;

    private void Start()
    {
        tutorialButton.onClick.AddListener(delegate { ChangeState(1); });
        currentState = TutorialState.CLEAR;
        for (int i = 0; i < _machines.Length; i++)
        {
            Debug.Log("Hello");
            _machines[i].GetComponent<Machine>().enabled = false;
            _machines[i].GetComponent<MachineInteraction>().enabled = false;
            _machines[i].GetComponent<MachineUpgrade>().enabled = false;
            _machines[i].GetComponent<MachineProduction>().enabled = false;
        }
    }

    private bool forwardMovement = false;
    private bool backwardMovement = false;
    private bool leftMovement = false;
    private bool rightMovement = false;
    private void CheckMovement()
    {
        if (!forwardMovement && Input.GetKey(KeyCode.W))
            forwardMovement = true;
        if (!backwardMovement && Input.GetKey(KeyCode.S))
            backwardMovement = true;
        if (!leftMovement && Input.GetKey(KeyCode.A))
            leftMovement = true;
        if (!rightMovement && Input.GetKey(KeyCode.D))
            rightMovement = true;

        if (forwardMovement && backwardMovement || leftMovement && rightMovement)
        {
            tutorialPanel.SetActive(true);
            movementText.SetActive(false);
            cameraRotationText.SetActive(true);
            currentState = TutorialState.CLEAR;
            tutorialButton.onClick.RemoveAllListeners();
            tutorialButton.onClick.AddListener(delegate { ChangeState(2); });
        }
    }

    private void CheckCameraRotation()
    {
        if (Input.GetKey(KeyCode.Mouse2) || Input.GetKey(KeyCode.Mouse1))
        {
            tutorialPanel.SetActive(true);
            cameraRotationText.SetActive(false);
            cameraScrollingText.SetActive(true);
            currentState = TutorialState.CLEAR;
            tutorialButton.onClick.RemoveAllListeners();
            tutorialButton.onClick.AddListener(delegate { ChangeState(3); });
        }
    }

    private void CheckCameraScrolling()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            tutorialPanel.SetActive(true);
            cameraScrollingText.SetActive(false);
            interactieText.SetActive(true);
            currentState = TutorialState.CLEAR;
            tutorialButton.onClick.RemoveAllListeners();
            tutorialButton.onClick.AddListener(delegate { ChangeState(4); });
        }
    }

    [System.NonSerialized]
    public static TutorialState currentState;
    public void ChangeState(int newState)
    {
        currentState = (TutorialState)newState;
    }

    private void Update()
    {
        //Debug.Log(currentState);
        if (currentState == TutorialState.MOVEMENT)
        {
            CheckMovement();
        }
        if (currentState == TutorialState.CAMERAROTATION)
        {
            CheckCameraRotation();
        }
        if (currentState == TutorialState.CAMERASCROLLING)
        {
            CheckCameraScrolling();
        }
        if (currentState == TutorialState.INTERACTIE)
        {
            for (int i = 0; i < _machines.Length; i++)
            {
                _machines[i].GetComponent<Machine>().enabled = true;
                _machines[i].GetComponent<MachineInteraction>().enabled = true;
                _machines[i].GetComponent<MachineUpgrade>().enabled = true;
                _machines[i].GetComponent<MachineProduction>().enabled = true;
            }
            currentState = TutorialState.CLEAR;
        }
    }
}