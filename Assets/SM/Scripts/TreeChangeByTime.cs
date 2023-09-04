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

    [Header("Tree Color")]
    [SerializeField] private Color[] m_treeColors;
    [SerializeField] private Color m_nowColor;
    [SerializeField] private MaterialPropertyBlock m_mpb;
    [SerializeField] private Renderer m_renderer;

    [Header("Time")]
    [SerializeField] private int m_startTime;
    private int m_objTime;

    [Header("IsChopped")]
    [SerializeField] private bool isChopped = false;
    #endregion
    #region PublicMethod
    public override void UpdateTimeState()
    {
        m_objTime = GetObjectTIme();

        if (isChopped)
            return;
        if (m_objTime <= 0)
            EnableYoung();
        else if (m_objTime == 1)
            EnableYouth();
        else if (m_objTime >= 2)
        {
            EnableElder();
            StartCoroutine(ChangeColorOverTime(m_treeColors[m_objTime - 2], 1f));
        }
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
        isChopped = true;
        transform.parent.gameObject.SetActive(false);
    }

    #endregion
    #region PrivateMethod
    private void EnableYoung()
    {
        m_headTransform.DOScale(new Vector3(0f, 0f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScaleY(0.1f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMove(new Vector3(0f, -0.1f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOScaleY(0.2f, TimeManager.s_Instance.m_skipTimeLength);
    }
    private void EnableYouth()
    {
        m_headTransform.DOLocalMoveY(3.5f, TimeManager.s_Instance.m_skipTimeLength);
        m_headTransform.DOScale(new Vector3(1f, 1f, 1f), TimeManager.s_Instance.m_skipTimeLength);
        m_bodyTransform.DOScaleY(1.2f, TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMove(new Vector3(0f, 2.5f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOScaleY(5f, TimeManager.s_Instance.m_skipTimeLength);
    }
    private void EnableElder()
    {
        m_headTransform.DOLocalMoveY(7f, TimeManager.s_Instance.m_skipTimeLength);
        m_headTransform.DOScale(new Vector3(1.5f, 2f, 1.5f), TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOLocalMove(new Vector3(0f, 4.5f, 0f), TimeManager.s_Instance.m_skipTimeLength);
        m_collisionTransform.DOScaleY(10f, TimeManager.s_Instance.m_skipTimeLength);
    }

    public override void Start()
    {
        base.Start();
        m_renderer = m_headTransform.GetComponent<Renderer>();
        m_nowColor = m_treeColors[0];
        m_mpb = new MaterialPropertyBlock();
        SetStartTime(m_startTime);
        UpdateTimeState();
    }

    private IEnumerator ChangeColorOverTime(Color _endColor, float _time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _time)
        {
            Color lerpedColor = Color.Lerp(m_nowColor, _endColor, elapsedTime / _time);
            m_mpb.SetColor(Shader.PropertyToID("_Color"), lerpedColor);
            m_renderer.SetPropertyBlock(m_mpb);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        m_nowColor = _endColor;
        m_mpb.SetColor(Shader.PropertyToID("_Color"), _endColor);
        m_renderer.SetPropertyBlock(m_mpb);
    }
    #endregion
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
