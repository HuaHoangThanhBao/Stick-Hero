using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForFalling : MonoBehaviour {
    
    PlayerControl player;

    GameObject columnControl;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
        columnControl = GameObject.Find("Column Control");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "FallPoint" && !player.variables.allowClick)
        {
            player.variables.hitFallPoint = true;

            columnControl.transform.GetChild(player.variables.getNumber + 1).transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
