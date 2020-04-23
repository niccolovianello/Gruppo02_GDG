using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    private Light flashlight;
    private bool isOn;

    private void Start()
    {
        flashlight = this.GetComponent<Light>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isOn = !isOn; 
        }
        if (isOn)
            flashlight.enabled = true;
        else
            flashlight.enabled = false;
    }
}
