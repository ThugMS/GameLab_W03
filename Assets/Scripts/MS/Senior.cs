using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Senior : Player
{
    #region PublicVariables
    [Header("Key")]
    public GameObject m_key;
    public GameObject m_keyPrefab;
    public Door m_targetDoor;
    #endregion

    #region PrivateVariables
    [Header("Grab")]
    [SerializeField] private bool m_isGrab = false;
    [SerializeField] private bool m_isHand = false;
    [SerializeField] private Collider[] m_collider;
    [SerializeField] private Vector3 m_boxPosition = new Vector3(0, 0, 1f);
    [SerializeField] private Vector3 m_boxSize = new Vector3(1.5f, 2, 1.5f);

    
    #endregion

    #region PublicMethod
    private void OnEnable()
    {
        m_speed = 3.0f;
        if (m_grabItem == ITEM.Key)
        {
            m_key.SetActive(true);
        }
    }

    private void OnDisable()
    {
        ResetSetting();
    }

    public void Interact(InputAction.CallbackContext _context)
    {
        if (GetComponent<Senior>().isActiveAndEnabled == false)
            return;
        Debug.Log("yes");
        if (_context.started)
        {
            if (m_grabItem == ITEM.None)
            {
                CheckInteract();
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

        if (m_collider != null)
        {
            for (int i = 0; i < m_collider.Length; i++)
            {
                if (m_collider[i].tag == "Switch")
                {
                    m_collider[i].GetComponent<Switch>().TurnSwitch();
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

    private void KeyAction()
    {
        m_collider = null;
        m_collider = CheckCollider("Door");

        if (m_collider.Length != 0)
        {
            Debug.Log("door open");
            m_collider[0].TryGetComponent(out m_targetDoor);
            m_targetDoor.InteractStart();
            m_key.SetActive(false);
        }
        else
        {
            m_key.SetActive(false);
            ReturnKey();
        }
    }

    private void ReturnKey()
    {
        Instantiate(m_keyPrefab, transform.position + new Vector3(0, 0, 3), Quaternion.identity);
        m_grabItem = ITEM.None;
    }

    private void ResetSetting()
    {
        if(m_grabItem == ITEM.Key)
        {
            m_key.SetActive(false);
            m_grabItem = ITEM.None;
        }
    }
    #endregion
}
