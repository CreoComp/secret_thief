using Assets.Scripts.Game.Simulation.RageSystem;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public const int MAX_LIVES = 3;

    public event Action<PlayerHealth> PlayerHasDied;
    public event Action<PlayerHealth> PlayerHasDamaged;
    public int CurrentLivesCount { get; private set; } = MAX_LIVES;

    private void Start()
    {
        RageScale.RageScaleFull += TakeDamage;
    }

    public void TakeDamage()
    {
        CurrentLivesCount -= 1;
        PlayerHasDamaged?.Invoke(this);

        if (CurrentLivesCount <= 0)
            PlayerHasDied?.Invoke(this);
        Debug.Log("Player damaged");
    }
    public void Win()
    {
        CurrentLivesCount = MAX_LIVES;
    }

    private void OnDestroy()
    {
        RageScale.RageScaleFull -= TakeDamage;
    }
}
