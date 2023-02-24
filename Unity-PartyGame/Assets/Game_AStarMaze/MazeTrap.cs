using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTrap : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioClip explosionSFX;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            var health = other.gameObject.GetComponent<PlayerHealth>();
            health.TakeTrapDamage();
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
            gameObject.SetActive(false);
        }
    }
}
