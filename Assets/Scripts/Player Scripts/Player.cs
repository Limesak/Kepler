using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Player's movements stats")]
    public float baseVerticalSpeed = 5f;
    public float baseHorizontalSpeed = 7f;
    public float dashSpeed;
    public float dashDuration;
    private float dashTimer;
    public float dashDelay;
    public float rotationMagnitude;
    public float rotationSpeed;

    [Header("Playerstate checks")]
    public bool canMove;
    [SerializeField] private bool hasDash;
    private bool dashReloading, isDashing, isShooting, canTakeDamage;

    [Header("Objects references")]
    public GameObject shipVisual;
    public Collider playerCollider;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject doubleShotPrefab;

    [Header("Shots properties")]
    [SerializeField] private bool hasDoubleShot;
    public float reloadTime;
    private float reloadTimer;

    [Header("Player health properties")]
    public int life;
    public int maxLife;

    private Vector2 movementMagnitude;
    private Vector2 directionToDash;
    private Main main;

    void Start()
    {
        dashReloading = false;
        main = Main.Instance;
    }

    private void Update(){
        if(!main.stateOfPlay.Equals("Paused_Game")){
            // timer for firerate
            // and dash system cooldown
            if(reloadTimer > 0f){
                reloadTimer -= Time.deltaTime;
            }
            if(dashTimer > 0f){
                dashTimer -= Time.deltaTime;
            }        

            CalculateMovement();
            PerformShooting();

            main.UpdateLifeHUD(life);

            if (life <= 0){
                PlayerDeath();
            }

            if(isDashing){
                transform.localPosition += (Vector3)directionToDash * Time.deltaTime;
                canTakeDamage = false;
            }
        }
    }


    /////////Movements section////////
    public void MovementInput(InputAction.CallbackContext value){
        Vector2 inputMagnitude = value.ReadValue<Vector2>();
        movementMagnitude = inputMagnitude.normalized;
    }

    private void CalculateMovement(){
        Vector2 futurePos = transform.localPosition;
        futurePos.x += movementMagnitude.x * baseHorizontalSpeed * Time.deltaTime;
        futurePos.y += movementMagnitude.y * baseVerticalSpeed * Time.deltaTime;

        if(canMove){
            transform.localPosition = futurePos;
        }

        ClampPosition();
        UpdateRotation();
    }

    // Limits player to the boundaries of the screen
    private void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    // Rotates the player's ship when moving
    private void UpdateRotation()
    {
        Vector3 newRotation = new Vector3(0f, -(movementMagnitude.x * rotationMagnitude) + 180f, -90f);
        Quaternion newQuaternion = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
        shipVisual.transform.rotation = Quaternion.RotateTowards(shipVisual.transform.rotation, newQuaternion, rotationSpeed);
    }

    public void DashInput(InputAction.CallbackContext button){
        if(button.started && dashTimer <= 0 && hasDash){
            dashTimer = dashDelay;
            PerformDash();
        }
    }

    private void PerformDash(){
        if(movementMagnitude != Vector2.zero){
            directionToDash = movementMagnitude * dashSpeed;
        }
        else{
            directionToDash = (Vector2)transform.up * dashSpeed;
        }

        StartCoroutine(DashCoroutine());

        IEnumerator DashCoroutine(){
            canMove = false;
            isDashing = true;
            yield return new WaitForSeconds(dashDuration);
            StartCoroutine(EndDash());
        }

        IEnumerator EndDash(){
            yield return new WaitForSeconds(0.03f);
            isDashing = false;
            canMove = true;
            canTakeDamage = true;
        }
    }

    ////////////////////////////////////

    //////////Fire main weapon section////////
    public void FireInput(InputAction.CallbackContext button){
        if(button.started){
            isShooting = true;
        }

        if(button.canceled){
            isShooting = false;
        }
    }

    private void PerformShooting(){
        if(isShooting && reloadTimer <= 0){
            if(!hasDoubleShot){
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                OnFireShot?.Invoke();
                reloadTimer = reloadTime;
            }
            else{
                Instantiate(doubleShotPrefab, firePoint.position, Quaternion.identity);
                OnFireShot?.Invoke();
                reloadTimer = reloadTime;
            }
        }
    }

    public static event Action OnFireShot;
    //////////////////////////////////////////

    public void TakeDamage(int receivedDamage){
        if(canTakeDamage){
            life -= receivedDamage;
        }
    }

    public void GainHealth(int receivedHealth){
        if((life + receivedHealth) <= maxLife){
            life += receivedHealth;
        }
    }

    private void PlayerDeath(){
        main.EndGame();
        main.UpdateEndScreenText();
        Destroy(gameObject);
    }

    public void UnlockDoubleShot(){
        hasDoubleShot = true;
    }

    public void UnlockDash(){
        hasDash = true;
    }
}