using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_TYPE 
{
    Baby, Youth, Senior
}


public class PlayerManager : TimeInfluenced
{
    #region PublicVariables
    public GameObject m_player;
    public PLAYER_TYPE m_playerType;
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
    private void Update()
    {
    }

    public override void UpdateTimeState()
    {
        ChangeType(m_objectTime);
    }

    public int GetTimePlayer()
    {
        return (int)m_playerType;
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
                OnSetting(m_youthObj, m_youthScript);
                OffSetting(m_babyObj, m_babyScript);
                OffSetting(m_seniorObj, m_seniorScript);
                break;

            case 2:
                OnSetting(m_seniorObj, m_seniorScript);
                OffSetting(m_babyObj, m_babyScript);
                OffSetting(m_youthObj, m_youthScript);
                break;

            default:
                break;
        }
    }
    #endregion

    #region PrivateMethod
    private void OnSetting<T>(GameObject _obj, T _script) where T : Player
    {
        _obj.SetActive(true);
        _script.enabled = true;
    }

    private void OffSetting(GameObject _obj, Player _script)
    {
        _obj.SetActive(false);
        _script.enabled = false;
    }

    private void Start()
    {
        m_objectTime = TimeManager.s_Instance.m_timeCount;
    }
    #endregion
}
