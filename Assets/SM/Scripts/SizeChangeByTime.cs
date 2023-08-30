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
    
    [Header("Cover Objects")]
    [SerializeField] private GameObject m_youngObj;
    [SerializeField] private GameObject m_youthObj;
    [SerializeField] private GameObject m_elderObj;

    [Header("Time")]
    [SerializeField] private int m_startTime;
    [SerializeField] private float m_timeScale = 2f;
    #endregion
    #region PublicMethod
    public override void UpdateTimeState()
    {
        StopAllCoroutines();
        ResetObjects();
        if (GetObjectTIme() <= 0)
            EnableYoung();
        else if (GetObjectTIme() == 1)
            EnableYouth();
        else if (GetObjectTIme() >= 2)
            EnableElder();
    }

    private void ResetObjects()
    {
        m_youngObj.SetActive(false);
        m_youthObj.SetActive(false);
        m_elderObj.SetActive(false);
    }

    public void EnableYoung()
    {
        m_youngObj.SetActive(true);
        StartCoroutine(nameof(IE_MoveCoroutine), m_youngYPosition);
    }
    public void EnableYouth()
    {
        m_youthObj.SetActive(true);
        StartCoroutine(nameof(IE_MoveCoroutine), m_youthYPosition);
    }
    public void EnableElder()
    {
        m_elderObj.SetActive(true);
        StartCoroutine(nameof(IE_MoveCoroutine), m_elderYPosition);
    }

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
        SetStartTime(m_startTime);
    }
    #endregion
}
