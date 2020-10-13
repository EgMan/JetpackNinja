using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject OptionsCanvas, ControlsCanvas;
    public TextMeshProUGUI instructions;
    public TextMeshProUGUI YButton, LeftThumbstick, XButton, StartButton, BButton, RightThumbstick, AButton, SystemButton, LeftIndexTrigger, RightIndexTrigger, LeftHandTrigger, RightHandTrigger;
    public enum State
    {
        MainMenu,
        Controls,
        Movement,
        Turning,
        ApplyingThrust,
        Flying,
        Climbing,
        Shooting,
        GameMenu,
        Reset,
    };
    private State state = State.MainMenu;
    private void Start() {
        ControlsCanvas.SetActive(false);
    }
    private void Update() {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        switch (state)
        {
            case State.MainMenu:
                OptionsCanvas.SetActive(true);
                ControlsCanvas.SetActive(false);
                break;
            case State.Controls:
                OptionsCanvas.SetActive(false);
                ControlsCanvas.SetActive(true);
                resetTextColor();
                state = State.Movement;
                break;
            case State.Movement:
                instructions.SetText("Use the left thumbstick to move around");
                LeftThumbstick.color = Color.red;
                if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).magnitude > 0.01)
                {
                    LeftThumbstick.color = Color.black;
                    state = State.Turning;
                }
                break;
            case State.Turning:
                instructions.SetText("Tilt the right thumbstick left and right to yaw");
                RightThumbstick.color = Color.red;
                if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x > 0.01)
                {
                    RightThumbstick.color = Color.black;
                    state = State.ApplyingThrust;
                }
                break;
            case State.ApplyingThrust:
                instructions.SetText("Use the left index trigger to apply thrust");
                LeftIndexTrigger.color = Color.red;
                if  (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0.01)
                {
                    LeftIndexTrigger.color = Color.black;
                    state = State.Flying;
                    Invoke("stateClimbing", 10f);
                }
                break;
            case State.Flying:
                instructions.SetText("Make sure to keep an eye on your fuel, and don't hit the ground too hard.");
                break;
            case State.Climbing:
                instructions.SetText("You can climb on anything made of wood.  Use the hand triggers to grab on.");
                LeftHandTrigger.color = Color.red;
                RightHandTrigger.color = Color.red;
                if  (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
                {
                    LeftHandTrigger.color = Color.black;
                    RightHandTrigger.color = Color.black;
                    state = State.Shooting;
                }
                break;
            case State.Shooting:
                instructions.SetText("I haven't implemented shooting yet, but this is how you'll do it lol");
                RightIndexTrigger.color = Color.red;
                if  (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.01)
                {
                    RightIndexTrigger.color = Color.black;
                    state = State.GameMenu;
                }
                break;
            case State.GameMenu:
                instructions.SetText("Access the menu at any time with the left menu button.");
                StartButton.color = Color.red;
                if  (OVRInput.Get(OVRInput.Button.Start))
                {
                    StartButton.color = Color.black;
                    state = State.Reset;
                }
                break;
            case State.Reset:
                instructions.SetText("Finally, reset the level using buttons Y and B. :)");
                YButton.color = Color.red;
                BButton.color = Color.red;
                break;
        }
    }
    public void EnterFreePlay()
    {
        SceneManager.LoadScene("FreeFly");
    }

    public void EnterTrial(int a)
    {
        SceneManager.LoadScene($"Lv{a}");
    }

    public void setState(string state)
    {
        this.state = (State)System.Enum.Parse(typeof(State), state, true);
    }
    private void stateClimbing()
    {
        this.state = State.Climbing;
    }
    private void resetTextColor()
    {
        YButton.color = Color.black;
        LeftThumbstick.color = Color.black;
        XButton.color = Color.black;
        StartButton.color = Color.black;
        BButton.color = Color.black;
        RightThumbstick.color = Color.black;
        AButton.color = Color.black;
        SystemButton.color = Color.black;
        LeftIndexTrigger.color = Color.black;
        RightIndexTrigger.color = Color.black;
        LeftHandTrigger.color = Color.black;
        RightHandTrigger.color = Color.black;
    }
}
