using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Player
{
    #region PublicVariables
    #endregion

    #region PrivateVariables

    #endregion

    #region PublicMethod
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        PlayerManager.instance.SetIsCeiling(m_isCeiling);
        PlayerManager.instance.SetIsGround(m_isGround);
    }
    #endregion

    #region PrivateMethod
    #endregion
}
