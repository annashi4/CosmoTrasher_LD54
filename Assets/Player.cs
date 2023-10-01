using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void Buy()
    {
        _particleSystem.Play();
    }
}