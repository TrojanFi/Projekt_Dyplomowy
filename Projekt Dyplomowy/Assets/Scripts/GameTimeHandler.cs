using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeHandler : MonoBehaviour
{
    public static bool nextSceneLoader = false;
    void Start()
    {
        StartCoroutine(Time());
    }
    IEnumerator Time()
    {
        yield return new WaitForSeconds(5);
        nextSceneLoader = true;
       // Debug.Log("End");
    }

}