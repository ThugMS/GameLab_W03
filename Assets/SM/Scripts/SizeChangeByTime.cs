using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeByTime : TimeInfluenced
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [SerializeField] private GameObject m_young;
    [SerializeField] private GameObject m_youth;
    [SerializeField] private GameObject m_elder;

    [SerializeField] private int m_startTime;
    #endregion
    #region PublicMethod
    public override void UpdateTimeState()
    {
        DisableModels();
        if (m_objectTime <= 0)
            EnableYoung();
        else if (m_objectTime == 1)
            EnableYouth();
        else if (m_objectTime >= 2)
            EnableElder();
    }

    public void EnableYoung() => m_young.SetActive(true);
    public void EnableYouth() => m_youth.SetActive(true);
    public void EnableElder() => m_elder.SetActive(true);

    #endregion
    #region PrivateMethod
    private void DisableModels()
    {
        m_young.SetActive(false);
        m_youth.SetActive(false);
        m_elder.SetActive(false);
    }

    private void OnEnable()
    {
        m_objectTime = m_startTime;
    }
    #endregion
}
