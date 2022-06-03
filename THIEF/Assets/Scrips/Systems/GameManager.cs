using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;
    public Transform respawnPoint;
    public Player player;
    public void Awake()
    {
        if (!GameManager.Instance || GameManager.Instance != this) { Destroy(this); }
        instance = this;
    }
    public void Death()
    {
        player.transform.position = respawnPoint.position;
    }
}
