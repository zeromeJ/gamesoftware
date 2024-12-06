using UnityEngine;
using System.Collections;

public class TigerBGMController : MonoBehaviour
{
    // BGM
    [SerializeField] private AudioSource gameAudioSource; // BGM AudioSource
    [SerializeField] private AudioSource endingAudioSource; // BGM AudioSource

    public void StartTigerGame()
    {
        endingAudioSource.Stop();
        gameAudioSource.Play();
        gameAudioSource.pitch = 1.05f;
    }
    public void DuringTigerFever(bool isFever)
    {
        gameAudioSource.pitch = isFever ? 2f : 1f;
    }

    public void OnEndingPannel()
    {
        gameAudioSource.Stop();
        endingAudioSource.Play();
    } 
}
