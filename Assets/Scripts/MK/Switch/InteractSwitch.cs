using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSwitch : MonoBehaviour
{
    #region PublicVariables
    #endregion
    #region PrivateVariables
    [SerializeField] private bool m_canClick = false;
    [SerializeField]private GameObject m_switch;
    #endregion
    #region PublicMethod
    #endregion
    #region PrivateMethod
    private void Update()
    {
        if ( m_canClick && Input.GetKeyDown(KeyCode.F))
        {
            m_switch.GetComponent<Switch>().TurnSwitch(true);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Switch"))
        {
            m_switch = collision.gameObject;
            m_canClick = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Switch"))
        {
            m_switch = null;
            m_canClick = false;
        }
    }
    #endregion
}