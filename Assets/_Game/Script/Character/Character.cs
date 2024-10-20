using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header(" State ")]
    public bool isVincible = false;
    public bool isDead = false;
    public float currentDamage;

    [Header(" Indicator ")]
    public float HP;
    public float attackDamage;
    public float heavyAttackDamage;
    public float spawnDuration = 2f;

    [Header(" Collider ")]
    public Collider collider;

    [Header(" Health ")]
    public Slider healthSlider;

    [Header(" Material Animation ")]
    public MaterialPropertyBlock materialPropertyBlock;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header(" Drop Item ")]
    public GameObject dropItem;

    public virtual void Awake()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(materialPropertyBlock);

        collider = GetComponent<Collider>();
        healthSlider.value = 1;
    }

    public virtual void ApplyDame(float dame, Vector3 attackPos = new Vector3())
    {
        if (this.isVincible)
        {
            return;
        }

        GetDame(dame);

        //Debug.Log(GetCurrentHealthValue());

        if (GetCurrentHealthValue() > 0)
        {
            StartCoroutine(MaterialBlink());
        }
        else
        {
            Death();
        }
    }

    IEnumerator MaterialBlink()
    {
        materialPropertyBlock.SetFloat("_blink", 0.4f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        yield return new WaitForSeconds(0.2f);

        materialPropertyBlock.SetFloat("_blink", 0f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    public float GetCurrentHealthValue()
    {
        return healthSlider.value * HP;
    }

    public void GetDame(float dame)
    {
        //Debug.Log("Current Health: " + GetCurrentHealthValue());
        float currentHP = Mathf.Floor(GetCurrentHealthValue() - dame);
        healthSlider.value = currentHP / HP;
    }

    public virtual void GetHeal(float heal)
    {
        float currentHP = Mathf.Floor(GetCurrentHealthValue() + heal);
        healthSlider.value = currentHP / HP;
    }

    public virtual void Death()
    {
        isDead = true;
    }

    public void MaterialDissolve()
    {
        StartCoroutine(MaterialDissolveCoroutine());
    }

    public IEnumerator MaterialDissolveCoroutine()
    {
        yield return new WaitForSeconds(2f);

        float dissolveTimeDuration = 2f;
        float currentDissolveTime = 0f;
        float dissolveHeight_Start = 20f;
        float dissolveHeight_target = -10f;
        float dissolveHeight;

        materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        while (currentDissolveTime < dissolveTimeDuration)
        {
            currentDissolveTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeight_Start, dissolveHeight_target, currentDissolveTime / dissolveTimeDuration);
            materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
            yield return null;
        }

        DropItem();
    }

    public void DropItem()
    {
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }

    public void MaterialAppear()
    {
        StartCoroutine(MaterialAppearCouroutine());
    }

    public IEnumerator MaterialAppearCouroutine()
    {
        float dissolveTimeDuration = spawnDuration;
        float currentDissolveTime = 0f;
        float dissolveHeight_Start = -10f;
        float dissolveHeight_target = 20f;
        float dissolveHeight;

        materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        while (currentDissolveTime < dissolveTimeDuration)
        {
            currentDissolveTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeight_Start, dissolveHeight_target, currentDissolveTime / dissolveTimeDuration);
            materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
            yield return null;
        }

        materialPropertyBlock.SetFloat("_enableDissolve", 0f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
