using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    PlayerControl player;

    public bool doneRotate;
    public bool disableBoxCol;
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    public void RotateLine()
    {
        StartCoroutine(DoRotate(new Vector3(0, 0, -90)));
    }

    public void RotateWhenFall()
    {
        StartCoroutine(DoRotateFall(new Vector3(0, 0, -90)));
    }

    private IEnumerator DoRotate(Vector3 rotation)
    {
        Quaternion start = transform.rotation;
        Quaternion destination = start * Quaternion.Euler(rotation);
        float startTime = Time.time;
        float percentComplete = 0f;
        player.variables.rotating = true;
        while (percentComplete <= 1.0f)
        {
            percentComplete = (Time.time - startTime) / player.variables.timeToRotate;
            transform.rotation = Quaternion.Slerp(start, destination, percentComplete);
            yield return null;
        }
        player.variables.rotating = false;
        doneRotate = true;
        disableBoxCol = true;
    }


    private IEnumerator DoRotateFall(Vector3 rotation)
    {
        Quaternion start = transform.rotation;
        Quaternion destination = start * Quaternion.Euler(rotation);
        float startTime = Time.time;
        float percentComplete = 0f;
        player.variables.rotateWhenFall = true;
        while (percentComplete <= 1f)
        {
            percentComplete = (Time.time - startTime) / player.variables.timeToRotate * 4;
            transform.rotation = Quaternion.Slerp(start, destination, percentComplete);
            yield return null;
        }
        player.variables.rotateWhenFall = false;
    }
}
