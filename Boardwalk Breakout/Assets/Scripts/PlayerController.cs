using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public float speed = 7.0f;
    public float rotationSpeed = 3.0f;

    bool isGrounded;
    Rigidbody rb;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
            isGrounded = false;
    }

    private Vector3 pos;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        pos = transform.position;

        if (Input.GetKey("w"))
        {
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotationSpeed);
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * rotationSpeed);
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * rotationSpeed);
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * rotationSpeed);
            pos.x -= speed * Time.deltaTime;
        }


        transform.position = pos;



        


    }
}





