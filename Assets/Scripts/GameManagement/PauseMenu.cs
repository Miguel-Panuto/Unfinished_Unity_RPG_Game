using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject main;
    [SerializeField]
    private GameObject[] menus;

    private GameObject[] keybindButtons;
    private bool menuActive;

    private static PauseMenu instace;

    public static PauseMenu Instace
    {
        get
        {
            if (instace == null)
            {
                instace = FindObjectOfType<PauseMenu>();
            }
            return instace;
        }
    }

    void Awake() 
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    void Start()
    {
        menuActive = false;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCloseMenu();
        }
        
    }
    
    public void OpenCloseMenu()
    {
        menuActive = !menuActive;
        menu.SetActive(menuActive);
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        if (!menuActive)
        {
            gameObject.GetComponent<KeybindScript>().SaveKeys();
            for(int i = 0; i < menus.Length; i++)
            {
                menus[i].SetActive(false);
            }
            main.SetActive(true);
        }
    }
}
