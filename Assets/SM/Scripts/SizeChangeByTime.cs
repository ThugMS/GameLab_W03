using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeByTime : TimeInfluenced
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [Header("Tree Transform")]
    [SerializeField] private Transform m_treeTransform;

    [Header("Y Position by time")]
    [SerializeField] private float m_youngYPosition = -1f;
    [SerializeField] private float m_youthYPosition = 0f;
    [SerializeField] private float m_elderYPosition = 1f;

    [Header("Time")]
    [SerializeField] private int m_startTime;
    [SerializeField] private float m_timeScale = 2f;
    #endregion
    #region PublicMethod
    public override void UpdateTimeState()
    {
        StopAllCoroutines();
        if (m_objectTime <= 0)
            EnableYoung();
        else if (m_objectTime == 1)
            EnableYouth();
        else if (m_objectTime >= 2)
            EnableElder();
    }

    public void EnableYoung() => StartCoroutine("IE_MoveCoroutine", m_youngYPosition);
    public void EnableYouth() => StartCoroutine("IE_MoveCoroutine", m_youthYPosition);
    public void EnableElder() => StartCoroutine("IE_MoveCoroutine", m_elderYPosition);

    #endregion
    #region PrivateMethod
    // 코루틴 추가
    IEnumerator IE_MoveCoroutine(float _yPosition)
    {
        while (Time.fixedDeltaTime * m_timeScale <= 1f)
        {
            m_treeTransform.localPosition = Vector3.Lerp(m_treeTransform.localPosition, new Vector3(m_treeTransform.localPosition.x, _yPosition, m_treeTransform.localPosition.z), Time.fixedDeltaTime * m_timeScale);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnEnable()
    {
        m_objectTime = m_startTime;
    }
    #endregion
}
