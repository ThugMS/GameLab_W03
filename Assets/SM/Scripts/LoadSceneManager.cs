using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    #region PublicVariables
    public static LoadSceneManager s_Instance
    {
        get
        {
            if (s_instance == null)
            {
                return null;
            }
            return s_instance;
        }
    }
    #endregion
    #region PrivateVariables
    private static LoadSceneManager s_instance;
    #endregion
    #region PublicMethod
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
    #region PrivateMethod
    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
