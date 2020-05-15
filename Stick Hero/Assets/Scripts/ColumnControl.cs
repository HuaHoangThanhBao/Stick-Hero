using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnControl : MonoBehaviour {
    
    public enum ColumnStyle
    {
        ColumnBig,
        ColumnNormal,
        ColumnSmall
    }

    public ColumnStyle columnStyle;

    GameObject columnBig;
    GameObject columnSmall;
    GameObject columnNormal;

    GameObject a, b, c;

    PlayerControl player;

    public int temp = 0;
    int temp1 = 0;
    float random_distance_X;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
        columnBig = Resources.Load("Prefabs/Column Big") as GameObject;
        columnNormal = Resources.Load("Prefabs/Column Normal") as GameObject;
        columnSmall = Resources.Load("Prefabs/Column Small") as GameObject;
    }

    private void Start()
    {
        temp = 0;
        a = Instantiate(columnBig, new Vector3(player.variables.startX, player.variables.starY, 0), Quaternion.identity);
        a.transform.parent = transform;
    }

    private void Update()
    {
        SmartCreateColumn();

        DestroyDeltaTime();

    }

    void DestroyDeltaTime()
    {
        if (player.variables.columnOrder > 1)
        {
            if (temp1 == 0)
            {
                player.variables.getNumber -= 1;
                Destroy(transform.GetChild(0).gameObject);
                temp1++;
            }
        }
    }

    void SmartCreateColumn()
    {
        if (player.variables.seenPoint)
        {
            if (temp == 0)
            {
                temp1 = 0;

                int random_column_type = Random.Range(1, 10);

                switch (random_column_type)
                {
                    case 1:
                    case 2:
                    case 5:
                    case 9:
                        columnStyle = ColumnStyle.ColumnSmall;
                        break;

                    case 3:
                    case 4:
                    case 6:
                        columnStyle = ColumnStyle.ColumnNormal;
                        break;

                    case 7:
                    case 8:
                    case 10:
                        columnStyle = ColumnStyle.ColumnBig;
                        break;
                }

                if (columnStyle == ColumnStyle.ColumnBig)
                {
                    random_distance_X = Random.Range(player.variables.startX + player.variables.min_D_B,
                        player.variables.startX + player.variables.max_D_B);
                }
                if (columnStyle == ColumnStyle.ColumnNormal)
                {
                    random_distance_X = Random.Range(player.variables.startX + player.variables.min_D_N,
                        player.variables.startX + player.variables.min_D_N);
                }
                if(columnStyle == ColumnStyle.ColumnSmall)
                {
                    random_distance_X = Random.Range(player.variables.startX + player.variables.min_D_S,
                        player.variables.startX + player.variables.max_D_S);
                }

                switch (columnStyle)
                {
                    case ColumnStyle.ColumnBig:

                        a = Instantiate(columnBig, new Vector3(random_distance_X, player.variables.starY, 0), Quaternion.identity);
                        a.transform.parent = transform;
                        temp++;
                        break;

                    case ColumnStyle.ColumnNormal:

                        b = Instantiate(columnNormal, new Vector3(random_distance_X, player.variables.starY, 0), Quaternion.identity);
                        b.transform.parent = transform;
                        temp++;
                        break;

                    case ColumnStyle.ColumnSmall:

                        c = Instantiate(columnSmall, new Vector3(random_distance_X, player.variables.starY, 0), Quaternion.identity);
                        c.transform.parent = transform;
                        temp++;
                        break;
                }

                player.variables.startX = random_distance_X;
            }
        }
        else temp = 0;
    }
}
