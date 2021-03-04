using UnityEngine;

/// <summary>
/// Adds a simple star-filled background
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class Stars : MonoBehaviour
{
    public int		starCount = 300;
    ParticleSystem  PS;
    ParticleSystem.Particle[] particles;

    private void Awake ()
    {
        particles = new ParticleSystem.Particle[ starCount ];
        PS = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        for (int i = 0; i < starCount; i++)
        {
            particles[i].position = ScreenBounds.GetRandomPositionFullScreen();
            particles[i].startSize = Random.Range(.05f, .15f);
            particles[i].startColor = Random.ColorHSV(0.2f, 1f, 0f, 1f, 0.25f, .65f);
        }
        PS.SetParticles(particles, particles.Length);
    }
}