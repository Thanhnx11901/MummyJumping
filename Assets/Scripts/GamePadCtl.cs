using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadCtl : Singleton<GamePadCtl>
{
    public bool isOnbile;
    private bool m_canMoveRight;
    private bool m_canMoveLeft;

    public bool CanMoveRight { get => m_canMoveRight; set => m_canMoveRight = value; }
    public bool CanMoveLeft { get => m_canMoveLeft; set => m_canMoveLeft = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }
    private void Update()
    {
        if (isOnbile) return;
        m_canMoveLeft = Input.GetAxisRaw("Horizontal") < 0 ? true : false;
        m_canMoveRight = Input.GetAxisRaw("Horizontal") > 0 ? true : false;

    }
}
