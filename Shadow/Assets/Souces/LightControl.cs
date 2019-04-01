using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {

    Vector3 moucepos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        moucepos = Input.mousePosition;

        //マウスで指し示す場所がワールド座標のz=0であるという意思表示（重要）
        moucepos.z = -Camera.main.transform.position.z;

        //カーソル君の座標をワールド座標に変換
        Vector3 mouceWorldPos = Camera.main.ScreenToWorldPoint(moucepos);

        transform.position = mouceWorldPos;
    }
}
