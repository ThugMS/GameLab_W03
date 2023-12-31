using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level1Door : MonoBehaviour, ISwitchConnectedObjects
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    private bool isInteracted = false;
    #endregion
    #region PublicMethod
    public void InteractStart()
    {
        if (isInteracted)
            return;
        isInteracted = true;
        transform.DOScaleY(transform.localScale.y - 0.3f, TimeManager.s_Instance.m_skipTimeLength);
        transform.DOLocalMoveY(transform.localPosition.y + 1.5f, TimeManager.s_Instance.m_skipTimeLength);
        CameraShakeTrigger.instance.ShakeCamera(2,1f);
    }
    #endregion
    #region PrivateMethod
    #endregion
}
