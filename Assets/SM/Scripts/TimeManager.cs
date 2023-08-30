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

    private List<TimeInfluenced> m_levelObjects;
    #endregion
    #region PublicMethod

    public void PlusTime()
    {
        foreach(TimeInfluenced obj in m_levelObjects)
        {
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
            obj.MinusTime();
        }
        m_timeCount--;

        PlayerManager.instance.ChangeType(m_timeCount);
        PlayerManager.instance.StopMove(m_skipTimeLength);
        CameraShakeTrigger.instance.ShakeCamera(2, m_skipTimeLength);
    }

    public void GetObjects(List<TimeInfluenced> _objects)
    {
        m_levelObjects = _objects;
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
