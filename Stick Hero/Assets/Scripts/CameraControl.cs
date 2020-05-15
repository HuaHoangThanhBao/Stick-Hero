using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    PlayerControl player;

    float timeFall;

    int temp = 0;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void Update()
    {
        Movement();   

        if(player.variables.fall)
        {
            timeFall += Time.deltaTime;

            if(timeFall > 0.7f)
            {
                if(temp == 0)
                {
                    StartCoroutine(Shake(.05f, .4f));
                    temp++;
                }
            }
        }
    }

    int i = 0;

    void Movement()
    {
        if (player.variables.seenPoint && !player.variables.startCreate)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + player.variables.xOffset, 
                player.transform.position.y + player.variables.yOffest, -10), Time.deltaTime * player.variables.cameraLerpSpeed);
        }
    }


    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = transform.localPosition.x + Random.Range(-0.2f, 0.2f) * magnitude;
            float y = transform.localPosition.y + Random.Range(-0.2f, 0.2f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        player.variables.endGame = true;
    }
}
