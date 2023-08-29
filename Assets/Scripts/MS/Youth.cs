using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Youth : Player
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [Header("Grab")]
    [SerializeField] private bool m_isGrab = false;
    [SerializeField] private bool m_isHand = false;
    [SerializeField] Collider[] m_collider;
    [SerializeField] private Vector3 m_boxPosition = new Vector3 (0, 0, 1f);
    [SerializeField] private Vector3 m_boxSize = new Vector3(1.5f, 2, 1.5f);

    #endregion

    #region PublicMethod
    public void Interact(InputAction.CallbackContext _context)
    {
        CheckGrab();
    }
    #endregion

    #region PrivateMethod
    public void CheckGrab()
    {   //,  LayerMask.NameToLayer("Axe")
        m_collider = Physics.OverlapBox(m_boxPosition, m_boxSize * 0.5f, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(m_boxPosition, m_boxSize);
    }
    #endregion
}
