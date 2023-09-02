using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeChangeByTime : TimeInfluenced, IBurn
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

    public void Burn()
    {
        // 임시 연소
        transform.parent.gameObject.SetActive(false);
    }

    #endregion
    #region PrivateMethod
    private void EnableYoung()
    {
        m_headTransform.DOScale(new Vector3(0f, 0f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScaleY(0.1f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMove(new Vector3(0f, -5f, 0f), TimeManager.s_Instance.m_skipTimeLength);
    }
    private void EnableYouth()
    {
        m_headTransform.DOLocalMoveY(3.5f, TimeManager.s_Instance.m_skipTimeLength);
        m_headTransform.DOScale(new Vector3(1f, 1f, 1f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScaleY(1.2f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMove(new Vector3(0f, 0f, 0f), TimeManager.s_Instance.m_skipTimeLength);
    }
    private void EnableElder()
    {
        m_headTransform.DOLocalMoveY(7f, TimeManager.s_Instance.m_skipTimeLength);
        m_headTransform.DOScale(new Vector3(1.5f, 2f, 1.5f), TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMove(new Vector3(0f, 4.5f, 0f), TimeManager.s_Instance.m_skipTimeLength);
    }

    private void Start()
    {
        SetStartTime(m_startTime);
        UpdateTimeState();
    }
    #endregion
}
