using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Variables {

    #region PlayerControl
    [System.NonSerialized]
    public string idleStr = "Idle";
    [System.NonSerialized]
    public string walkStr = "Walk";
    [System.NonSerialized]
    public string kickStr = "Kick";

    [System.NonSerialized]
    public bool idle;
    [System.NonSerialized]
    public bool walk;
    [System.NonSerialized]
    public bool kick;

    [System.NonSerialized]
    public bool endGame;
    [System.NonSerialized]
    public bool allowClick;
    [System.NonSerialized]
    public bool disableClick;
    [System.NonSerialized]
    public bool keyHit;
    [System.NonSerialized]
    public bool keyHitUp;
    [System.NonSerialized]
    public bool keyDown;
    [System.NonSerialized]
    public bool gameBegin;
    [System.NonSerialized]
    public bool fall;
    [System.NonSerialized]
    public bool seenPoint;

    [System.NonSerialized]
    public float gravityScale = 200;
    [System.NonSerialized]
    public float speed = 4;
    [System.NonSerialized]
    public float distance = 0.6f;
    [System.NonSerialized]
    public float distance_D = 1;

    [System.NonSerialized]
    public int getNumber = 0;
    [System.NonSerialized]
    public int columnOrder = -1;
    #endregion


    #region LineControl
    [System.NonSerialized]
    public float x, y;
    [System.NonSerialized]
    public bool startCreate;
    [System.NonSerialized]
    public bool doneCreateLine;
    #endregion


    #region Point

    [System.NonSerialized]
    public bool rotating = true;
    [System.NonSerialized]
    public float timeToRotate = .5f;
    [System.NonSerialized]
    public bool rotateWhenFall;

    #endregion


    #region Camera Control
    [System.NonSerialized]
    public float xOffset = 4.2f;
    [System.NonSerialized]
    public float yOffest = 2;
    [System.NonSerialized]
    public float cameraLerpSpeed = 10;

    #endregion


    #region Column Control

    [System.NonSerialized]
    public float min_D_B = 6;
    [System.NonSerialized]
    public float max_D_B = 10;
    [System.NonSerialized]
    public float min_D_N = 7;
    [System.NonSerialized]
    public float max_D_N = 9;
    [System.NonSerialized]
    public float min_D_S = 4;
    [System.NonSerialized]
    public float max_D_S = 7;

    [System.NonSerialized]
    public float startX = -0.35f;
    [System.NonSerialized]
    public float starY = -6.3f;

    #endregion


    #region Check For Falling
    [System.NonSerialized]
    public bool hitFallPoint;
    #endregion
}
