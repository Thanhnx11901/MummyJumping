using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public Player player;
    public int startingPlatform;//khi bắt đầu tạo ra bao nhiêu Platform
    public float xSpawnOffset; // điểu kiển khoảng cách Platform theo trục x
    public float minYspawnPos;// giá trị min theo trục Y
    public float maxYspawnPos;// giá trị max theo trục Y

    public Platform[] platformPrefabs;
    public CollectableItem[] collectableItems;

    private Platform m_lastPlatformSpawned;//platform cuối cùng khi spawn ra
    private List<int> m_platformLandedIds;//lưu lại các platform người chơi đã nhảy lên
    private float m_halfCamSizeX;//biến lưu nữa chiều rộng của cam
    private int m_score;//điểm số của người chơi

    public Platform LastPlatformSpawned { get => m_lastPlatformSpawned; set => m_lastPlatformSpawned = value; }
    public List<int> PlatformLandedIds { get => m_platformLandedIds; set => m_platformLandedIds = value; }
    public int Score { get => m_score; }

    //Hủy đối tượng này khi scenes load
    public override void Awake()
    {
        MakeSingleton(false);
        m_platformLandedIds = new List<int>();
        m_halfCamSizeX = Helper.Get2DCamSize().x / 2;
    }

    private void PlatformInit()
    {
        m_lastPlatformSpawned = player.PlatformLanded;
        for (int i = 0; i < startingPlatform; i++)
        {
            SpawnPlatform();
        }
    }
    public override void Start()
    {
        base.Start();
        state = GameState.Starting;
        Invoke("PlatformInit", 0.5f);
        AudioController.Ins.PlayBackgroundMusic();
    }

    public void PlayGame()
    {
        if (GUIManager.Ins)
        {
            GUIManager.Ins.ShowGamePlay(true);
        }
        Invoke("PlayGameIvk", 1f);
    }

    private void PlayGameIvk()
    {
        state = GameState.Playing;
        if (player)
        {
            player.Jump();
        }
    }

    //phương thức kiểm tra platform đã được người chơi nhảy lên chưa
    public bool IsPlatformLanded(int id)
    {
        if (m_platformLandedIds == null || m_platformLandedIds.Count <= 0) return false;
        return m_platformLandedIds.Contains(id);
    }

    // cộng điểm 
    public void AddScore(int scoreToAdd)
    {
        if (state != GameState.Playing) return;
        m_score += scoreToAdd;
        Pref.BestScore = m_score;
        if (GUIManager.Ins)
        {
            GUIManager.Ins.UpdateScore(m_score);
        }
    }

    public void SpawnCollectable(Transform spawnPoint)
    {
        if (collectableItems == null || collectableItems.Length <= 0 || state != GameState.Playing) return;
        int randIdx = Random.Range(0, collectableItems.Length);
        var collectItem = collectableItems[randIdx];

        if (collectItem == null) return;
        float ranCheck = Random.Range(0f, 1f);
        if(ranCheck <= collectItem.spawnRate && collectItem.collectable)
        {
            var cClone = Instantiate(collectItem.collectable, spawnPoint.position, Quaternion.identity);
            cClone.transform.SetParent(spawnPoint);
        }
    }

    //phương thức tạo ra các platform
    public void SpawnPlatform()
    {
        if (!player || platformPrefabs == null || platformPrefabs.Length <= 0) return;
        float spawnPosX = Random.Range(-(m_halfCamSizeX - xSpawnOffset),(m_halfCamSizeX - xSpawnOffset)); // vị trí platform theo trục x
        float disBetweenPlat = Random.Range(minYspawnPos, maxYspawnPos); // khoảng cách theo trục y của 2 platform
        float spawnPosY = m_lastPlatformSpawned.transform.position.y + disBetweenPlat;
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0);// vị trí mà platform mới tạo ra
        int randIdx = Random.Range(0, platformPrefabs.Length);// lấy random platform
        var platformPrefab = platformPrefabs[randIdx];
        if (!platformPrefab) return;
        var platformClone = Instantiate(platformPrefab, spawnPos, Quaternion.identity); // tạo ra
        platformClone.Id = m_lastPlatformSpawned.Id + 1; // tăng id lên
        m_lastPlatformSpawned = platformClone;
    }
}
