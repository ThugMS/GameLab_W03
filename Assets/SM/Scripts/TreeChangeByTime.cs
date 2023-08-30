using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeChangeByTime : TimeInfluenced
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [Header("Tree Transform")]
    [SerializeField] private Transform m_headTransform;
    [SerializeField] private Transform m_bodyTransform;
    [SerializeField] private Transform m_collisionTransform;

    [Header("Time")]
    [SerializeField] private int m_startTime;
    [SerializeField] private float m_moveTime = 0.5f;

    [Header("IsChopped")]
    [SerializeField] private bool isChopped = false;
    #endregion
    #region PublicMethod
    public override void UpdateTimeState()
    {
        if (isChopped)
            return;
        if (GetObjectTIme() <= 0)
            EnableYoung();
        else if (GetObjectTIme() == 1)
            EnableYouth();
        else if (GetObjectTIme() >= 2)
            EnableElder();
    }
    public bool Chop()
    {
        if (GetObjectTIme() != 1 && !isChopped)
            return false;
        EnableYoung();
        isChopped = true;
        return true;
    }

    #endregion
    #region PrivateMethod
    private void EnableYoung()
    {
        m_headTransform.DOScale(new Vector3(0f, 0f, 0f), m_moveTime);
        m_bodyTransform.DOScaleY(0.1f, m_moveTime);
        m_collisionTransform.DOLocalMove(new Vector3(0f, -5f, 0f), m_moveTime);
    }
    private void EnableYouth()
    {
        m_headTransform.DOLocalMoveY(3.5f, m_moveTime);
        m_headTransform.DOScale(new Vector3(1f, 1f, 1f), m_moveTime);
        m_bodyTransform.DOScaleY(1f, m_moveTime);
        m_collisionTransform.DOLocalMove(new Vector3(0f, 0f, 0f), m_moveTime);
    }
    private void EnableElder()
    {
        m_headTransform.DOLocalMoveY(7f, m_moveTime);
        m_headTransform.DOScale(new Vector3(1.5f, 2f, 1.5f), m_moveTime);
        m_collisionTransform.DOLocalMove(new Vector3(0f, 4.5f, 0f), m_moveTime);
    }

    private void OnEnable()
    {
        SetStartTime(m_startTime);
    }
    #endregion
}
