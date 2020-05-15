using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour {

    PlayerControl player;

    GameObject columnControl;

    GameObject line;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
        columnControl = GameObject.Find("Column Control");
        line = Resources.Load("Prefabs/Line") as GameObject;
    }

    private void Update()
    {
        CreateLine();

        ControlDeltaTime();
    }

    void ControlDeltaTime()
    {
        if(player.variables.keyHit && player.variables.seenPoint)
        {
            timeInstantiate += Time.deltaTime;

            if (timeInstantiate > 0.5f) disableInstantiate = true;
        }

        if (player.variables.walk) timeInstantiate = 0;
    }

    bool disableInstantiate;
    float timeInstantiate;
    public int i = 0;

    void CreateLine()
    {
        if (player.variables.allowClick)
        {
            if (!player.variables.disableClick)
            {
                if (player.variables.keyDown && player.variables.seenPoint)
                {
                    player.variables.startCreate = true;

                    player.variables.x = columnControl.transform.GetChild(player.variables.getNumber).transform.GetChild(0).transform.GetChild(0).position.x;
                    player.variables.y = columnControl.transform.GetChild(player.variables.getNumber).transform.GetChild(0).transform.GetChild(0).position.y;
                }


                if (player.variables.seenPoint)
                {
                    if (player.variables.keyHit)
                    {
                        if (player.variables.startCreate)
                        {
                            if (!disableInstantiate)
                            {
                                line = Instantiate(line, new Vector3(player.variables.x, player.variables.y, 0), Quaternion.identity);
                                line.transform.parent = columnControl.transform.GetChild(player.variables.getNumber).transform.GetChild(0);
                                player.variables.y += 0.4f;
                            }
                        }
                    }
                }
            }
        }

        if(!player.variables.keyHit && player.variables.startCreate)
        {
            if (i == 0)
            {
                player.variables.disableClick = true;
                player.variables.doneCreateLine = true;
                i++;
            }
        }
    }
}
