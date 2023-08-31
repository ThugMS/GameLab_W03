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
    public void gameStart(int index)
    {
        SceneManager.LoadScene(1);
    }
#endregion
#region PrivateMethod
#endregion
}