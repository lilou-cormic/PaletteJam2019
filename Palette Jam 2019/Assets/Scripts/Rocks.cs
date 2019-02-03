using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    [SerializeField]
    private AudioSource HitAudioSource = null;

    private bool _isHit = false;

    private Player _player = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isHit)
            return;

        _isHit = true;

        HitAudioSource.Play();

        _player = collision.GetComponentInParent<Player>();

        _player.GameOver();

        StartCoroutine(WaitForGameOver());

    }

    private IEnumerator WaitForGameOver()
    {
        yield return new WaitForSeconds(0.7f);

        _player.ShowGameOverScreen();
    }
}
