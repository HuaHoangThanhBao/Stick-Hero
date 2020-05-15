using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    Animator anim;

    Rigidbody2D rb;

    public LayerMask mask;

    public enum State
    {
        Idle,
        Walk,
        Kick
    }

    public State state;

    public Variables variables;

    LineControl lineControl;

    GameObject columnControl;

    int tempResetColumn = 0;
    int tempResetRotateFall = 0;

    private void Awake()
    {
        lineControl = FindObjectOfType<LineControl>();
        columnControl = GameObject.Find("Column Control");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        variables.allowClick = true;
    }

    private void Update()
    {
        Inputs();
        CheckForPoint();
        Movement();
        HandleAnimation();
    }

    void Movement()
    {
        if(variables.gameBegin)
        {
            if (variables.walk)
            {
                rb.velocity = Vector2.right * variables.speed;
            }
            else rb.velocity = Vector3.zero;
        }
    }

    void Inputs()
    {
        if(Input.GetMouseButton(0))
        {
            variables.keyHit = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            variables.keyHit = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            variables.keyDown = true;
        }
        else variables.keyDown = false;

        if (Input.GetMouseButtonUp(0))
        {
            variables.keyHitUp = true;
        }
        else variables.keyHitUp = false;
    }

    void HandleAnimation()
    {
        if(variables.gameBegin)
        {
            if (!variables.fall)
            {
                if (!variables.seenPoint)
                {
                    variables.startCreate = false;
                    lineControl.i = 0;
                    FindObjectOfType<ColumnControl>().temp = 0;

                    variables.walk = true;
                    variables.idle = false;
                    variables.kick = false;
                }
                else
                {
                    if (tempResetColumn == 0)
                    {
                        variables.columnOrder++;
                        tempResetColumn++;
                    }

                    if(variables.doneCreateLine)
                    {
                        variables.allowClick = false;
                    }
                    
                    variables.walk = false;
                    variables.idle = true;
                    variables.kick = false;
                }

                if (variables.doneCreateLine)
                {
                    variables.walk = false;
                    variables.idle = false;
                    variables.kick = true;
                }
                else variables.kick = false;

                if (variables.columnOrder > -1)
                {
                    variables.disableClick = false;
                }

                if (columnControl.transform.GetChild(variables.getNumber).transform.GetChild(0).GetComponent<Point>().doneRotate)
                {
                    variables.walk = true;
                    variables.idle = false;
                    variables.kick = false;
                    variables.seenPoint = false;
                    columnControl.transform.GetChild(variables.getNumber).transform.GetChild(0).GetComponent<Point>().doneRotate = false;
                    variables.getNumber++;
                }

                HandleState();
            }
            else
            {
                ApplyGravityOnFalling();

                if (tempResetRotateFall == 0)
                {
                    columnControl.transform.GetChild(variables.getNumber - 1).transform.GetChild(0).GetComponent<Point>().RotateWhenFall();
                    tempResetRotateFall++;
                }
            }
        }
    }

    void ApplyGravityOnFalling()
    {
        GetComponent<CapsuleCollider2D>().enabled = false; //Disable capsule collider
        rb.velocity = Vector3.zero;
        rb.gravityScale = variables.gravityScale;
    }

    void HandleState()
    {
        if (variables.walk)
        {
            state = State.Walk;
            tempResetColumn = 0;
        }

        if (variables.idle) state = State.Idle;

        if (variables.kick) state = State.Kick;


        if (columnControl.transform.GetChild(variables.getNumber).transform.GetChild(0).GetComponent<Point>().doneRotate)
        {
            state = State.Walk;
        }

        switch (state)
        {
            case State.Walk:
                anim.SetBool(variables.walkStr, true);
                break;
            case State.Idle:
                anim.SetBool(variables.walkStr, false);
                break;
            case State.Kick:
                anim.SetBool(variables.kickStr, true);
                break;
        }
    }

    void CheckForPoint()
    {
        Debug.DrawRay(transform.position, Vector3.right * variables.distance, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, variables.distance, mask);

        RaycastHit2D hit_D = Physics2D.Raycast(transform.position, Vector3.down, variables.distance_D, mask);

        if(hit_D.collider == null)
        {
            variables.fall = true;
        }

        if (hit.collider != null)
        {
            if (hit.transform.tag == "Point")
            {
                variables.seenPoint = true;
            }
        }
        else
        {
            if (FindObjectOfType<Point>().disableBoxCol)
                variables.seenPoint = false;
        }
    }

    public void AllowClick()
    {
        variables.allowClick = true;
    }

    public void NonAllowClick()
    {
        variables.allowClick = false;
    }
    
    public void DisableRotateLine()
    {
        variables.doneCreateLine = false;
        anim.SetBool(variables.kickStr, false);
    }

    public void RotateLine()
    {
        columnControl.transform.GetChild(variables.getNumber).transform.GetChild(0).GetComponent<Point>().RotateLine();
    }
}
