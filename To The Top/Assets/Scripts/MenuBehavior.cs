using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    // Shared
        public GameObject BackToMM;     // Button to return to menu
        //public bool BackToMMVisible = false;

        public GameObject BackToMP;     // Button to return to multiplayer menu
        //public bool BackToMPVisible = false;

    //~~ Main menu (MM) ~~
        public GameObject TitleText;
        public GameObject SubtitleText;
        public GameObject SingleplayerMM;
        public GameObject MultiplayerMM;
        public GameObject SettingsMM;
        public GameObject CreditsMM;
        public GameObject QuitMM;

        //NOTE: IDK if we need the visible variables. They don't actually do anything anymore.
        // I think we should just cut the 'visible' variables and go entirely off methods
        /*
        public bool TitleTextVisible = true;
        public bool SubtitleTextVisible = true;
        public bool SingleplayerMMVisible = true;
        public bool MultiplayerMMVisible = true;
        public bool SettingsMMVisible = true;
        public bool CreditsMMVisible = true;
        public bool QuitMMVisible = true;
        */

    //~~ Settings (ST) ~~
        public GameObject SoundST;
        // BackToMM Object

        //public bool SoundSTVisible = false;
        // BackToMM Visible

    //~~ Multiplayer (MP) ~~
        public GameObject CreateRoomMP;
        public GameObject JoinRoomMP;
        // BackToMM Object

        //public bool CreateRoomMPVisible = false;
        //public bool JoinRoomMPVisible = false;
        // BackToMM Visible

    //~~ Create Room (CR) ~~
        public GameObject PlayerCountCR;    // Readout for editing player count
        public GameObject TimeSettingCR;    // Readout for editing timer
        public GameObject NextPlayerCR;     // Button for selecting next player count
        public GameObject PrevPlayerCR;     // Button for selecting previous player count
        public GameObject NextTimeCR;     // Button for selecting next player count
        public GameObject PrevTimeCR;     // Button for selecting previous player count
        public GameObject CreateGameCR;     // Button for creating a room with the selected settings
                                            // BackToMP Object
        /*
        public bool PlayerCountCRVisible = false;
        public bool TimeSettingCRVisible = false;
        public bool NextPlayerCRVisible = false;
        public bool PrevPlayerCRVisible = false;
        public bool NextTimeCRVisible = false;
        public bool PrevTimeCRVisible = false;
        public bool CreateGameCRVisible = false;
        */
        // BackToMP Visible

    //~~ Join Room (JR) ~~
        public GameObject RoomButton1JR;    // Button for selecting the 1st room on the list
        public GameObject RoomButton2JR;    // Button for selecting the 2nd room on the list
        public GameObject RoomButton3JR;    // Button for selecting the 3rd room on the list
        public GameObject RoomButton4JR;    // Button for selecting the 4th room on the list
        public GameObject NextPageJR;       // Button for selecting the next page of rooms
        public GameObject PrevPageJR;       // Button for selecting the previous page of rooms
        public GameObject JoinRoomJR;       // Button for joining the selected room
        public GameObject JoinRandRoomJR;   // Button for joining a random room; testing/debug ??
        // BackToMP Object
        /*
        public bool RoomButton1JRVisible = false;
        public bool RoomButton2JRVisible = false;
        public bool RoomButton3JRVisible = false;
        public bool RoomButton4JRVisible = false;
        public bool NextPageJRVisible = false;
        public bool PrevPageJRVisible = false;
        public bool JoinRoomJRVisible = false;
        public bool JoinRandRoomJRVisible = false;
    */
        // BackToMP Visible

    //~~ Credits (CD)~~
        public GameObject CreditsTextCD;
        public GameObject CopyrightTextCD;
        // BackToMM Object

        //public bool CreditsTextCDVisible = false;
        //public bool CopyrightTextCDVisible = false;
        // BackToMM Visible

    //~~ Quit Game (QG)
        public GameObject QuitYesQG;    // Button to confirm quitting game
        public GameObject QuitNoQG;     // Button to cancel quitting game, returns to MM

        //public bool QuitYesQGVisible = false;
        //public bool QuitNoQGVisible = false;


    // Start is called before the first frame update
    void Start()
    {   
        menuMM();
        Debug.Log("Starting menu behavior script");
    }

    // Sets the visibility of each element to false, effectively clearing the menu
    void menuHideAllElements()
    {
        //.SetActive(false);

        BackToMM.SetActive(false);
        BackToMP.SetActive(false);

        TitleText.SetActive(false);
        SubtitleText.SetActive(false);
        SingleplayerMM.SetActive(false);
        MultiplayerMM.SetActive(false);
        SettingsMM.SetActive(false);
        CreditsMM.SetActive(false);
        QuitMM.SetActive(false);

        SoundST.SetActive(false);

        CreateRoomMP.SetActive(false); 
        JoinRoomMP.SetActive(false);

          PlayerCountCR.SetActive(false);   
          TimeSettingCR.SetActive(false); 
          NextPlayerCR.SetActive(false);    
          PrevPlayerCR.SetActive(false); 
          CreateGameCR.SetActive(false);  

          RoomButton1JR.SetActive(false); 
          RoomButton2JR.SetActive(false); 
          RoomButton3JR.SetActive(false);  
          RoomButton4JR.SetActive(false);  
          NextPageJR.SetActive(false);    
          PrevPageJR.SetActive(false);  
          JoinRoomJR.SetActive(false);
          JoinRandRoomJR.SetActive(false);

          CreditsTextCD.SetActive(false);
          CopyrightTextCD.SetActive(false);

          QuitYesQG.SetActive(false);  
          QuitNoQG.SetActive(false);

        Debug.Log("Hiding All Elements");

}

    void menuMM()
    {
        menuHideAllElements();

        TitleText.SetActive(true);
        SubtitleText.SetActive(true);
        SingleplayerMM.SetActive(true);
        MultiplayerMM.SetActive(true);
        SettingsMM.SetActive(true);
        CreditsMM.SetActive(true);
        QuitMM.SetActive(true);

        Debug.Log("Showing Main Menu");
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
    void menuCD()
    {
        /*
        CreditsText.SetActive(CreditsTextVisible);
        CopyrightText.SetActive(CopyrightTextVisible);
        */
    }

}
