using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    GameObject a, b;

    private void Awake()
    {
        a = GameObject.Find("Top Point") as GameObject;
        b = GameObject.Find("Bottom Point") as GameObject;
    }

    private void Update()
    {
        float time = Mathf.PingPong(Time.time * 1.5f, 1);
        transform.position = Vector3.Lerp(b.transform.position, a.transform.position, time);
    }
}
