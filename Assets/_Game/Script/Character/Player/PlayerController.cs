using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerController : Character
{
    [Header(" State Machine ")]
    public PlayerBaseState currentState;
    public PlayerNormalState NormalState = new PlayerNormalState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerAttackState AttackState = new PlayerAttackState();
    public PlayerHeavyAttackState HeavyAttackState = new PlayerHeavyAttackState();
    public PlayerSlideState SlideState = new PlayerSlideState();
    public PlayerBeingHitState BeingHitState = new PlayerBeingHitState();
    public PlayerDeadState DeadState = new PlayerDeadState();

    [Header(" Input ")]
    public PlayerInput input;

    [Header(" CharController ")]
    public CharacterController characterController;

    [Header(" VFX ")]
    public PlayerVFXManager vFXManager;

    [Header(" Animation ")]
    public Animator animator;
    public float allowPlayerRotation = 0.1f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    [Header(" Third Person Camera ")]
    public GameObject _mainCamera;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    public float _targetRotation;
    public float _rotationVelocity;
    public float _verticalVelocity;
    public float _terminalVelocity = 53.0f;

    [Header(" Info ")]
    public GameObject visual;
    public float gravity = -9.8f;
    public float moveSpeed = 5f;
    public Vector3 movementVelocity;
    public float verticalVelocity;

    [Header("Cinemachine")]
    public CinemachineVirtualCamera PlayerFollowCamera;

    //Camera Zoom
    public float cameraZoomSpeed = 0.3f;
    public float cameraZoomMax = 15f;
    public float cameraZoomMin = 4f;
    

    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    [Header(" Jump ")]
    public float jumpHeight = 2f;
    public bool isJumping = false;

    [Header(" Attack Slide ")]
    //Attack Slide
    public float attackStartTime;
    public float attackSlideDuration = 0.4f; 
    public float attackSlideSpeed = 0.06f;

    [Header(" Heavy Attack ")]
    public float timeToHeavyAttack = 1f;
    public float currentHeavyAttackTime;
    public bool canHeavyAttack = true;
    public float energyToHeavyAttack = 30f;

    [Header(" Attack ")]
    //Attack
    public float attackAnimationDuration;

    [Header(" Target Enemy When Attack ")]
    public EnemyGroup enemyGroup;
    public float maxDistanceTarget = 5f;

    [Header(" Slide ")]
    public float slideSpeed = 9f;
    public float energyToSlide = 20f;

    [Header(" Hit Impact ")]
    public Vector3 impactOnPlayer;

    [Header(" Energy Bar ")]
    public Slider energySlider;
    public float energyAmountMax = 200;
    public bool canIncreaseEnergy = true;

    [Header(" Coin ")]
    public TextMeshProUGUI coinValueText;
    public int coinValue = 0;


    public override void Awake()
    {
        base.Awake();

        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();

        currentHeavyAttackTime = timeToHeavyAttack;
    }

    private void Start()
    {
        SwitchToState(NormalState);

        Init();
    }

    private void Init()
    {
        energySlider.value = 1f;
        coinValueText.text = "0";
    }

    private void Update()
    {
        if (GameManager.Ins.State != GameManager.GameState.Playing)
        {
            return;
        }

        if (input.mouseRightButtonUp)
        {
            canHeavyAttack = true;
        }

        currentState.UpdateState(this);

        if (impactOnPlayer.magnitude > 0.2f)
        {
            movementVelocity = impactOnPlayer * Time.deltaTime;
        }
        impactOnPlayer = Vector3.Lerp(impactOnPlayer, Vector3.zero, Time.deltaTime * 5);

        if (!isJumping)
        {
            if (!characterController.isGrounded)
            {
                verticalVelocity = gravity;
            }
            else
            {
                verticalVelocity = gravity * 0.3f;
            }

            movementVelocity += verticalVelocity * Vector3.up * Time.deltaTime;
        }


        characterController.Move(movementVelocity);
    }

    private void LateUpdate()
    {
        if (GameManager.Ins.State != GameManager.GameState.Playing)
        {
            return;
        }

        CameraRotation();
        CameraZoom();

        if (canIncreaseEnergy)
        {
            IncreaseEnergy();
        }
    }

    public void SwitchToState(PlayerBaseState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.EnterState(this);
        }
    }

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetYaw += input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += input.look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private void CameraZoom()
    {
        float inputMouseScroll = UnityEngine.Input.mouseScrollDelta.y;
        //Debug.Log(inputMouseScroll);
        
        if (inputMouseScroll < 0f)
        {
            float distance = PlayerFollowCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance;
            distance += cameraZoomSpeed;
            PlayerFollowCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = Mathf.Clamp(distance, cameraZoomMin, cameraZoomMax);

        }
        else if (inputMouseScroll > 0f)
        {
            float distance = PlayerFollowCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance;
            distance -= cameraZoomSpeed;
            PlayerFollowCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = Mathf.Clamp(distance, cameraZoomMin, cameraZoomMax);
        }
        
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public override void ApplyDame(float dame, Vector3 attackPos = default)
    {
        base.ApplyDame(dame, attackPos);

        if (isVincible)
        {
            return;
        }

        SoundManager.Ins.Play("Hurt");

        if (!isDead)
        {
            SwitchToState(BeingHitState);
            AddImpact(attackPos, 10f);
        }
    }

    private void AddImpact(Vector3 attackPos, float force)
    {
        Vector3 impactDir = transform.position - attackPos;
        impactDir.Normalize();
        impactDir.y = 0;
        impactOnPlayer = impactDir * force;
        transform.rotation = Quaternion.LookRotation(-impactDir);
    }

    public float GetCurrentEnergyAmount()
    {
        return energySlider.value * energyAmountMax;
    }

    public void UpdateEnergyAmount(float value)
    {
        energySlider.value = value/energyAmountMax;
    }

    public void IncreaseEnergy()
    {
        UpdateEnergyAmount(GetCurrentEnergyAmount() + 0.05f);
    }

    public override void Death()
    {
        base.Death();

        GameManager.Ins.LostGame();

        SwitchToState(DeadState);
    }

    public void PickUpItem(PickUpItem item)
    {
        switch (item.type)
        {
            case global::PickUpItem.PickUpType.Heal:
                GetHeal(item.value);
                break;
            case global::PickUpItem.PickUpType.Coin: 
                AddCoin(item.value);
                break;
        }
    }

    public void AddCoin(int value)
    {
        SoundManager.Ins.Play("Coin");
        coinValue += value;
        coinValueText.text = coinValue.ToString();
    }

    public override void GetHeal(float heal)
    {
        SoundManager.Ins.Play("Heal");

        base.GetHeal(heal);

        vFXManager.PlayHealVFX();
    }
}
