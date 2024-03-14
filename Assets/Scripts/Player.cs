using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;//lực nhảy
    public float moveSpeed;// tốc độ di chuyển
    private Platform m_platformLanded; // plt đã nhảy qua
    private float m_movingLimitX;// giới hạn player vị trí để ko ra khỏi màn hình

    private Rigidbody2D m_rb; // lưu trữ Rigidbody2D

    public Platform PlatformLanded { get => m_platformLanded; set => m_platformLanded = value; }
    public float MovingLimitX { get => m_movingLimitX; }
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        if (!GameManager.Ins || GameManager.Ins.state != GameState.Playing) return;
        if (!m_rb || m_rb.velocity.y > 0 || !m_platformLanded) return; // vận tốc theo chiều y > 0 (tức là đg nhảy lên)
        m_rb.velocity = new Vector2(m_rb.velocity.x, jumpForce); // thực hiện nhảy với lực jumpForce
        if(m_platformLanded is BreakablePlatform)
        {
            m_platformLanded.PlatformAction();
        }
        if (AudioController.Ins)
        {
            AudioController.Ins.PlaySound(AudioController.Ins.jump);
        }
    }

    // liên quan đến vật lý thì để trong hàm này
    private void FixedUpdate()
    {
        MovingHandle();
    }
    // nút di chuyển
    private void MovingHandle()
    {
        if (!GamePadCtl.Ins || !m_rb || !GameManager.Ins || GameManager.Ins.state != GameState.Playing) return;

        if (GamePadCtl.Ins.CanMoveLeft) // di chuyển sang trái
        {
            m_rb.velocity = new Vector2(-moveSpeed, m_rb.velocity.y);
        }
        else if (GamePadCtl.Ins.CanMoveRight) // di chuyển sang phải
        {
            m_rb.velocity = new Vector2(moveSpeed, m_rb.velocity.y);
        }
        else
        {
            m_rb.velocity = new Vector2(0, m_rb.velocity.y);
        }

        m_movingLimitX = Helper.Get2DCamSize().x / 2; //lấy ra chiều rộng của camera

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -m_movingLimitX, m_movingLimitX), transform.position.y, transform.position.z);
        // Mathf.Clamp là giới hạn trục x trong khoảng m_movingLimitX

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Collectable.ToString()))
        {
            var collectable = collision.GetComponent<Collectable>();
            if (collectable)
            {
                collectable.Trigger();
            }
        }
    }
}
