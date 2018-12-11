using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInfo : MonoBehaviour
{
    [Header("Text")]
    public TextMesh name_text;
    public TextMesh age_text;
    public TextMesh wage_text;
    public TextMesh job_text;
    public TextMesh Goingto_text;

    [Header("GameObjects")]
    public GameObject infoCard;
    public GameObject AIPlayer;

    [Header("Audio")]
    public AudioSource ASource;
    public AudioClip Click;
    public AudioClip PopUp;
    public AudioClip PopDown;
    public AudioClip Pin;

    [Header("Bools")]
    public bool IsOpen;
    public bool unPined;
    public bool ShowName;

    int repin;

    private Camera MainCamera;
    private RaycastHit hit;

    void Start()
    {
        MainCamera = GetComponent<Camera>();
        infoCard.active = false;
        ShowName = false;
    }

    void Update()
    {
        Raycast();

        if(repin == 2)
        {
            repin = 0;
            if(repin == 0)
            {
                unPined = true;
            }
        }

        if (unPined)
        {
            infoCard.transform.position = new Vector3(AIPlayer.transform.position.x + 2f, 1, AIPlayer.transform.position.z);
        }
    }
       
    private void Raycast()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        if (Input.GetButtonDown("Fire1"))
        {
            //ASource.PlayOneShot(Click);
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity) && hit.collider.tag == "AI" && IsOpen == false)
            {
                ASource.PlayOneShot(PopUp);
                ASource.volume = 0.3f;
                infoCard.SetActive(true);
                unPined = true;
                IsOpen = true;
                AIPlayer = hit.transform.gameObject;
                name_text.text = AIPlayer.GetComponent<AIInfoInput>().name_holder;
                age_text.text = AIPlayer.GetComponent<AIInfoInput>().age_holder;
                wage_text.text = AIPlayer.GetComponent<AIInfoInput>().wage_holder;
                job_text.text = AIPlayer.GetComponent<AIInfoInput>().job_holder;
                Goingto_text.text = AIPlayer.GetComponent<AIInfoInput>().Goingto_Holder;
            }
            else if (Physics.Raycast(castPoint, out hit, Mathf.Infinity) && hit.collider.tag == "Ground" && IsOpen == true || hit.collider.tag == "Close") {
                ASource.PlayOneShot(PopDown);
                ASource.volume = 0.3f;
                unPined = true;
                IsOpen = false;
                infoCard.SetActive(false);
                ShowName = false;
            }
            else
                ASource.volume = 0.2f;
        }   

        if(Physics.Raycast(castPoint, out hit, Mathf.Infinity) && hit.collider.tag == "InfoCard")
        {
            infoCard.SetActive(true);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity) && hit.collider.tag == "Pin")
            {
                ASource.PlayOneShot(Pin);
                ASource.volume = 0.3f;
                unPined = false;
                repin += 1;
                infoCard.transform.position = new Vector3(AIPlayer.transform.position.x + 2f, 1, AIPlayer.transform.position.z);
                
            }


        }
    }

}
