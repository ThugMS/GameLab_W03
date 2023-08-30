using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level1Door : MonoBehaviour, ISwitchConnectedObjects
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    #endregion
    #region PublicMethod
    public void InteractStart()
    {
        transform.DOLocalMoveY(6.5f, TimeManager.s_Instance.m_skipTimeLength);
    }
    #endregion
    #region PrivateMethod
    #endregion
}
