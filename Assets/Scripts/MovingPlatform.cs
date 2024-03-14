using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform
{
    public float moveSpeed; // vận tốc platform di chuyển   
    public bool m_canMoveRight; // di chuyển sang phải
    public bool m_canMoveLeft; // di chuyển sang trái
    protected override void Start()
    {
        base.Start();

        // random hướng di chuyển ban đầu
        float randCheck = Random.Range(0f, 1f);
        if(randCheck <= 0.5f)
        {
            m_canMoveLeft = true;
            m_canMoveRight = false;
        }
        else
        {
            m_canMoveLeft = false;
            m_canMoveRight = true;
        }
    }
    private void FixedUpdate()
    {
        float curSpeed = 0;
        if (!m_rb) return;
        // xét tốc độ khi đi về bên trái or bên phải
        if (m_canMoveLeft)
        {
            curSpeed = -moveSpeed;
        }else if (m_canMoveRight)
        {
            curSpeed = moveSpeed;
        }
        m_rb.velocity = new Vector2(curSpeed, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //xử lí khi va chạm 
        if (collision.CompareTag(GameTag.LeftCorner.ToString()))
        {
            m_canMoveLeft = false;
            m_canMoveRight = true;
        }
        else if (collision.CompareTag(GameTag.RightCorner.ToString()))
        {
            m_canMoveLeft = true;
            m_canMoveRight = false;
        }
    }
}

