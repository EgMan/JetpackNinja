using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberHand : MonoBehaviour
{
    // Should be OVRInput.Controller.LTouch or OVRInput.Controller.RTouch.
    [SerializeField]
    protected OVRInput.Controller m_controller;
    public float grabBegin = 0.55f;
    public float grabEnd = 0.35f;
    public bool grabbing = false;
    private int intersectingClimbable = 0;
    public Collider grabbedObject = null;
    //private List<Collider> climbables = new List<Collider>();

    public Collider getGrabbedCollider()
    {
        if (intersectingClimbable == 0)
        {
            return null;
        }
        return grabbedObject;
    }
    private void Update() {
        if (intersectingClimbable != 0)
        {
            float grabInput = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
            if (grabInput > grabBegin)
            {
                grabbing = true;
            }
            else if (grabInput < grabEnd)
            {
                grabbing = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            if (intersectingClimbable == 0)
            {
                grabbedObject = other;
            }
            intersectingClimbable++;
            //climbables.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            intersectingClimbable = 0;
            //climbables.Remove(other);
        }
    }
}
