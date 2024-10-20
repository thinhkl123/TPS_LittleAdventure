using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject gateVisual;
    private Collider gateCollider;
    public float openDuration = 2f;
    public float OpenTargetY = -1.5f;

    private void Awake()
    {
        gateCollider = GetComponent<Collider>();
    }

    IEnumerator OpenAnimation()
    {
        float currentOpenDuration = 0;
        Vector3 startPos = gateVisual.transform.position;
        Vector3 targetPos = startPos + Vector3.up * OpenTargetY;

        while (currentOpenDuration < openDuration)
        {
            currentOpenDuration += Time.deltaTime;
            gateVisual.transform.position = Vector3.Lerp(startPos, targetPos, currentOpenDuration / openDuration);

            yield return null;
        }

        gateCollider.enabled = false;
    }

    public void Open()
    {
        StartCoroutine(OpenAnimation());
    }
}
