using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeByTime : TimeInfluenced
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [SerializeField] private GameObject m_sprout;
    [SerializeField] private GameObject m_tree;
    [SerializeField] private GameObject m_elderTree;

    [SerializeField] private int m_startTime;
    #endregion
    #region PublicMethod
    public override void UpdateTimeState()
    {
        DisableModels();
        if (m_objectTime <= 0)
            EnableSprout();
        else if (m_objectTime == 1)
            EnableTree();
        else if (m_objectTime >= 2)
            EnableElderTree();
    }

    public void EnableSprout() => m_sprout.SetActive(true);
    public void EnableTree() => m_tree.SetActive(true);
    public void EnableElderTree() => m_elderTree.SetActive(true);
    // 베는 애니메이션 넣기
    public void Chop() => DisableModels();

    #endregion
    #region PrivateMethod
    private void DisableModels()
    {
        m_sprout.SetActive(false);
        m_tree.SetActive(false);
        m_elderTree.SetActive(false);
    }

    private void OnEnable()
    {
        m_objectTime = m_startTime;
    }
    #endregion
}
