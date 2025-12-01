using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    public void OnClickBackButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
