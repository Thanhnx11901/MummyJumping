using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Player.ToString()))
        {
            Destroy(collision.gameObject);
            if (GameManager.Ins)
            {
                GameManager.Ins.state = GameState.Gameover;
            }
            if(GUIManager.Ins || GUIManager.Ins.gameoverDialog)
            {
                GUIManager.Ins.gameoverDialog.Show(true);
            }
        }
        if (collision.CompareTag(GameTag.Platform.ToString()))
        {
            Destroy(collision.gameObject);
        }
        if (AudioController.Ins)
        {
            AudioController.Ins.PlaySound(AudioController.Ins.gameover);
        }
    }
}
