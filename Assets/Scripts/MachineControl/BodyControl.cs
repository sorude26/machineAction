using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : MonoBehaviour
{
    [SerializeField]
    Transform m_target = default;
    [SerializeField]
    Animator m_animator = default;
    [SerializeField]
    GroundCheck m_groundCheck = default;
    [SerializeField]
    Transform m_camera = default;
    [SerializeField]
    LegControl m_leg = default;
    [SerializeField]
    float m_maxUpAngle = 20f;
    [SerializeField]
    float m_maxDownAngle = -10f;
    [SerializeField]
    Transform[] m_bodyControlBase = new Transform[2];
    [SerializeField]
    Transform[] m_rightControlBase = new Transform[4];
    [SerializeField]
    Transform[] m_leftControlBase = new Transform[4];
    protected Quaternion m_headRotaion = Quaternion.Euler(0, 0, 0);
    protected float m_headRSpeed = 1.0f;
    protected Quaternion m_bodyRotaion = Quaternion.Euler(0, 0, 0);
    protected float m_bodyRSpeed = 1.0f;
    protected Quaternion m_lArmRotaion = Quaternion.Euler(0, 0, 0);
    protected float m_lArmRSpeed = 1.0f;
    protected Quaternion m_lArmRotaion2 = Quaternion.Euler(0, 0, 0);
    protected float m_lArmRSpeed2 = 1.0f;
    protected Quaternion m_rArmRotaion = Quaternion.Euler(0, 0, 0);
    protected float m_rArmRSpeed = 8.0f;
    protected Quaternion m_rArmRotaion2 = Quaternion.Euler(0, 0, 0);
    protected float m_rArmRSpeed2 = 8.0f;
    float m_turn = 0;
    float m_upTurn = 0;
    bool m_action = false;
    public bool TurnNow { get; private set; }
    private void Start()
    {
        GameScene.InputManager.Instance.OnFirstInputAttack += HandAttackRight;
        //GameScene.InputManager.Instance.OnFirstInputShotR += HandAttackLeft;
    }
    private void LateUpdate()
    {
       // PartsMotion();
    }
    public void ChangeSpeed(float speed)
    {
        if (m_animator)
        {
            m_animator.SetFloat("Speed", speed);
        }
    }
    public void HandAttackLeft()
    {
        if (m_target == null)
        {
            return;
        }
        //Vector3 targetPos = m_bodyControlBase[0].TransformPoint(m_target.position);
        Vector3 targetPos = m_target.position;
        Vector3 targetDir = targetPos - m_bodyControlBase[0].position;
        targetDir.y = 0.0f;
        Quaternion endRot = Quaternion.LookRotation(targetDir);  //< 方向からローテーションに変換する
        m_bodyRotaion = endRot;
        //m_rArmRotaion = endRot;
        //targetPos = m_rightControlBase[2].TransformPoint(m_target.position);
        targetDir = targetPos - m_rightControlBase[2].position; 
        //targetDir = targetPos;
        endRot = Quaternion.LookRotation(targetDir) * Quaternion.Euler(-90,0,0);
        m_rArmRotaion2 = endRot; ;
    }
    int attackCount = 0;
    bool attack = false;
    public void HandAttackRight()
    {
        m_action = true;
        m_bodyRotaion = Quaternion.Euler(0, 0, 0);
        if (attackCount == 0)
        {
            attackCount++;
            if (m_groundCheck.IsGrounded())
            {
                ChangeAnimation("attackSwingDArm");
                //ChangeAnimation("attackKnuckleRArm");
                m_leg?.AttackMoveR();
            }
            else
            {
                ChangeAnimation("attackSwingDArm3", 0.5f);
                //ChangeAnimation("attackKnuckleRArm");
            }
            return;
        }
        attack = true;
    }
    void Attack()
    {
        if (attack)
        {
            if (attackCount == 1)
            {
                ChangeAnimation("attackSwingDArm2");
                //ChangeAnimation("attackKnuckleLArm");
                m_leg?.AttackMoveL();
            }
            else if (attackCount == 2)
            {
                ChangeAnimation("attackSwingDArm3", 0.5f);
                //ChangeAnimation("attackKnuckleRArm");
                m_leg?.AttackMoveR();
                attackCount = 0;
            }
            attackCount++;
            attack = false;
        }
    }
    void AttackEnd()
    {
        attackCount = 0;
        attack = false;
        m_action = false;
    }
    public void LookMove(Vector2 dir, float turnSpeed)
    {
        if (TurnNow)
        {
            return;
        }
        if (dir.x > 0.3f)
        {
            Turn(turnSpeed);
        }
        else if (dir.x < -0.3f)
        {
            Turn(-turnSpeed);
        }
        else
        {
            if (dir.y > 0.7f)
            {
                UpTurn(turnSpeed);
            }
            else if (dir.y < -0.7f)
            {
                UpTurn(-turnSpeed);
            }
        }
    }
    void Turn(float q)
    {
        if (TurnNow)
        {
            return;
        }
        m_turn += q;
        if (m_turn > 360)
        {
            m_turn -= 360;
        }
        if (m_turn < -360)
        {
            m_turn += 360;
        }
        Vector3 dir = Quaternion.Euler(m_upTurn, m_turn, 0) * Vector3.forward;
        TurnNow = true;
        StartCoroutine(Turn(m_camera, 1f, dir));
    }
    void UpTurn(float q)
    {
        if (TurnNow)
        {
            return;
        }
        if (q > 0)
        {
            if (m_upTurn <= m_maxUpAngle)
            {
                m_upTurn += q;
                if (m_upTurn > m_maxUpAngle)
                {
                    m_upTurn = m_maxUpAngle;
                }
            }
        }
        else
        {
            if (m_upTurn >= m_maxDownAngle)
            {
                m_upTurn += q;
                if (m_upTurn > m_maxDownAngle)
                {
                    m_upTurn = m_maxDownAngle;
                }
            }
        }
        Vector3 dir = Quaternion.Euler(m_upTurn, m_turn, 0) * Vector3.forward;
        TurnNow = true;
        StartCoroutine(Turn(m_camera, 0.8f, dir));
    }
    IEnumerator Turn(Transform tragetTransform, float turnSpeed, Vector3 targetDir)
    {
        while (true)
        {
            Vector3 newDir = Vector3.RotateTowards(tragetTransform.forward, targetDir, turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime);
            Quaternion turnRotation = Quaternion.LookRotation(newDir);
            if (tragetTransform.rotation == turnRotation)
            {
                break;
            }
            tragetTransform.rotation = turnRotation;
            yield return null;
        }
        TurnNow = false;
    }
    void ChangeAnimation(string changeTarget, float changeTime = 0.5f)
    {
        m_animator.CrossFadeInFixedTime(changeTarget, changeTime);
    }

    protected void PartsMotion()
    {
        m_bodyControlBase[1].rotation = Quaternion.Lerp(m_bodyControlBase[1].rotation, m_headRotaion, m_headRSpeed * Time.deltaTime);
        m_bodyControlBase[0].localRotation = Quaternion.Lerp(m_bodyControlBase[0].localRotation, m_bodyRotaion, m_bodyRSpeed * Time.deltaTime);
        m_leftControlBase[0].localRotation = Quaternion.Lerp(m_leftControlBase[0].localRotation, m_lArmRotaion, m_lArmRSpeed * Time.deltaTime);
        m_leftControlBase[2].localRotation = Quaternion.Lerp(m_leftControlBase[2].localRotation, m_lArmRotaion2, m_lArmRSpeed2 * Time.deltaTime);
        m_rightControlBase[0].localRotation = Quaternion.Lerp(m_rightControlBase[0].localRotation, m_rArmRotaion, m_rArmRSpeed * Time.deltaTime);
        m_rightControlBase[2].rotation = Quaternion.Lerp(m_rightControlBase[2].rotation, m_rArmRotaion2, m_rArmRSpeed2 * Time.deltaTime);
    }
}
