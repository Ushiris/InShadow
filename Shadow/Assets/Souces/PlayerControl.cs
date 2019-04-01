using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed;
    public float JumpTime;
    public float JumpPower;
    public Vector3 ShadeGravity;

    bool IsShade = false;
    bool IsJump = false;
    float StopWatch;
    Rigidbody player;
    
    
	// Use this for initialization
	void Start () {
        player = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if(IsShade)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-Time.deltaTime * speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                player.AddForce(new Vector3(0, speed / 2.0f));
            }
            if (Input.GetKeyDown(KeyCode.Space) && (!IsJump))
            {
                player.AddForce(new Vector3(0, JumpPower));
                IsJump = true;
                StopWatch = Time.time;
            }
            player.AddForce(ShadeGravity, ForceMode.Acceleration);
        }

        if(IsJump)
        {
            if (Time.time - StopWatch >= JumpTime)
                IsJump = false;
        }

	}

    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Shadow")
        {
            IsShade = true;
            player.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Shadow")
        {
            player.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Shadow")
        {
            IsShade = false;
            player.useGravity = true;
        }

    }

}
