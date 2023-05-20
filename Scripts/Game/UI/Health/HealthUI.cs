using Assets.Scripts.Game.Simulation;
using Assets.Scripts.Game.Simulation.Player;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private List<HeartToggler> _hearts = new();
    private Player _player;
    
    public HealthUI() 
    {
        SimulationState.PlayerChanged += ChangePlayer;
        ChangePlayer(SimulationState.Instance.CurrentPlayingPlayer);
    }

    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var toggler = child.GetComponent<HeartToggler>();
            if(toggler != null)
            {
                _hearts.Add(toggler);
            }
        }
    }

    private void ChangePlayer(Player newPlayer)
    {
        if(_player != null)
        {
            _player.PlayerHealth.PlayerHasDamaged -= UpdateHealth;
        }
        _player = newPlayer;
        if(_player != null)
        {
            _player.PlayerHealth.PlayerHasDamaged += UpdateHealth;
        }
    }

    private void UpdateHealth(PlayerHealth playerHealth)
    {
        Debug.Log("Health UI updated");
        foreach(var heart in _hearts)
        {
            heart.SetAlive(playerHealth.CurrentLivesCount >= heart.HpRemaining);
        }
    }

    private void OnDestroy()
    {
        
    }
}
