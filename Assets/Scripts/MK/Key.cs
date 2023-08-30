using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, ISwitchConnectedObjects
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [SerializeField] private bool m_isSwitchConnected= false;
    #endregion
    #region PublicMethod
    public void InteractStart()
    {
        if (!m_isSwitchConnected)
        {
            m_isSwitchConnected = true;
        }
    }
    #endregion
    #region PrivateMethod
    #endregion


}