using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticle : MonoBehaviour
{
    private ParticleSystem dustParticle;
    int safeIndex = 0;

    void Start()
    {
        dustParticle = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(safeIndex > 0)
            dustParticle.Play();
        safeIndex++;
    }
}
