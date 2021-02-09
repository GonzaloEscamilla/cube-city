using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticlesHandler : MonoBehaviour
{
    [SerializeField] protected ParticleSystem[] particles;
    
    [ContextMenu("Play")]
    public virtual void Play()
    {
        foreach (ParticleSystem particle in particles)
        {
            if (!particle.isPlaying)
            {
                particle.Play();
            }
        }
    }
}
