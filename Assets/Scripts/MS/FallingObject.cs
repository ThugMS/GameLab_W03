using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : TimeInfluenced
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private Transform m_start;
    [SerializeField] private Transform m_end;

    [Header("Transform")]
    [SerializeField] private Vector3 m_startPos;
    [SerializeField] private Vector3 m_endPos;
    [SerializeField] private Ease m_youngEase;
    [SerializeField] private ShakeRandomnessMode m_youthEase;
    [SerializeField] private AnimationCurve m_elderEase;
    [SerializeField] private float m_moveDis = 5f;

    [Header("Time")]
    [SerializeField] private int m_startTime = 1;
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
    }

    public override void Start()
    {   
        base.Start();
        m_startPos = m_start.position;
        m_endPos = m_end.position;

        SetStartTime(m_startTime);
        UpdateTimeState();
    }

    #endregion

    #region PrivateMethod
    private void EnableYoung()
    {
        transform.DOMove(m_startPos, TimeManager.s_Instance.m_skipTimeLength).SetEase(m_youngEase);
        //CameraShakeTrigger.instance.ShakeCamera(2, TimeManager.s_Instance.m_skipTimeLength);
    }

    private void EnableYouth()
    {
        
    }

    private void EnableElder()
    {
        transform.DOMove(m_endPos, TimeManager.s_Instance.m_skipTimeLength).SetEase(m_elderEase);
    }
    #endregion
}
