using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public enum PickUpType
    {
        Heal,
        Coin
    }

    public PickUpType type;
    public int value = 20;
    public ParticleSystem collectVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().PickUpItem(this);
            if (collectVFX != null)
            {
                Instantiate(collectVFX, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
