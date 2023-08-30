using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_TYPE 
{
    Baby, Youth, Senior
}


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region PublicVariables
    public GameObject m_player;
    public PLAYER_TYPE m_playerType;

    public ITEM m_item = ITEM.None;
    #endregion

    #region PrivateVariables
    [Header("Baby")]
    [SerializeField] private GameObject m_babyObj;
    [SerializeField] private Baby m_babyScript;

    [Header("Youth")]
    [SerializeField] private GameObject m_youthObj;
    [SerializeField] private Youth m_youthScript;

    [Header("Senior")]
    [SerializeField] private GameObject m_seniorObj;
    [SerializeField] private Senior m_seniorScript;
    #endregion

    #region PublicMethod
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        switch (TimeManager.s_Instance.GetCureentTime()) 
        {
            case 0:
                m_playerType = PLAYER_TYPE.Baby;
                break;

            case 1:
                m_playerType = PLAYER_TYPE.Youth;
                break;

            case 2:
                m_playerType = PLAYER_TYPE.Senior;
                break;

            default:
                break;

        }

        ChangeType(TimeManager.s_Instance.GetCureentTime());
    }

    public void ChangeType(int _time)
    {
        switch(_time)
        {
            case 0:
                OnSetting(m_babyObj, m_babyScript);
                OffSetting(m_youthObj, m_youthScript);
                OffSetting(m_seniorObj, m_seniorScript);
                break;

            case 1:
                OffSetting(m_babyObj, m_babyScript);
                OffSetting(m_seniorObj, m_seniorScript);
                OnSetting(m_youthObj, m_youthScript);
                break;

            case 2:
                OffSetting(m_babyObj, m_babyScript);
                OffSetting(m_youthObj, m_youthScript, true);
                OnSetting(m_seniorObj, m_seniorScript);
                break;

            default:
                break;
        }
        m_item = ITEM.None;
    }
    #endregion

    #region PrivateMethod
    private void OnSetting<T>(GameObject _obj, T _script) where T : Player
    {   
        if(m_item == ITEM.Key)
        {
            _script.m_grabItem = ITEM.Key;
        }

        _obj.SetActive(true);
        _script.enabled = true;
    }

    private void OffSetting<T>(GameObject _obj, T _script) where T : Player
    {
        
        if (_script.m_grabItem == ITEM.Key)
        {
            m_item = ITEM.Key;
        }
        _obj.SetActive(false);
        _script.enabled = false;
    }


    private void OffSetting<T>(GameObject _obj, T _script, bool _isUp) where T : Player
    {

        if (_script.m_grabItem == ITEM.Key)
        {
            m_item = ITEM.Key;
        }
        _script.m_isUp = _isUp;
        _obj.SetActive(false);
        _script.enabled = false;

    }
    #endregion
}
