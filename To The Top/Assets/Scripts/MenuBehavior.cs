using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    //Variables
    //Main menu -- Create, Join, Credits, Quit
    public bool TitleVisible = true;
    public bool SubtitleVisible = true;
    public bool MainMenuVisible = true;
    public bool CreateGameVisible = false;
    public bool JoinGameVisible = false;
    public bool CreditsVisible = false;
    public bool SettingsVisible = true;
    public bool QuitVisible = true;

    public GameObject TitleText;
    public GameObject SubtitleText;
    public GameObject CreateGameUI;
    public GameObject JoinGameUI;
    public GameObject CreditsUI;
    public GameObject SettingsUI;
    public GameObject QuitUI;

    //Create Game menu -- SetTime, SetPlayer, GenRoomCode, Start, Back
    public bool GenRoomCodeVisible = false;
    public bool SetTimeVisible = false;
    public bool SetPlayerVisible = false;
    public bool StartVisible = false;
    public bool BackVisible = false;

    public GameObject GeneratedRoomCode;
    public GameObject StartUI;

    //Join Game menu -- RoomCode, Join, Back
    public bool JoinRoomCodeVisible = false;
    public bool JoinVisible = false;

    public GameObject JoinUI;

    //Credits menu -- Credits, Copyright, Back
    public bool CreditsTextVisible = false;
    public bool CopyrightTextVisible = false;

    public GameObject CreditsText;
    public GameObject CopyrightText;
    

    // Start is called before the first frame update
    void Start()
    {
        //Main menu 
        TitleText.SetActive(TitleVisible);
        SubtitleText.SetActive(SubtitleVisible);
        CreateGameUI.SetActive(CreateGameVisible);
        JoinGameUI.SetActive(JoinGameVisible);
        CreditsUI.SetActive(CreditsVisible);
        SettingsUI.SetActive(SettingsVisible);
        QuitUI.SetActive(QuitVisible);
        
        // For MainMenuVisible, we'll should do SetActive to the children, just in case we need to spawn in the scene with it hidden
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Exits the game
    // Prompts user to confirm choice
   void quitGame()
    {
        Application.Quit();
    }
   

    // We'll probably want functions to enable or disable the various menus so that we can tie them to the buttons

    // Makes Create Game menu visible and disables the other menus

    // Makes Join Game menu visible and disables the other menus

    // Makes Credits Menu visible and disables the other menus
    void selectCredits()
    {
        CreditsText.SetActive(CreditsTextVisible);
        CopyrightText.SetActive(CopyrightTextVisible);
    }

}
