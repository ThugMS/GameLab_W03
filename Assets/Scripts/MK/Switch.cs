using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [SerializeField]private bool m_isSwitchOn = false;
    #endregion
    #region PublicMethod

    public void TurnSwitch(bool isSwitchOn)
    {
        m_isSwitchOn = isSwitchOn; 
    }
    #endregion
    #region PrivateMethod


    #endregion
}