using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void DoControlStuff()
    {
        SceneManager.LoadScene("ControlScene");

    }

    public void DoMainStuff()
    {
        SceneManager.LoadScene("TPTScene");
    }
}
