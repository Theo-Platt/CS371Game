using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, MIDDLE, PLAYERTURN, ENEMYTURN, WIN, LOSS}
public class GameSys : MonoBehavior
{
    public GameObject player;
    public GameObject enemy;

    public Transform playerLocation;
    public Transform enemyLocation;

    public BattleState state;
    
    // Start called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        Instantiate(player, playerLocation);
        Instantiate(enemy, enemyLocation);
            
    }
}