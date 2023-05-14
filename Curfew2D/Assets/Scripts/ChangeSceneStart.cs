using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneStart : MonoBehaviour
{
    public void SceneStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
