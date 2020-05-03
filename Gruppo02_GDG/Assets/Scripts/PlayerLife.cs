using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int playerHealth = 100;
    int plCurrentHealth;

    //CHANGE
    public Light torch_light;
    public Light flashlight_light;

    // access equipment list

    //CHANGE END

    // Start is called before the first frame update
    void Start()
    {
        plCurrentHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //if(/*torch_light.enabled == false &&*/ flashlight_light.enabled == false)
        //{
        //    Die();
        //}
    }

    void Die()
    {
        Debug.Log("alldark");

        StartCoroutine(ExecuteAfterTime(2f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("i'm_dead");
        //Destroy(gameObject);
    }
}
