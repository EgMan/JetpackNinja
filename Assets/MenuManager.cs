using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject OptionsCanvas, ControlsCanvas;
    public enum State
    {
        MainMenu,
        Controls,
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
}
