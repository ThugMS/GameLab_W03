using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : TimeInfluenced
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [Header("Position")]
    [SerializeField] private Transform m_start;
    [SerializeField] private Transform m_end;
    [SerializeField] private Transform m_startPlayer;
    [SerializeField] private Transform m_endPlayer;
    [SerializeField] private float m_offset;

    [Header("Transform")]
    [SerializeField] private Vector3 m_startPos;
    [SerializeField] private Vector3 m_endPos;
    [SerializeField] private Vector3 m_startPosPlayer;
    [SerializeField] private Vector3 m_endPosPlayer;
    [SerializeField] private Ease m_youngEase;
    [SerializeField] private ShakeRandomnessMode m_youthEase;
    [SerializeField] private AnimationCurve m_elderEase;
    [SerializeField] private float m_moveDis = 5f;

    [Header("Time")]
    [SerializeField] private int m_startTime = 1;

    [Header("Collider")]
    [SerializeField] private GameObject m_player;
    #endregion

    #region PublicMethod
    public override void UpdateTimeState()
    {
        if (GetObjectTIme() <= 0)
            EnableYoung();
        else if (GetObjectTIme() == 1)
            EnableYouth();
        else if (GetObjectTIme() >= 2)
            EnableElder();
    }

    private void Update()
    {
    }

    public override void Start()
    {   
        base.Start();
        m_startPos = m_start.position;
        m_endPos = m_end.position;

        m_startPosPlayer = m_startPlayer.position;
        m_endPosPlayer = m_endPlayer.position;

        SetStartTime(m_startTime);
        UpdateTimeState();

        m_offset = transform.localScale.y * 0.6f;
        //m_startPosPlayer = new Vector3(m_startPos.x, m_startPos.y + m_offset, m_startPos.z);
        //m_endPosPlayer = new Vector3(m_endPos.x, m_endPos.y + m_offset, m_endPos.z);
    }

    #endregion

    #region PrivateMethod
    private void EnableYoung()
    {
        var tween = transform.DOMove(m_startPos, TimeManager.s_Instance.m_skipTimeLength).SetEase(m_youngEase);

        if (m_player != null)
        {
            m_player.transform.DOMove(m_startPosPlayer, TimeManager.s_Instance.m_skipTimeLength).SetEase(m_youngEase);
            StartCoroutine(nameof(IE_Move), tween);
        }
    }

    private void EnableYouth()
    {
        
    }

    private void EnableElder()
    {
        var tween = transform.DOMove(m_endPos, TimeManager.s_Instance.m_skipTimeLength).SetEase(m_elderEase);

        if (m_player != null)
        {
            m_player.transform.DOMove(m_endPosPlayer, TimeManager.s_Instance.m_skipTimeLength).SetEase(m_elderEase);
            StartCoroutine(nameof(IE_Move), tween);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || m_player != null)
        {
            m_player = null;
        }
    }

    private IEnumerator IE_Move(Tweener _tween)
    {
        m_player.GetComponent<Rigidbody>().isKinematic = true;
        yield return _tween.WaitForCompletion();
        
        m_player.GetComponent <Rigidbody>().isKinematic = false;
    }
    #endregion
}
