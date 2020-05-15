using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpController : MonoBehaviour {

    GameObject startUp;

    private void Awake()
    {
        startUp = Resources.Load("Prefabs/StartUp") as GameObject;
    }

    private void Start()
    {
        startUp = Instantiate(startUp, Vector3.zero, Quaternion.identity);

        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(4);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StickHero");
    }
}
