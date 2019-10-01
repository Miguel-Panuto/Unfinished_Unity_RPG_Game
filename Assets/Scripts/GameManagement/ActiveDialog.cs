using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDialog : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogSystem;
    private Dialog dialog;

    void Start()
    {
        dialog = GetComponent<Dialog>();
        dialog.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            dialog.index = 0;
            dialogSystem.SetActive(true);
            dialog.enabled = true;
            dialog.StartDialog();
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            dialog.SetTextsNull();
            dialog.enabled = false;
            dialogSystem.SetActive(false);
        }
    }
}
