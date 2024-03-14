using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform cSpawnPoin;

    //lưu trữ id 
    private int m_id;

    protected Player m_player;

    //lưu trữ Rigidbody2D của Platform
    protected Rigidbody2D m_rb;

    public int Id { get => m_id; set => m_id = value; }

    protected virtual void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        if (!GameManager.Ins) return;
        m_player = GameManager.Ins.player;

        if (cSpawnPoin)
        {
            GameManager.Ins.SpawnCollectable(cSpawnPoin);
        }
    }
    public virtual void PlatformAction()
    {

    }
}
