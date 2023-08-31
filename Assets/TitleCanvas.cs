using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    #endregion
    #region PublicMethod
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void gameStart(int index)
    {
        SceneManager.LoadScene(1);
    }

    public void gameQuit()
    {
        Application.Quit();
    }
#endregion
#region PrivateMethod
#endregion
}