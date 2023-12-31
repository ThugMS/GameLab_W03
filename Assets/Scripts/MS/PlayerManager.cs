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

    public bool m_isGround = false;
    public bool m_isCeiling = false;


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
        Screen.SetResolution(1920, 1080, true);
    }
    private void Start()
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
                OffSetting(m_youthObj, m_youthScript);
                OffSetting(m_seniorObj, m_seniorScript);
                OnSetting(m_babyObj, m_babyScript);
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

    public void StopMove(float _time)
    {
        m_babyScript.m_stopMove = true;
        m_youthScript.m_stopMove = true;
        m_seniorScript.m_stopMove = true;

        StartCoroutine(nameof(IE_StopMove), _time);
    }

    public void SetOnPanel()
    {
        m_babyScript.m_onPanel = true;
        m_youthScript.m_onPanel = true;
        m_seniorScript.m_onPanel = true;
    }

    public void SetOffPanel()
    {
        m_babyScript.m_onPanel = false;
        m_youthScript.m_onPanel = false;
        m_seniorScript.m_onPanel = false;
    }

    public void SetIsCeiling(bool _value)
    {
        m_isCeiling = _value;
    }

    public void SetIsGround(bool _value)
    {
        m_isGround = _value;
    }
    #endregion

    #region PrivateMethod
    private void SetGravity(float _num)
    {
        Physics.gravity = new Vector3(Physics.gravity.x, 0f, Physics.gravity.z);
    }

    private void OnSetting<T>(GameObject _obj, T _script) where T : Player
    {   
        if(m_item == ITEM.Key)
        {   
            if(TimeManager.s_Instance.GetCureentTime() == 0)
            {
                _script.m_isUp = true;
            }
            else
            {
                _script.m_grabItem = ITEM.Key;
            }
        }

        _obj.SetActive(true);
        _script.enabled = true;
        _script.m_isGround = m_isGround;
        _script.m_isCeiling = m_isCeiling;
    }

    private void OffSetting<T>(GameObject _obj, T _script) where T : Player
    {
        _script.OnChange();
        _obj.SetActive(false);
        _script.enabled = false;
    }


    private void OffSetting<T>(GameObject _obj, T _script, bool _isUp) where T : Player
    {
        _script.m_isUp = _isUp;
        _script.OnChange();
        _obj.SetActive(false);
        _script.enabled = false;
    }

    private IEnumerator IE_StopMove(float _time)
    { 

        yield return new WaitForSeconds(_time);

        m_babyScript.SetStopMoveFalse();
        m_youthScript.SetStopMoveFalse();
        m_seniorScript.SetStopMoveFalse();
    }
    #endregion
}
