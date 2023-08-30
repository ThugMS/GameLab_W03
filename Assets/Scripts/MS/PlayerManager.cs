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
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public int GetTimePlayer()
    {
        return (int)m_playerType;
    }

    
    #endregion

    #region PrivateMethod
   
    #endregion
}
