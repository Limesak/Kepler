using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private bool dashReloading, isDashing;    

    [Header("Objects references")]
    public GameObject shipVisual;
    public Collider playerCollider;
    public Transform FirePoint;
    public GameObject bulletPrefab;

    [Header("Shots properties")]
    public float reloadShot;

    [Header("Player health properties")]
    public int life;

    private Vector2 movementMagnitude;
    private Main main;

    void Start()
    {
        dashReloading = false;
        main = Main.Instance;
    }

    private void Update(){

        // on déroule les timers pour la fréquence de tir et la réutilisation du dash
        if(reloadShot > 0f){
            reloadShot -= Time.deltaTime;
        }
        if(dashTime > 0f){
            dashTime -= Time.deltaTime;
        }        

        CalculateMovement();

        if (life <= 0){
            PlayerDeath();
        }
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

    void UpdateRotation()
    {
        Vector3 newRotation = new Vector3(0f, -(movementMagnitude.x * rotationMagnitude) + 180f, -90f);
        Quaternion newQuaternion = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
        shipVisual.transform.rotation = Quaternion.RotateTowards(shipVisual.transform.rotation, newQuaternion, rotationSpeed);
    }

    public void MovementInput(InputAction.CallbackContext value){
        Vector2 inputMagnitude = value.ReadValue<Vector2>();
        movementMagnitude = inputMagnitude.normalized;
    }

    private void PlayerDeath(){
        main.EndGame();
        main.UpdateEndScreenText();
        Destroy(gameObject);
    }
}