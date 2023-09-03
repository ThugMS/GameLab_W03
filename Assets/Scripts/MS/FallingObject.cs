using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : TimeInfluenced
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private Ease m_ease;
    #endregion

    #region PublicMethod
    public override void UpdateTimeState()
    {
        if (GetObjectTIme() <= 0)
            EnableYoung();
        else if (GetObjectTIme() == 1)
            EnableYouth();
        else if (GetObjectTIme() >= 2)
            EnableElder();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            EnableYoung();
        }
    }

    #endregion

    #region PrivateMethod
    private void EnableYoung()
    {
        transform.DOMoveY(5f, TimeManager.s_Instance.m_skipTimeLength).SetEase(m_ease);
        CameraShakeTrigger.instance.ShakeCamera(2, TimeManager.s_Instance.m_skipTimeLength);
    }

    private void EnableYouth()
    {

    }

    private void EnableElder()
    {

    }
    #endregion
}
