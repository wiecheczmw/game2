using System;
using Pathfinding;
using UnityEngine;


public class CharacterStatistics : MonoBehaviour
{

    [SerializeField] 
    private Statistic movementspeed;

    public Statistic MovementSpeed=>movementspeed;
    
    private void Start()
    {
        // GetComponent<RichAI>().maxSpeed = movementspeed.Value;
        GetComponent<FollowerEntity>().maxSpeed = movementspeed.Value;
        
    }

    private void OnEnable()
    {
        movementspeed.OnChange += setmovementspeed;
    }

    private void OnDisable()
    {
        movementspeed.OnChange -= setmovementspeed;
    }

    private void setmovementspeed()
    {
        //GetComponent<RichAI>().maxSpeed = movementspeed.Value;
        GetComponent<FollowerEntity>().maxSpeed = movementspeed.Value;
    }
    
    
    
}
