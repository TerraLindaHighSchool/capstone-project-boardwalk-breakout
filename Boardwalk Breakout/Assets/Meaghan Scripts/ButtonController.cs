using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject door;
    private bool pressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pressed = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            door.transform.localRotation = Quaternion.Slerp(door.transform.localRotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 2);
        }
    }
}
