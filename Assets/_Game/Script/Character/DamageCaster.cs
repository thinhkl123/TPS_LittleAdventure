using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField] private Character character;

    [SerializeField] private Collider collider;
    [SerializeField] private string targetTag;
    private List<Collider> damagedTargetList;

    private void Awake()
    {
        character = GetComponentInParent<Character>();
        collider = GetComponent<Collider>();
        collider.enabled = false;
        damagedTargetList = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.CompareTag(targetTag));
        if (other.CompareTag(targetTag) && !damagedTargetList.Contains(other))
        {
            Character targetCC = other.GetComponent<Character>();
            if (targetCC != null)
            {
                targetCC.ApplyDame(character.currentDamage, transform.parent.position);

                PlayerVFXManager playerVFXManager = transform.parent.GetComponent<PlayerVFXManager>();

                if (playerVFXManager != null)
                {
                    RaycastHit hit;

                    Vector3 originalPos = transform.position + (-collider.bounds.extents.z) * transform.forward;

                    bool isHit = Physics.BoxCast(originalPos, collider.bounds.extents / 2, transform.forward, out hit, transform.rotation, collider.bounds.extents.z, 1 << 6);

                    if (isHit)
                    {
                        playerVFXManager.PlaySlash(hit.point + new Vector3(0, 0.5f, 0));
                    }
                }

            }
            damagedTargetList.Add(other);
        }
    }
    public void EnableDamageCaster()
    {
        //Debug.Log("Enable Caster");
        damagedTargetList.Clear();
        collider.enabled = true;
    }

    public void DisableDamageCaster()
    {
        //Debug.Log("Disable Caster");
        damagedTargetList.Clear();
        collider.enabled = false;
    }
}
