using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [Header(" State Machine ")]
    public EnemyBaseState currentState;
    public EnemyNormalState NormalState = new EnemyNormalState();
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyDeadState DeadState = new EnemyDeadState();
    public EnemySpawnState SpawnState = new EnemySpawnState();

    public enum EnemyType
    {
        Melee,
        Archer,
    }
    [Header(" Type of enemy ")]
    public EnemyType enemyType;

    [Header(" NavMesh ")]
    public NavMeshAgent agent;
    public Transform target;
    public bool isTargeting;
    public Character characterTarget;

    [Header(" Visual ")]
    public Animator animator;
    public EnemyVFXManager vfxManager;

    [Header(" Info ")]
    public float speed;

    [Header(" Spawn ")]
    public float currentSpawnTime;

    [Header(" Attack ")]
    public bool canAttack = true;
    public float timeEachAttack = 3f;

    [Header(" Shoot ")]
    public GameObject bullet;
    public Transform shootingPoint;

    public override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        characterTarget = target.GetComponent<Character>();
        agent.speed = speed;
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        collider.enabled = false;
        SwitchToState(SpawnState);
    }

    private void Update()
    {
        if (GameManager.Ins.State != GameManager.GameState.Playing || isDead)
        {
            agent.enabled = false;  
            return;
        }

        currentState.UpdateState(this);
    }

    public void SwitchToState(EnemyBaseState state)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.EnterState(this);
        }
    }

    public override void ApplyDame(float dame, Vector3 attackPos = default)
    {
        base.ApplyDame(dame, attackPos);

        SoundManager.Ins.Play("EnemyHurt");

        vfxManager.PlayBeingHitVFX(attackPos);
    }

    public override void Death()
    {
        base.Death();

        SwitchToState(DeadState);
    }

    public void CoolDownTimeToAttack()
    {
        StartCoroutine(CoolDownTimeToAttackCoroutine());
    }

    IEnumerator CoolDownTimeToAttackCoroutine()
    {
        canAttack = false;

        yield return new WaitForSeconds(timeEachAttack);

        canAttack = true;
    }

    public void Shoot()
    {
        if (bullet != null)
        {
            Debug.Log("Shoot");
            Instantiate(bullet, shootingPoint.position, Quaternion.LookRotation(shootingPoint.forward));
        }
    }
}
