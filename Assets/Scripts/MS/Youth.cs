using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ITEM
{
    None, Axe, Key
}

public class Youth : Player
{
    #region PublicVariables
    public ITEM m_grabItem = ITEM.None;
    #endregion

    #region PrivateVariables
    [Header("Jump")]
    [SerializeField] private bool m_isGround = false;
    [SerializeField] private float m_jumpPower = 1f;
    [SerializeField] private float m_gravityValue = 10f;

    [Header("Grab")]
    [SerializeField] private bool m_isGrab = false;
    [SerializeField] private bool m_isHand = false;
    [SerializeField] private Collider[] m_collider;
    [SerializeField] private Vector3 m_boxPosition = new Vector3 (0, 0, 1f);
    [SerializeField] private Vector3 m_boxSize = new Vector3(1.5f, 2, 1.5f);


    [Header("Axe")]
    [SerializeField] private GameObject m_axe;
    [SerializeField] private GameObject m_axePrefab;


    private Tree m_targetTree;

    [Header("Key")]
    [SerializeField] private GameObject m_key;
    [SerializeField] private GameObject m_keyPrefab;

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
        if(GetComponent<Youth>().isActiveAndEnabled == true)
            Jump(); 
    }

    public void Interact(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            if (m_grabItem == ITEM.None)
            {
                CheckInteract();
            }
            else if (m_grabItem == ITEM.Axe)
            {
                AxeAction();
            }
            else if (m_grabItem == ITEM.Key)
            {
                KeyAction();
            }
        }
    }
        #endregion

    #region PrivateMethod
    public void CheckInteract()
    {
        m_collider = CheckCollider();

        if(m_collider != null)
        {   
            for(int i=0;i< m_collider.Length;i++)
            {
                if (m_collider[i].tag == "Switch")
                {
                    m_collider[i].GetComponent<Switch>().TurnSwitch();
                }

                if (m_collider[i].gameObject.layer == LayerMask.NameToLayer("Axe"))
                {
                    Destroy(m_collider[i].gameObject);
                    m_axe.SetActive(true);
                    m_grabItem = ITEM.Axe;
                }

                if (m_collider[i].gameObject.layer == LayerMask.NameToLayer("Key"))
                {
                    Destroy(m_collider[i].gameObject);
                    m_key.SetActive(true);
                    m_grabItem = ITEM.Key;
                }
            }
        }
    }

    private Collider[] CheckCollider()
    {
        Collider[] collider;

        Vector3 pos = transform.position + transform.forward;
        collider = Physics.OverlapBox(pos, m_boxSize * 0.5f, Quaternion.Euler(transform.forward));

        return collider;
    }

    private Collider[] CheckCollider(string _layer)
    {
        Collider[] collider = null;

        Vector3 pos = transform.position + transform.forward;
        collider = Physics.OverlapBox(pos, m_boxSize * 0.5f, transform.rotation, 1 << LayerMask.NameToLayer(_layer));

        return collider;
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
        //1 << LayerMask.NameToLayer("Ground")
        if (Physics.Raycast(ray, 1.1f))
        {
            m_isGround = true;
            m_isJump = false;
        }
        else
        {
            m_isGround = false;
            m_isJump = true;
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

    private void AxeAction()
    {
        m_collider = null;
        m_collider = CheckCollider("Tree");

        if (m_collider.Length != 0)
        {
            m_collider[0].TryGetComponent(out m_targetTree);
            m_targetTree.Chop();
        }
        else 
        {
            m_axe.SetActive(false);

            Instantiate(m_axePrefab, transform.position + new Vector3(0, 0, 3), Quaternion.identity);
            m_grabItem = ITEM.None;
        }

    }

    private void KeyAction()
    {
        m_collider = null;
        m_collider = CheckCollider("Door");

        if (m_collider.Length != 0)
        {
            Debug.Log("door open");
            m_key.SetActive(false);
        }
        else
        {
            m_key.SetActive(false);

            Instantiate(m_keyPrefab, transform.position + new Vector3(0, 0, 3), Quaternion.identity);
            m_grabItem = ITEM.None;
        }

    }
    #endregion
}
