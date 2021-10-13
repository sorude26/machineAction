using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineControl : MonoBehaviour
{
    // <summary> 移動力 </summary>
    [SerializeField] float movePower = 10f;
    [SerializeField] float maxSpeed = 5f;
    /// <summary> 入力方向 </summary>
    Vector2 inputDirection;
    Rigidbody rigidbody;
    bool firstPush;
    bool dash;
    float pushTimer = 0.3f;
    float dashPower = 50f;
    /// <summary> 上昇力 </summary>
    [SerializeField] float jumpPower = 5f;
    float maxJumpSpeed = 8f;
    [SerializeField] float turnSpeed = 2f;
    [SerializeField] float m_isGroundedLength = 0.2f;
    /// <summary> 飛翔力 </summary>
    float flySpeed = 50f;
    /// <summary> 着地硬直時間計 </summary>
    float waitTimer = 0;
    float turnTimer = 0;
    [SerializeField] Transform ground;
    private void Awake()
    {
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        Vector3 dir = Vector3.forward * inputDirection.y + Vector3.right * inputDirection.x;
        if (dir == Vector3.zero && IsGrounded2())
        {
            if (!dash)
            {
                rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f);
                if (waitTimer <= 0)
                {
                }
                else
                {
                    
                    waitTimer -= Time.deltaTime;
                }
                turnTimer = 0;
            }
        }
        else if (IsGrounded2())
        {
            if (!dash)
            {
                Vector3 velo = dir.normalized * movePower;
                if (waitTimer <= 0)
                {
                   
                    if (turnTimer > 0.1f)
                    {
                        dir = Camera.main.transform.TransformDirection(dir);
                        dir.y = 0;
                        Quaternion targetRotation = Quaternion.LookRotation(dir);
                        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                    }
                    else
                    {
                        turnTimer += Time.deltaTime;
                    }
                }
                else
                {
                    waitTimer -= Time.deltaTime;
                    velo.x = 0;
                    velo.z = 0;
                }
                velo.y = rigidbody.velocity.y;
                rigidbody.velocity = velo;
            }
        }
        else if (dir != Vector3.zero)
        {
            if (!dash)
            {
                if (turnTimer > 0.1f)
                {
                    dir = Camera.main.transform.TransformDirection(dir);
                    dir.y = 0;
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                }
                else
                {
                    turnTimer += Time.deltaTime;
                }
            }
        }
        if (!dash)
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                if (!firstPush)
                {
                    firstPush = true;
                    pushTimer = 0.3f;
                }
                else
                {
                    dash = true;
                    dir.y = 0.01f;
                    rigidbody.AddForce(dir.normalized * dashPower, ForceMode.Impulse);
                    
                }
            }
            else if (firstPush)
            {
                if (pushTimer > 0)
                {
                    pushTimer -= Time.deltaTime;
                }
                else
                {
                    firstPush = false;
                }
            }
        }
        else
        {
            if (pushTimer <= 0.3f)
            {
                pushTimer += Time.deltaTime;
            }
            else
            {
                dash = false;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() && waitTimer <= 0)
            {
                rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            else
            {
                if (dir != Vector3.zero)
                {
                    rigidbody.velocity = Vector3.zero;
                    dir.y = 0.01f;
                    rigidbody.AddForce(dir.normalized * flySpeed, ForceMode.Impulse);
                }
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 dir = Vector3.forward * inputDirection.y + Vector3.right * inputDirection.x;
        if (Input.GetButton("Jump") && !IsGrounded2())
        {
            if (dir != Vector3.zero)
            {
                rigidbody.velocity = Vector3.zero;
                dir.y = 0.01f;
                rigidbody.AddForce(dir.normalized * flySpeed * 8f);
                
            }
            else
            {
                float speed = rigidbody.velocity.y;
                if (speed <= maxJumpSpeed)
                {
                    rigidbody.AddForce(Vector3.up * jumpPower * 5f);
                }
                
                Vector3 v = new Vector3(rigidbody.velocity.x * 0.99f, rigidbody.velocity.y, rigidbody.velocity.z * 0.99f);
                rigidbody.velocity = v;
            }
            waitTimer = 0.5f;
        }
        else if (!IsGrounded2())
        {
            if (dir == Vector3.zero)
            {
                Vector3 v = new Vector3(rigidbody.velocity.x * 0.995f, rigidbody.velocity.y, rigidbody.velocity.z * 0.995f);
                rigidbody.velocity = v;
                
            }
            else
            {
                rigidbody.velocity = Vector3.zero;
                dir.y = 0.01f;
                rigidbody.AddForce(dir.normalized * flySpeed * 4f);
                
            }
            waitTimer = 0.5f;
        }
    }

    bool IsGrounded()
    {
        Vector3 start = this.transform.position;   // start: オブジェクトの中心
        Vector3 end = start + Vector3.down * m_isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }
    bool IsGrounded2()
    {
        bool isGrounded = Physics.BoxCast(ground.position, new Vector3(1, 0.1f, 1), Vector3.down);
        if (isGrounded)
        {
            Debug.Log("On");
        }
        return isGrounded;
    }
}
