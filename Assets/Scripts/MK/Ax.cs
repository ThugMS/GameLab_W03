using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour, ISwitchConnectedObjects
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [SerializeField] private bool isSwitchConnected= false;
    #endregion
    #region PublicMethod
    #endregion
    #region PrivateMethod
    #endregion
    public void InteractStart()
    {
        if (!isSwitchConnected)
        {
            isSwitchConnected = true;
            gameObject.SetActive(true);
        }
    }

}