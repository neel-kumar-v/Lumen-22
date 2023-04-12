//333333333333333333333333333333333333333333333333333333333333333333\\
//
//          Arthur: Cato Parnell
//          Description of script: control keypad button clicks and actions
//          Any queries please go to Youtube: Cato Parnell and ask on video. 
//          Thanks.
//
//33333333333333333333333333333333333333333333333333333333333333333\\

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keypad : MonoBehaviour
{
    // *** CAN DELETE THESE ** \\
    // Used to hide joystick and slider
    [Header("Objects to Hide/Show")]

    // Object to be enabled is the keypad. This is needed
    public GameObject objectToEnable;

    // *** Breakdown of header(public) variables *** \\
    // curPassword : Pasword to set. Ive set the password in the project. Note it can be any length and letters or numbers or sysmbols
    // input: What is currently entered
    // displayText : Text area on keypad the password entered gets displayed too.
    // audioData : Play this sound when user enters in password incorrectly too many times

    [Header("Keypad Settings")]
    public string curPassword = "123";
    public string input;
    public Text displayText;
    public AudioSource audioData;
    public GameObject text;

    //Local private variables
    private bool keypadScreen;
    private float btnClicked = 0;
    private float numOfGuesses;
    [SerializeField] private ShootLaser shootLaser;
    public static bool paused = false;

    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        btnClicked = 0; // No of times the button was clicked
        numOfGuesses = curPassword.Length; // Set the password length.
    }

    // Update is called once per frame
    void Update()
    {
        if (btnClicked == numOfGuesses)
        {
            if (input == curPassword)
            {
                shootLaser.doorTurnOff.SetActive(false);
                shootLaser.laserOn = false;
                shootLaser.audioManager.StopSound("LaserHum");
                shootLaser.audioManager.StopSound("LaserElectrical");
                shootLaser.audioManager.PlaySound("Crash");
                text.SetActive(true);
                shootLaser.doorAnimator.Play("TextShow");
                shootLaser.OnDoorHit();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                keypadScreen = false;
            }
            else
            {
                //Reset input varible
                input = "";
                displayText.text = input.ToString();
                audioData.Play();
                btnClicked = 0;
            }

        }

    }

    void OnGUI()
    {
        // Action for clicking keypad( GameObject ) on screen
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                var selection = hit.transform;

                if (selection.CompareTag("keypad")) // Tag on the gameobject - Note the gameobject also needs a box collider
                {
                    keypadScreen = true;

                    var selectionRender = selection.GetComponent<Renderer>();
                    if (selectionRender != null)
                    {
                        keypadScreen = true;
                        
                    }
                }

            }
        }

        // Disable sections when keypadScreen is set to true
        if (keypadScreen)
        {
            objectToEnable.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            PlayerMovement.paused = true;
            MouseLook.paused = true;  
        }
        else
        {
            objectToEnable.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.paused = false;
            MouseLook.paused = false;  
            
        }

    }

    public void ValueEntered(string valueEntered)
    {
        switch (valueEntered)
        {
            case "Q": // QUIT
                objectToEnable.SetActive(false);
                btnClicked = 0;
                keypadScreen = false;
                input = "";
                displayText.text = input.ToString();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case "C": //CLEAR
                input = "";
                btnClicked = 0;// Clear Guess Count
                displayText.text = input.ToString();
                break;

            default: // Buton clicked add a variable
                btnClicked++; // Add a guess
                input += valueEntered;
                displayText.text = input.ToString();
                break;
        }


    }
}
