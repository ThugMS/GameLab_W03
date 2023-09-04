
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceChangeByTime : TimeInfluenced, IBurn
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [Header("Ice Transform")]
    [SerializeField] private Transform m_bodyTransform;
    [SerializeField] private Transform m_collisionTransform;

    [Header("Ice Color")]
    [SerializeField] private Color[] m_iceColors;
    [SerializeField] private Color m_nowColor;
    [SerializeField] private MaterialPropertyBlock m_mpb;
    [SerializeField] private Renderer m_renderer;
    [SerializeField] private Material m_targetMaterial;

    [Header("Time")]
    [SerializeField] private int m_startTime;
    private int m_objTime;

    #endregion
    #region PublicMethod
    public override void UpdateTimeState()
    {
        m_objTime = GetObjectTIme();

        if (!transform.parent.gameObject.activeSelf)
            return;

        if (m_objTime <= 0)
        {
            EnableYoung();
            m_targetMaterial.DOColor(m_iceColors[m_objTime + 2], 1f);
        }
        else if (m_objTime == 1)
            EnableYouth();
        else if (m_objTime >= 2)
            EnableElder();
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

        m_bodyTransform.DOLocalMove(new Vector3(0f, 0.5f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScaleY(10f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMoveY(0f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOScaleY(10f, TimeManager.s_Instance.m_skipTimeLength);
    }
    private void EnableYouth()
    {
        m_bodyTransform.DOLocalMove(new Vector3(0f,  -2f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScale(new Vector3(2f, 5f, 2f), TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMoveY(-2.5f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOScaleY(5f, TimeManager.s_Instance.m_skipTimeLength);
    }
    private void EnableElder()
    {

        m_bodyTransform.DOLocalMove(new Vector3(0f, -4.7f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScaleY(1f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMoveY(-5f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOScaleY(0.2f, TimeManager.s_Instance.m_skipTimeLength);
    }

    public override void Start()
    {
        base.Start();
        m_renderer = m_bodyTransform.GetComponent<Renderer>();
        m_nowColor = m_iceColors[0];
        m_targetMaterial = m_renderer.materials[1];
        m_mpb = new MaterialPropertyBlock();
        SetStartTime(m_startTime);
        UpdateTimeState();
    }
    #endregion
}

