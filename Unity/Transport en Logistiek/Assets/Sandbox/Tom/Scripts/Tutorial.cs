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
    INTERACTIE,
    UPGRADEREPAIR,
    MACHINEONOFF
}

public class Tutorial : MonoBehaviour
{
    [Header("Tutorial Panel/Button")]
    public GameObject tutorialPanel;
    public GameObject smallerTutorialPanel;
    public Button tutorialButton;
    public Button smallTutorialPanelButton;

    public GameObject machinePanel;

    [Header("Normal Tutorial Texts")]
    public GameObject movementText;
    public GameObject cameraRotationText;
    public GameObject cameraScrollingText;
    public GameObject interactieText;

    [Header("Small Tutorial Texts")]
    public GameObject upgrade_repairText;
    public GameObject machineOnOffText;

    public GameObject[] _machines;

    private void Start()
    {
        tutorialButton.onClick.AddListener(delegate { ChangeState(1); });
        currentState = TutorialState.CLEAR;
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

    private void MachineOnOffText()
    {
        upgrade_repairText.SetActive(false);
        machineOnOffText.SetActive(true);
        currentState = TutorialState.UPGRADEREPAIR;
    }

    [System.NonSerialized]
    public static TutorialState currentState;
    public void ChangeState(int newState)
    {
        currentState = (TutorialState)newState;
    }

    private void Update()
    {
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
            if (machinePanel.activeSelf)
            {
                smallerTutorialPanel.SetActive(true);
                smallTutorialPanelButton.onClick.AddListener(MachineOnOffText);
            }
            else
            {
                smallerTutorialPanel.SetActive(false);
            }
        }
        if(currentState == TutorialState.MACHINEONOFF)
        {
            currentState = TutorialState.CLEAR;
        }
    }
}