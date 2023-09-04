using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    #region PublicVariables
    public static ObjectManager s_Instance
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
    #endregion
    #region PrivateVariables
    private static ObjectManager s_instance;
    #endregion
    #region PublicMethod
    public List<TimeInfluenced> m_LevelObjects;

    public void AddObject(TimeInfluenced _obj)
    {  
        m_LevelObjects.Add(_obj); 
        TimeManager.s_Instance.AddObject(_obj);
    }

    public void RemoveObject(TimeInfluenced _obj)
    {
        m_LevelObjects.Remove(_obj);
        TimeManager.s_Instance.RemoveObject(_obj);
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
    }

    private void Start()
    {
    }
    #endregion
}
