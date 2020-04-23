using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchOnOff : MonoBehaviour
{
    
    public bool isOn;
    public ParticleSystem fire;
    public Light fireLight;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isOn)
            {
                fireLight.enabled = false;
                fire.Stop();
                isOn = false;
            }
            else
            {
                fireLight.enabled = true;
                fire.Play();
                isOn = true;
            }
                
        }
        
    }
}
