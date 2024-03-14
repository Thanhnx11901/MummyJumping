using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (!collision.gameObject.CompareTag(GameTag.Platform.ToString())) return; // nếu ko va chạm với platform
        var platformLanded = collision.gameObject.GetComponent<Platform>(); // platform hiện tại
        if (!GameManager.Ins || !GameManager.Ins.player || !platformLanded) return;
        GameManager.Ins.player.PlatformLanded = platformLanded; // nếu đã nhảy qua platform khác thì gắn nó vào cái mới
        GameManager.Ins.player.Jump(); //nhảy
        // kiểm tra xem người chơi đã nhảy tới platform này chưa, nếu chưa thì cộng điểm và add vào list 
        if (!GameManager.Ins.IsPlatformLanded(platformLanded.Id))
        {
            int randScore = Random.Range(3, 8);
            GameManager.Ins.AddScore(randScore);
            GameManager.Ins.PlatformLandedIds.Add(platformLanded.Id);
        }
    }
}
