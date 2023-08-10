using Core.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameReaction : MonoBehaviour
{
    // Particle system
    [SerializeField] private ParticleSystem _reactionParticleSys;
    [SerializeField] private Material _happyParticleSystem;
    [SerializeField] private Material _sadParticleSystem;

    private ParticleSystemRenderer _particleSystemRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystemRenderer = _reactionParticleSys.GetComponent<ParticleSystemRenderer>();
    }

    public void SetHappyReaction(Vector2 particlePosition)
    {
        if (_reactionParticleSys.isPlaying)
        {
            _reactionParticleSys.Stop();
        }

        _reactionParticleSys.Clear();
        _particleSystemRenderer.material = _happyParticleSystem;
        _reactionParticleSys.Play();
    }

    // TODO: lump this with above function
    public void SetSadReaction(Vector2 particlePosition)
    {
        if (_reactionParticleSys.isPlaying)
        {
            _reactionParticleSys.Stop();
        }

        _reactionParticleSys.Clear();
        _particleSystemRenderer.material = _sadParticleSystem;
        _reactionParticleSys.Play();
    }

    public void EndMinigame()
    {
        MinigameSceneController.Instance.EndMinigame();
    }

    public void ReplayMinigame()
    {
        MinigameManager.Instance.StartMinigame(MinigameManager.Instance.GetCurrentMinigame().minigameID, MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty);
    }
}
