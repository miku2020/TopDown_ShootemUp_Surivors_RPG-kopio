using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Interraction : MonoBehaviour
{

    [SerializeField]
    private GameObject EText;
    private bool EButton = false;
    private Dq_manager DQmanager;

    // Start is called before the first frame update
    void Start()
    {
        DQmanager = FindObjectOfType<Dq_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EButton == true && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("E works");
            DQmanager.InitializeDQ();
        }
    }


    private void OnTriggerEnter2D(Collider2D PlayerDect)
    {
        if (PlayerDect.gameObject.tag == "Player")
        {
            EText.GetComponent<TMP_Text>().enabled = true;
            EButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D PlayerDect)
    {
        if (PlayerDect.gameObject.tag == "Player")
        {
            EText.GetComponent<TMP_Text>().enabled = false;
            EButton = false;
        }
    }


}

