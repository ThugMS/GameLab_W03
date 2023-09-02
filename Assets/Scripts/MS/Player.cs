using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Player : MonoBehaviour, IBurn
{
    #region PublicVariables
    public int m_time = 1;

    public ITEM m_grabItem = ITEM.None;
    public bool m_isUp = false;
    public bool m_stopMove = false;
    public bool m_isDead = false;
    public bool m_onPanel = false;
    #endregion

    #region PrivateVariables
    [SerializeField] public bool m_isGround = false;
    [SerializeField] public bool m_isCeiling = false;
    [SerializeField] protected bool m_isStucking = false;

    [Header("Camera Rotate")]
    [SerializeField] protected GameObject m_followTransform;
    [SerializeField] protected Vector2 m_look = Vector2.zero;
    [SerializeField] protected float m_rotationPower = 3f;
    [SerializeField] protected bool m_isGamepad = false;
    [SerializeField] protected bool m_isMouse = false;
    [SerializeField] protected float m_mouseSensitivity = 0.1f;
    [SerializeField] protected float m_padSensitivity = 3f;

    [Header("Move")]
    [SerializeField] protected Vector3 m_Direction = Vector3.zero;
    [SerializeField] protected Quaternion m_nextRotation = Quaternion.identity;
    [SerializeField] protected float m_rotationLerp = 0.8f;
    [SerializeField] protected Rigidbody m_rigidbody;
    [SerializeField] protected float m_maxSpeed = 5f;
     protected float m_addSpeed = 0.5f;
    [SerializeField] protected float m_curSpeed = 0f;
    [SerializeField] protected bool m_isMove = false;
    [SerializeField] protected bool m_isJump = false;

    [SerializeField] private Vector2 m_lastDir;

    [Header("Falling")]
    [SerializeField] private bool m_isFalling = false;
    [SerializeField] private float m_fallStartTime;
    [SerializeField] private float m_fallStartYPosition;
    [SerializeField] private float m_fallDeathHeight = 8f;
    #endregion

    #region PublicMethod


    public void Burn()
    {
        Death();
    }
    public void OnMovement(InputAction.CallbackContext _context)
    {

        if (m_stopMove == true || m_onPanel == true)
        {
            m_lastDir = m_Direction;
            m_Direction = Vector3.zero;
            return;
        }

        Vector2 input = _context.ReadValue<Vector2>();

        m_lastDir = input;

        if (m_isJump == true && input == Vector2.zero)
            return;
        
        m_Direction = new Vector3(input.x, 0f, input.y);

        if (Vector2.zero != input)
            m_isMove = true;

    }

    public void OnLook(InputAction.CallbackContext _context)
    {
        CheckInputType(_context.control.device.name);

        if (m_onPanel == true)
        {   
            return;
        }
            

        if (m_isGamepad == true)
        {
            m_rotationPower = m_padSensitivity;
        }

        if (m_isMouse == true)
        {
            m_rotationPower = m_mouseSensitivity;
        }

        m_look = _context.ReadValue<Vector2>();
    }

    protected virtual void Update()
    {
        if (m_lastDir == Vector2.zero)
        {
            m_isMove = false;
            m_curSpeed = 0f;
        }
    }

    protected virtual void FixedUpdate()
    {
        FallingCheck();
        CheckGround();
        CheckHead();
        CeilingCheck();

        if(m_stopMove == true)
        {
            m_Direction = new Vector3(0f, m_Direction.y, 0f);
        }

        #region Camera
        if(m_onPanel != true)
        {
            m_followTransform.transform.rotation *= Quaternion.AngleAxis(m_look.x * m_rotationPower, Vector3.up);
            m_followTransform.transform.rotation *= Quaternion.AngleAxis(-m_look.y * m_rotationPower, Vector3.right);

            var angles = m_followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = m_followTransform.transform.localEulerAngles.x;

            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }

           m_followTransform.transform.localEulerAngles = angles;
        }
        #endregion

        #region Move
        
        m_nextRotation = Quaternion.Euler(new Vector3(0, m_nextRotation.eulerAngles.y, 0));

        Vector2 movedirection = new Vector2(m_Direction.x, m_Direction.z);
        Vector2 a = new Vector2(0, 1f);
        float anglef = Vector2.Angle(a, movedirection);
        if (movedirection.x < 0)
        {
            anglef *= -1f;
        }

        if (movedirection == Vector2.zero) {
            m_rigidbody.angularVelocity = new Vector3(0, 0, 0);
            m_rigidbody.velocity = new Vector3(0, m_rigidbody.velocity.y, 0);
            return;
        }
            
        if (m_isMove == true)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, m_nextRotation.eulerAngles.y + anglef, 0), transform.rotation, m_rotationLerp);
            Move();
        }
        else if (m_isJump == true)
        {
            Move();
        }
        else
        {
            m_rigidbody.angularVelocity = new Vector3(0, 0, 0);
            m_rigidbody.velocity = new Vector3(0, m_rigidbody.velocity.y, 0);
        }
        m_nextRotation = Quaternion.Lerp(m_followTransform.transform.rotation, m_nextRotation, m_rotationLerp);
        #endregion
    }

    public void SetStopMoveFalse()
    {
        m_Direction = m_lastDir;
        m_stopMove = false;
    }

    protected virtual void Move()
    {   
        if(m_curSpeed < m_maxSpeed)
        {
            m_curSpeed += m_addSpeed;
        }

        Vector3 moveAmout = transform.forward * m_curSpeed * Time.deltaTime;
        Vector3 nextPosition = m_rigidbody.position + moveAmout;

        m_rigidbody.MovePosition(nextPosition);
    }
    #endregion

    #region PrivateMethod
    private void CheckInputType(string _type)
    {
        if (_type == "Mouse")
        {
            m_isMouse = true;
            m_isGamepad = false;
        }
        else
        {
            m_isGamepad = true;
            m_isMouse = false;
        }
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

    private void CeilingCheck()
    {
        if(m_isCeiling && m_isGround)
        {
            LoadSceneManager.s_Instance.ChangeScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FallingCheck()
    {
        if (!m_isGround && m_rigidbody.velocity.y < 0)
        {
            if (!m_isFalling)
            {
                m_isFalling = true;
                m_fallStartTime = Time.time;
                m_fallStartYPosition = transform.position.y;
                StartCoroutine(CheckFallingDuration());
            }
        }
        else
        {
            if (m_isFalling)
            {
                m_isFalling = false;
                if (m_fallStartYPosition - transform.position.y >= m_fallDeathHeight)
                {
                    Death();
                }
                StopCoroutine(CheckFallingDuration());
            }
        }
    }

    private IEnumerator CheckFallingDuration()
    {
        while (m_isFalling)
        {
            if (Time.time - m_fallStartTime >= 3f)
            {
                Death();
                break;
            }

            yield return null;
        }
    }

    private void Death()
    {
        m_isDead = true;
        m_grabItem = ITEM.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CheckHead()
    {
        Ray ray = new Ray(transform.position, Vector3.up);
        float rayLength = (TimeManager.s_Instance.GetCureentTime() == 0 ? 0.45f : 1.1f);
        if (Physics.Raycast(ray, rayLength))
        {
            m_isCeiling = true;
        }
        else
        {
            m_isCeiling = false;
        }
    }


    #endregion
}
