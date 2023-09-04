using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonConnectedDoor : MonoBehaviour, ISwitchConnectedObjects
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [SerializeField] private float offset = -5f;
    #endregion
    #region PublicMethod
    public void InteractStart()
    {
        transform.DOLocalMoveY(transform.localPosition.y + offset, TimeManager.s_Instance.m_skipTimeLength);
    }
    #endregion
    #region PrivateMethod
    #endregion
}
