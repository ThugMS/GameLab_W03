using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region PublicVariables
    public static TimeManager s_Instance
    {
        get 
        {
            if (s_instance == null)
            {
                return null;
            }
            return s_instance;
        }
    }

    public float m_skipTimeLength { private set; get; }
    public int m_timeCount;
    #endregion
    #region PrivateVariables

    private static TimeManager s_instance;

    [SerializeField]
    private List<TimeInfluenced> m_levelObjects = new List<TimeInfluenced>();
    #endregion
    #region PublicMethod

    public void PlusTime()
    {
        foreach(TimeInfluenced obj in m_levelObjects)
        {
            if (obj.enabled == true)
                obj.PlusTime();
        }
        m_timeCount++;

        PlayerManager.instance.ChangeType(m_timeCount);
        PlayerManager.instance.StopMove(m_skipTimeLength);
        CameraShakeTrigger.instance.ShakeCamera(2, m_skipTimeLength);
    }
    public void MinusTIme()
    {
        foreach (TimeInfluenced obj in m_levelObjects)
        {
            if (obj.enabled == true)
                obj.MinusTime();
        }
        m_timeCount--;

        PlayerManager.instance.ChangeType(m_timeCount);
        PlayerManager.instance.StopMove(m_skipTimeLength);
        CameraShakeTrigger.instance.ShakeCamera(2, m_skipTimeLength);
    }

    public void AddObject(TimeInfluenced _obj)
    {
        m_levelObjects.Add(_obj);
    }
    public void RemoveObject(TimeInfluenced _obj)
    {
        m_levelObjects.Remove(_obj);
    }

    public int GetCureentTime()
    {
        return m_timeCount;
    }
    #endregion
    #region PrivateMethod
    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        m_skipTimeLength = 1f;
    }
    #endregion
}
