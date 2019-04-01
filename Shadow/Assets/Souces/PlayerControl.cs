using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed;         //秒速。
    public float JumpTime;      //ジャンプのクールタイム
    public float JumpPower;     //ジャンプ力
    public Vector3 ShadeGravity;//影内での重力。

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
            //操作関係。影の中にいないときは行動できないようになっている。
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

            //ジャンプの処理。
            if (Input.GetKeyDown(KeyCode.Space) && (!IsJump))
            {
                player.AddForce(new Vector3(0, JumpPower));
                IsJump = true;
                StopWatch = Time.time;
            }

            //影内での重力処理。
            player.AddForce(ShadeGravity, ForceMode.Acceleration);
        }

        //ジャンプのクールタイムの確認
        if(IsJump)
        {
            if (Time.time - StopWatch >= JumpTime)
                IsJump = false;
        }

	}

    private void OnTriggerStay(Collider other)
    {
        //影内に居る時には通常の重力を消す。
        if (other.transform.tag == "Shadow")
        {
            IsShade = true;
            player.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //陰に入った段階で一度速度をゼロにする。（そうしないと基本の重力による加速で普通に落ちてしまうため。）
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
