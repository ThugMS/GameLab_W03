using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Youth : Player
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [Header("Jump")]
    [SerializeField] private bool m_isJump = false;
    [SerializeField] private bool m_isGround = false;
    [SerializeField] private float m_jumpPower = 1f;
    [SerializeField] private float m_gravityValue = 10f;

    [Header("Grab")]
    [SerializeField] private bool m_isGrab = false;
    [SerializeField] private bool m_isHand = false;
    [SerializeField] Collider[] m_collider;
    [SerializeField] private Vector3 m_boxPosition = new Vector3 (0, 0, 1f);
    [SerializeField] private Vector3 m_boxSize = new Vector3(1.5f, 2, 1.5f);


    #endregion

    #region PublicMethod
    protected override void FixedUpdate()
    {   
        base.FixedUpdate();
        CheckGround();
        ApplyGravity();
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        Jump();
    }

    public void Interact(InputAction.CallbackContext _context)
    {
        if (m_isGrab == false)
        {
            //CheckInteract();
        }
    }
    #endregion

    #region PrivateMethod
    public void CheckInteract()
    {   
        m_collider = Physics.OverlapBox(transform.position + m_boxPosition, m_boxSize * 0.5f, transform.rotation);
        
        if(m_collider != null)
        {
            if (m_collider[0].tag == "Ax")
            {
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(m_boxPosition, m_boxSize);
    }

    private void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(ray, 1.1f, 1 << LayerMask.NameToLayer("Ground")))
        {
            m_isGround = true;
        }
        else
        {
            m_isGround = false;
        }
    }

    private void Jump()
    {
        if(m_isGround == true)
        {
            m_rigidbody.AddForce(transform.up * m_jumpPower, ForceMode.Impulse);
        }
    }

    private void ApplyGravity()
    {
        if(m_isGround == false)
        {
            m_rigidbody.velocity += Vector3.down * m_gravityValue * Time.deltaTime;
        }
    }
    #endregion
}
