using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceChangeByTime : TimeInfluenced
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [Header("Ice Transform")]
    [SerializeField] private Transform m_bodyTransform;
    [SerializeField] private Transform m_collisionTransform;

    [Header("Time")]
    [SerializeField] private int m_startTime;

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

    #endregion
    #region PrivateMethod
    private void EnableYoung()
    {

        m_bodyTransform.DOLocalMove(new Vector3(0f, 0.5f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScaleY(10f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMoveY(0f, TimeManager.s_Instance.m_skipTimeLength);
    }
    private void EnableYouth()
    {
        m_bodyTransform.DOLocalMove(new Vector3(0f,  -2f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScale(new Vector3(2f, 5f, 2f), TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMoveY(-5f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOScaleY(10f, TimeManager.s_Instance.m_skipTimeLength);
    }
    private void EnableElder()
    {

        m_bodyTransform.DOLocalMove(new Vector3(0f, -4.7f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScaleY(1f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMoveY(-7f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOScaleY(5f, TimeManager.s_Instance.m_skipTimeLength);
    }

    private void Start()
    {
        SetStartTime(m_startTime);
        UpdateTimeState();
    }
#endregion
}