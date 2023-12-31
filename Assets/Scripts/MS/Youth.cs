using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum ITEM
{
    None, Axe, Key, Torch
}

public class Youth : Player
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [Header("Jump")]
    //[SerializeField] private bool m_isGround = false;
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
    private TreeChangeByTime m_targetTree;


    [Header("Key")]
    [SerializeField] public GameObject m_key;
    [SerializeField] private GameObject m_keyPrefab;
    [SerializeField] private Door m_targetDoor;

    [Header("Torch")]
    [SerializeField] private GameObject m_torch;
    [SerializeField] private GameObject m_torchPrefab;


    
    #endregion

    #region PublicMethod
    protected override void FixedUpdate()
    {   
        base.FixedUpdate();
        ApplyGravity();
    }

    private void OnEnable()
    {
        if (m_grabItem == ITEM.Key)
        {
            m_key.SetActive(true);
        }

        m_isUp = false;
    }

    public override void OnChange()
    {
        base.OnChange();
        m_isUp = false;
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.started == false)
            return;

        if (m_stopMove == true)
            return;
        

        if (GetComponent<Youth>().isActiveAndEnabled == false)
            return;

        Jump();
    }

    public void Interact(InputAction.CallbackContext _context)
    {
        if (m_stopMove == true)
        {
            return;
        }

        if (GetComponent<Youth>().isActiveAndEnabled == false)
            return;

        if (_context.started)
        {
            switch (m_grabItem)
            {
                case ITEM.None:
                    CheckInteract();
                    break;
                case ITEM.Axe:
                    AxeAction();
                    break;
                case ITEM.Key:
                    KeyAction();
                    break;
                case ITEM.Torch:
                    TorchAction();
                    break;
            }
        }
    }
    public override void ResetSetting()
    {
        //  시간이 올라간다
        switch (m_grabItem)
        {
            case ITEM.Axe:
                m_axe.SetActive(false);
                ReturnItem(m_axePrefab);
                break;
            case ITEM.Key:
                m_key.SetActive(false);
                if (m_isUp == true)
                    break;
                ReturnItem(m_keyPrefab);
                break;
            case ITEM.Torch:
                m_torch.SetActive(false);
                ReturnItem(m_torchPrefab);
                break;
        }
    }
        #endregion

        #region PrivateMethod
        public void CheckInteract()
        {
        if (m_isDead) return;

        m_collider = CheckCollider();

        if(m_collider != null)
        {   
             for(int i=0;i< m_collider.Length;i++)
             {
                if (m_collider[i].tag == "Switch")
                {
                    m_collider[i].GetComponent<Switch>().TurnSwitch();
                    
                }

                switch (m_collider[i].gameObject.layer)
                {
                    // Axe
                    case 6:
                        Destroy(m_collider[i].gameObject);
                        m_axe.SetActive(true);
                        m_grabItem = ITEM.Axe;
                        break;
                    // Key
                    case 8:
                        m_collider[i].gameObject.SetActive(false);
                        m_key.SetActive(true);
                        m_grabItem = ITEM.Key;
                        break;
                    // Torch
                    case 12:
                        m_collider[i].gameObject.SetActive(false);
                        m_torch.SetActive(true);
                        m_grabItem = ITEM.Torch;
                        break;
                }

                if (m_grabItem != ITEM.None)
                    break;
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
            if (m_targetTree != null)
            {
                if (m_targetTree.Chop())
                {
                    m_targetTree = null;
                    m_animator.SetTrigger("AxeTrigger");
                }
                else
                {
                   //Play Fail Animation
                }
            }
        }
        else 
        {
            m_axe.SetActive(false);
            ReturnItem(m_axePrefab);
        }

    }

    private void KeyAction()
    {
        m_collider = null;
        m_collider = CheckCollider("Door");

        if (m_collider.Length != 0)
        {
            m_animator.SetTrigger("KeyTrigger");
        }
        else
        {
            m_key.SetActive(false);
            ReturnItem(m_keyPrefab);
        }

    }

    public void OpenDoor()
    {
        m_collider[0].TryGetComponent(out m_targetDoor);
        m_key.SetActive(false);
        m_targetDoor.InteractStart();
        m_grabItem = ITEM.None;
    }

    private void TorchAction()
    {
        TorchLight torchLight;
        m_torch.TryGetComponent(out torchLight);
        if (!torchLight.SetFire())
        {
            ReturnItem(m_torchPrefab);
        }
        else
        {
            m_grabItem = ITEM.None;
        }
        m_torch.SetActive(false);
    }

    private void ReturnItem(GameObject _prefab)
    {
        if (m_isDead)
            return;
        Instantiate(_prefab, transform.position + transform.forward, Quaternion.identity);
        _prefab.SetActive(true);
        m_grabItem = ITEM.None;
    }

    /*
    private void ReturnKey()
    {
        if (m_isDead) return;

        Instantiate(m_keyPrefab, transform.position + transform.forward, Quaternion.identity);
        //m_grabItem = ITEM.None;
    }

    private void ReturnAxe()
    {
        if (m_isDead) return;

        Instantiate(m_axePrefab, transform.position + transform.forward, Quaternion.identity);
        //m_grabItem = ITEM.None;
    }
    private void ReturnTorch()
    {
        if (m_isDead) return;

        Instantiate(m_torchPrefab, transform.position + transform.forward, Quaternion.identity);
        //m_grabItem = ITEM.None;
    }
    */

        
        /* Refactoring
        if (m_grabItem == ITEM.Axe)
        {
            m_axe.SetActive(false);
            ReturnAxe();
        }

        if(m_isUp == true) 이게 뭐임? : 시간이 올라간다
        {
            m_key.SetActive(false);
            m_grabItem = ITEM.None;
        }
        else
        {
            if (m_grabItem == ITEM.Key)
            {
                m_key.SetActive(false);
                m_grabItem = ITEM.None;
                ReturnKey();
            }
        }
        */
    #endregion
}
