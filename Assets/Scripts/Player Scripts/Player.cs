using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    [Header("Player's movements stats")]
    public float baseVerticalSpeed = 5f;
    public float baseHorizontalSpeed = 7f;
    public float dashValue;
    public float dashTime;
    public float dashDelay;
    public float rotationMagnitude;
    public float rotationSpeed;

    [Header("Playerstate checks")]
    public bool canMove;
    private bool dashReloading, isDashing, isShooting;

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
    private Main main;

    void Start()
    {
        dashReloading = false;
        main = Main.Instance;
    }

    private void Update(){

        // on déroule les timers pour la fréquence de tir
        // et la réutilisation du dash
        if(reloadTimer > 0f){
            reloadTimer -= Time.deltaTime;
        }
        if(dashTime > 0f){
            dashTime -= Time.deltaTime;
        }        

        CalculateMovement();
        PerformShooting();

        main.UpdateLifeHUD(life);

        if (life <= 0){
            PlayerDeath();
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

    private void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void UpdateRotation()
    {
        Vector3 newRotation = new Vector3(0f, -(movementMagnitude.x * rotationMagnitude) + 180f, -90f);
        Quaternion newQuaternion = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
        shipVisual.transform.rotation = Quaternion.RotateTowards(shipVisual.transform.rotation, newQuaternion, rotationSpeed);
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
        life -= receivedDamage;
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
}