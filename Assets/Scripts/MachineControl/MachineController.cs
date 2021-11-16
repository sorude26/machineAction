using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MachineController : MonoBehaviour
{
    [SerializeField]
    MachineParameter m_parameter = default;
    [SerializeField]
    MoveControl m_moveControl = default;
    [SerializeField]
    TurnControl m_trunControl = default;
    [SerializeField]
    GroundCheck m_groundCheck = default;
    [SerializeField]
    LegControl m_leg = default;
    [SerializeField]
    BodyControl m_body = default;
    [SerializeField]
    BoosterControl m_booster = default;
    Rigidbody m_rb = default;
    [SerializeField]
    bool m_fly = default;
    bool m_jump = false;
    bool m_jet = false;
    [SerializeField]
    float m_boosterTime = 2;
    float m_boosterTimer = -1;
    Vector3 m_inputAxis = Vector3.zero;
    public Vector3 InputAxis { get => m_inputAxis; }
    private void Start()
    {
        GameScene.InputManager.Instance.OnInputAxisRaw += Move;
        GameScene.InputManager.Instance.OnInputAxisRawExit += MoveEnd;
        GameScene.InputManager.Instance.OnFirstInputJump += Jump;
        GameScene.InputManager.Instance.OnInputJump += Boost;
        GameScene.InputManager.Instance.OnFirstInputBooster += JetStart;
        m_rb = GetComponent<Rigidbody>();        
        m_leg.Set(this);
        m_leg.SetLandingTime(m_parameter.LandingTime);
        m_leg.ChangeSpeed(m_parameter.ActionSpeed);
        m_body.ChangeSpeed(m_parameter.ActionSpeed);
    }

    private void OnValidate()
    {
        m_leg.ChangeSpeed(m_parameter.ActionSpeed);
        m_body.ChangeSpeed(m_parameter.ActionSpeed);
        m_leg.SetLandingTime(m_parameter.LandingTime);
    }
    private void Move(float horizonal, float vertical)
    {
        m_inputAxis = new Vector3(horizonal, 0, vertical);
        if (!m_fly)
        {
            if (m_groundCheck.IsGrounded())
            {
                if (vertical > 0)
                {
                    m_leg.WalkStart(1);
                }
                else if (vertical < 0)
                {
                    m_leg.WalkStart(-1);
                }
                if (horizonal > 0)
                {
                    m_leg.TurnStartRight();
                }
                else if (horizonal < 0)
                {
                    m_leg.TurnStartLeft();
                }
            }
            else if(m_jump)
            {
                if (horizonal > 0)
                {
                    m_trunControl.Turn(m_rb, 1, m_parameter.TurnPower * 0.5f, m_parameter.TurnSpeed * 0.1f);
                }
                else if (horizonal < 0)
                {
                    m_trunControl.Turn(m_rb, -1, m_parameter.TurnPower * 0.5f, m_parameter.TurnSpeed * 0.1f);
                }
            }

        }
        else
        {
            int h = 0;
            int v = 0;
            if (horizonal > 0)
            {
                h = 1;
            }
            else if(horizonal < 0)
            {
                h = -1; 
            }
            if(vertical > 0)
            {
                v = 1;
                Stop();
            }
            else if (vertical < 0)
            {
                v = -1;
                Stop();
            }
            Vector3 dir = transform.right * h + transform.forward * v;
            m_moveControl.MoveFloat(m_rb, dir, m_parameter.FloatSpeed, m_parameter.MaxFloatSpeed);
            if (horizonal > 0)
            {
                m_trunControl.Turn(m_rb, 1, m_parameter.TurnPower * 0.05f, m_parameter.TurnSpeed * 0.1f);
            }
            else if (horizonal < 0)
            {
                m_trunControl.Turn(m_rb, -1, m_parameter.TurnPower * 0.05f, m_parameter.TurnSpeed * 0.1f);
            }
        }
    }
    private void MoveEnd()
    {
        if (!m_fly)
        {
            m_leg.WalkStop();
            if (m_groundCheck.IsGrounded())
            {
                Stop();
                Brake();
            }
        }
    }
    public void Walk(int angle)
    {
        if (m_groundCheck.IsGrounded())
        {
            m_rb.angularVelocity = Vector3.zero;
            m_moveControl.MoveWalk(m_rb, transform.forward * angle, m_parameter.WalkPower, m_parameter.MaxWalkSpeed);
        }
    }
    public void Jump()
    {
        if (m_fly)
        {
            Stop();
            m_moveControl.Jet(m_rb,Vector3.up * 0.5f, m_parameter.JetPower);
            return;
        }
        if (m_boosterTimer <= -1 || m_boosterTimer > 0)
        {
            m_booster.Boost();
        }
        Stop();
        m_leg.StartJump();
    }
    public void Boost()
    {
        if (m_jump && m_boosterTimer > 0)
        {
            m_rb.AddForce(Vector3.zero, ForceMode.Acceleration);
            m_boosterTimer -= Time.deltaTime;
            Vector3 vector = transform.forward * m_inputAxis.z + transform.right * m_inputAxis.x;
            m_moveControl.Jet(m_rb, Vector3.up + vector * m_parameter.JetControlPower, m_parameter.JetPower);
            if (m_boosterTimer <= 0)
            {
                m_booster.BoostEnd();
            }
        }
    }
    public void JetStart()
    {
        if (m_inputAxis == Vector3.zero)
        {
            return;
        }
        if (m_inputAxis.x != 0)
        {
            if (m_inputAxis.x > 0)
            {
                m_booster.BoostL();
            }
            else
            {
                m_booster.BoostR();
            }
        }
        else
        {
            if (m_inputAxis.z > 0)
            {
                m_booster.BoostL();
                m_booster.BoostR();
            }
        }
        m_leg.StartJet();
        m_booster.Boost();
    }
    public void Jet()
    {
        Vector3 vector = transform.forward * m_inputAxis.z + transform.right * m_inputAxis.x;
        m_rb.AddForce(vector * m_parameter.FloatSpeed + Vector3.up * 0.7f, ForceMode.Impulse);
    }
    public void Landing()
    {
        m_jump = false;
        m_booster.BoostEnd();
        m_boosterTimer = -1;
        Brake();
        Stop();
    }
    public void StartJump(Vector3 dir)
    {
        m_boosterTimer = m_parameter.JetTime;
        m_jump = true;
        m_rb.angularVelocity = Vector3.zero;
        m_moveControl.Jump(m_rb, dir, m_parameter.JumpPower);
    }
    public void TurnLeft()
    {
        m_trunControl.Turn(m_rb, -1, m_parameter.TurnPower, m_parameter.TurnSpeed);
    }
    public void TurnRight()
    {
        m_trunControl.Turn(m_rb, 1, m_parameter.TurnPower, m_parameter.TurnSpeed);
    }
    public void Turn(int angle)
    {
        m_trunControl.Turn(m_rb, angle, m_parameter.TurnPower, m_parameter.TurnSpeed);
    }
    public void Stop()
    {
        m_rb.angularVelocity = Vector3.zero;
        m_inputAxis = Vector3.zero;
    }
    public void Brake()
    {
        var v = m_rb.velocity;
        m_rb.velocity = v * 0.3f; 
        m_booster.BoostEnd();
    }
    void ChangeFloat()
    {
        if (m_fly)
        {
            m_fly = false;            
        }
        else
        {
            m_fly = true;
        }
        m_leg.ChangeMode();
    }
    void BodyTurn(Vector2 dir)
    {
        m_body.LookMove(dir,m_parameter.BodyTurnSpeed);
    }
}
