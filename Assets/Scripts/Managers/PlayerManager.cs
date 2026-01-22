using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : Manager
{
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    Vector3 playerSpawnPoint;
    public override void Init()
    {
        var player= Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
       //do poprawy
        FindAnyObjectByType<CinemachineCamera>(FindObjectsInactive.Exclude).Follow = player.transform;
    }

    public override void Enable()
    {
      
    }

    public override void Disable()
    {
      
    }
}
