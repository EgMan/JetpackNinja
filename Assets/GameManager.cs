using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject wristMenu;
    private bool startButtonPreviousFrame;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Two) || OVRInput.Get(OVRInput.Button.Four) || Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        bool startButtonCurrentFrame = OVRInput.Get(OVRInput.Button.Start);
        if (startButtonPreviousFrame && !startButtonCurrentFrame)
        {
            wristMenu.SetActive(!wristMenu.activeInHierarchy);
        }
        startButtonPreviousFrame = startButtonCurrentFrame;
    }
}
