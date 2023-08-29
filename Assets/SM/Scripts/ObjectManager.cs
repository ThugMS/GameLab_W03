using System.Collections;
using System.Collections.Generic;
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
    public List<TimeInfluenced> levelObjects;
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
        TimeManager.s_Instance.GetObjects(levelObjects);
    }
    #endregion
}
