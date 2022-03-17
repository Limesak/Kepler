using System;
using System.Collections;
using AsteroidBelt.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AsteroidBelt.Player_Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Player's movements stats")]
        public float baseVerticalSpeed = 5f;
        public float baseHorizontalSpeed = 7f;
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDuration;
        private float dashTimer;
        public float dashCooldown;
        private float rotationMagnitude = 40;
        private float rotationSpeed = 0.7f;

        [Header("Playerstate checks")]
        private bool canMove;
        [HideInInspector] public bool hasDash;
        private bool isDashing, isShooting, canTakeDamage;

        [Header("Objects references")]
        public GameObject shipVisual;
        private Transform shipTransform;
        public Transform firePoint;

        [Header("Shots properties")]
        public GameObject bulletPrefab;
        public GameObject doubleShotPrefab;
        public GameObject bombPrefab;
        public int bombsAmmo;
        public int maxBombAmmo;
        [HideInInspector] public bool hasDoubleShot, hasBombs;
        public float mainReloadTime;
        public float bombReloadTime;
        private float mainReloadTimer, bombReloadTimer;

        [Header("Player health properties")]
        public int life;
        public int maxLife;

        private Transform _transform;
        private Vector2 movementMagnitude, directionToDash;
        private Vector2 startPos = new Vector2(0, -16f);
        private Main main;

        void Start()
        {
            main = Main.Instance;
            canMove = true;
            canTakeDamage = true;
            _transform = transform;
            shipTransform = shipVisual.GetComponent<Transform>();
            _transform.localPosition = startPos;
        }

        private void Update()
        {
            if (!main.stateOfPlay.Equals("Paused_Game"))
            {
                // timer for firerate
                // and dash system cooldown
                if (mainReloadTimer > 0f)
                {
                    mainReloadTimer -= Time.deltaTime;
                }
                if (dashTimer > 0f)
                {
                    dashTimer -= Time.deltaTime;
                }
                if (bombReloadTimer > 0f)
                {
                    bombReloadTimer -= Time.deltaTime;
                }

                CalculateMovement();
                PerformShooting();

                if (life <= 0) PlayerDeath();

                if (isDashing)
                {
                    _transform.localPosition += (Vector3)directionToDash * Time.deltaTime;
                    canTakeDamage = false;
                }
            }
        }

        //////////////////////////////////
        /////////Movements section////////
        //////////////////////////////////
        public void MovementInput(InputAction.CallbackContext value)
        {
            Vector2 inputMagnitude = value.ReadValue<Vector2>();
            movementMagnitude = inputMagnitude.normalized;
        }

        private void CalculateMovement()
        {
            Vector2 futurePos = _transform.localPosition;
            futurePos.x += movementMagnitude.x * baseHorizontalSpeed * Time.deltaTime;
            futurePos.y += movementMagnitude.y * baseVerticalSpeed * Time.deltaTime;

            if (canMove)
            {
                _transform.localPosition = futurePos;
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
            _transform.position = Camera.main.ViewportToWorldPoint(pos);
        }

        // Rotates the player's ship when moving
        private void UpdateRotation()
        {
            Vector3 newRotation = new Vector3(0f, -(movementMagnitude.x * rotationMagnitude) + 180f, -90f);
            Quaternion newQuaternion = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
            shipTransform.rotation = Quaternion.RotateTowards(shipTransform.rotation, newQuaternion, rotationSpeed);
        }

        public void DashInput(InputAction.CallbackContext button)
        {
            if (button.started && dashTimer <= 0 && hasDash)
            {
                dashTimer = dashCooldown;
                PerformDash();
            }
        }

        private void PerformDash()
        {
            if (movementMagnitude != Vector2.zero)
            {
                directionToDash = movementMagnitude * dashSpeed;
            }
            else
            {
                directionToDash = (Vector2)transform.up * dashSpeed;
            }

            OnDashPerformed?.Invoke();
            StartCoroutine(DashCoroutine());

            IEnumerator DashCoroutine()
            {
                canMove = false;
                isDashing = true;
                yield return new WaitForSeconds(dashDuration);
                StartCoroutine(EndDash());
            }

            IEnumerator EndDash()
            {
                yield return new WaitForSeconds(0.03f);
                isDashing = false;
                canMove = true;
                canTakeDamage = true;
            }
        }

        public static event Action OnDashPerformed;

        //////////////////////////////////////////
        //////////Fire main weapon section////////
        //////////////////////////////////////////
        public void FireInput(InputAction.CallbackContext button)
        {
            if (button.started)
            {
                isShooting = true;
            }

            if (button.canceled)
            {
                isShooting = false;
            }
        }

        private void PerformShooting()
        {
            if (isShooting && mainReloadTimer <= 0)
            {
                if (!hasDoubleShot)
                {
                    Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    OnFireShot?.Invoke();
                    mainReloadTimer = mainReloadTime;
                }
                else
                {
                    Instantiate(doubleShotPrefab, firePoint.position, Quaternion.identity);
                    OnFireShot?.Invoke();
                    mainReloadTimer = mainReloadTime;
                }
            }
        }

        public static event Action OnFireShot;

        public void BombInput(InputAction.CallbackContext button)
        {
            if (button.started && bombsAmmo > 0 && hasBombs)
            {
                bombReloadTimer = bombReloadTime;
                LaunchBomb();
            }
        }

        private void LaunchBomb()
        {
            Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
            bombsAmmo--;
            OnBombLaunched?.Invoke();
        }

        public static event Action OnBombLaunched;

        ////////////////////////////////////////
        /////////////PlayerHitSection///////////
        ////////////////////////////////////////
        public void TakeDamage(int receivedDamage)
        {
            if (canTakeDamage)
            {
                life -= receivedDamage;
            }
            main.UpdateLifeHUD(life);
        }

        private void PlayerDeath()
        {
            main.EndGame();
            main.UpdateEndScreenText();
            this.gameObject.SetActive(false);
            _transform.position = startPos;
            life = 3;
            hasBombs = false;
            hasDoubleShot = false;
            hasDash = false;
            main.playerHasBombs = false;
        }
    }
}