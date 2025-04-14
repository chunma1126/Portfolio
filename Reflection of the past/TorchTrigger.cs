using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TorchTrigger : MonoBehaviour
{
    private bool isPlayer;
    [SerializeField] private Player Player;
    
    public void StayEvent()
    {
        if (isPlayer)
        {
            Player.AddTorchTimer(Time.deltaTime);
        }
    }
    
    public void ExitEvent()
    {
        Player = null;
        isPlayer = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        isPlayer = other.TryGetComponent(out Player);
    }
}