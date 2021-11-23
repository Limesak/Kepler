using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /* PROPRIETES DU PLAYER */

    // référence vers le script Main qui gère l'affiche du HUD et les états du jeu
    // la variable est en "public", on peut donc la régler dans l'editor
    public Main main;

    // on règle ici la vitesse de déplacement du player
    private float baseVerticalSpeed = 5f;
    private float baseHorizontalSpeed = 7f;

    private float currentVerticalSpeed;
    private float currentHorizontalSpeed;

    private float StockedVerticalSpeed;
    private float StockedHorizontalSpeed;

    private float boostedCurrentVerSpeed;
    private float boostedCurrentHorSpeed;

    // var for knock
    public Transform target;
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    // dash player
    public float dashSpeed = 0.5f;
    public float dashTime = 0f;
    public float dashDelay = 0f;
    bool dashReload;
    bool isDashing = false;

    // référence au Collider
    Collider2D playerCollider;

    SpriteRenderer spriteRenderer;
    Color baseColor = Color.white;

    // associe le joueur à la position du spawn des tirs
    public Transform FirePoint;

    // associe le joueur au prefab des balles
    public GameObject bulletPrefab;

    // valeur pour le temps de recharge du tir
    float reloadShot = 0f;

    // propriété qui stocke la vie du Player
    int life = 3;

    // valeur du score
    public int score = 0;

    // création d'un tableau qui comprend les objets asteroids et valeur pour identifier le script des astéroïdes
    GameObject[] asteroids;
    Asteroid asteroidscript;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashReload = false;
        spriteRenderer.color = baseColor;
        currentHorizontalSpeed = baseHorizontalSpeed;
        currentVerticalSpeed = baseVerticalSpeed;
        StockedHorizontalSpeed = currentHorizontalSpeed;
        StockedVerticalSpeed = currentVerticalSpeed;
    }

    /* FONCTIONS DU PLAYER */

    // la fonction Update s'effectue automatiquement à chaque frame
    // on va y gérer le déplacement du Player
    // et terminer la partie si le nombre de vie du Player tombe à 0
    void Update()
    {

        Vector3 pos = transform.position;

        //controls movements
        if (pos.y >= -9 && pos.y <= 9)
        {
            pos.y += Input.GetAxis("Vertical") * currentVerticalSpeed * Time.deltaTime;
        }

        else if (pos.y < -9)
        {
            pos.y = -9;
        }

        else if (pos.y > 9)
        {
            pos.y = 9;
        }

        if (pos.x >= -17 && pos.x <= 17)
        {
            pos.x += Input.GetAxis("Horizontal") * currentHorizontalSpeed * Time.deltaTime;
        }

        else if (pos.x < -17)
        {
            pos.x = -17;
        }

        else if (pos.x > 17)
        {
            pos.x = 17;
        }

        transform.position = pos;

        // baisse la valeur de reloadShot chaque seconde
        reloadShot -= Time.deltaTime;

        //Control shots
        if (Input.GetButton("Fire") && reloadShot <= 0)
        {
            Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
            reloadShot = 0.13f;
        }

        // mécanique de dash
        dashTime -= Time.deltaTime;

        if (Input.GetButtonDown("Dash") && dashTime <= 0 && dashDelay <= 0)
        {
            dashTime = 0.8f;
            currentHorizontalSpeed = currentHorizontalSpeed + dashSpeed;
            currentVerticalSpeed = currentVerticalSpeed + dashSpeed;
            playerCollider.enabled = false;
            dashDelay = 6f;
        }

        if (dashDelay > 0f)
        {
            dashReload = true;
        }
        else if (dashDelay <= 0)
        {
            dashReload = false;
        }

        if (currentHorizontalSpeed > baseHorizontalSpeed)
        {
            isDashing = true;
        }
        else if (currentHorizontalSpeed == baseHorizontalSpeed)
        {
            isDashing = false;
        }

        dashDelay -= Time.deltaTime;

        if (isDashing)
        {
            spriteRenderer.color = Color.red;
        }

        if (dashReload && !isDashing)
        {
            spriteRenderer.color = Color.green;
        }

        if (!dashReload && !isDashing)
        {
            spriteRenderer.color = Color.white;
        }

        if (dashTime <= 0)
        {
            currentVerticalSpeed = StockedVerticalSpeed;
            currentHorizontalSpeed = StockedHorizontalSpeed;
            playerCollider.enabled = true;
        }

        //  à chaque frame, on vérifie aussi la valeur de la vie du Player
        // si cette valeur est inférieure ou égale à 0, on termine la partie
        if (life <= 0)
        {
            // on appelle la fonction EndGame sur le script Main pour afficher l'écran de fin
            main.EndGame();

            // on détruit le Player
            Destroy(gameObject);


            main.UpdateEndScreenText();
        }

        // finds each Asteroids
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        // changes speed and spawn of enemies when score goes up
        foreach (GameObject asteroid in asteroids)
        {
            asteroidscript = asteroid.GetComponent<Asteroid>();
            if (score >= 10 && score < 20)
            {
                asteroidscript.movementSpeed = 2.5f;
                main.spawnDelay = 1.3f;
            }

            if (score >= 20 && score < 40)
            {
                asteroidscript.movementSpeed = 3f;
                main.spawnDelay = 1f;
            }

            if (score >= 40 && score < 60)
            {
                asteroidscript.movementSpeed = 3.5f;
                main.spawnDelay = 0.7f;
            }

            if (score >= 60 && score < 80)
            {
                asteroidscript.movementSpeed = 4f;
                main.spawnDelay = 0.5f;
            }

            if (score >= 80 && score < 100)
            {
                asteroidscript.movementSpeed = 5f;
                main.spawnDelay = 0.3f;
            }

            if (score >= 100)
            {
                asteroidscript.movementSpeed = 6f;
                main.spawnDelay = 0.1f;

            }

            if (score >= 150)
            {
                asteroidscript.movementSpeed = 8f;
                main.spawnDelay = 0.05f;
            }

            if (score >= 250)
            {
                asteroidscript.movementSpeed = 10f;
                main.spawnDelay = 0.03f;
            }
        }

    }

    // la fonction OnTriggerEnter2D est appellée lorsque le Player collide avec un autre collider 
    // le collider qui est en collision avec le Player est passé en paramètre de la fonction, sous le nom "other"
    void OnTriggerEnter2D(Collider2D other)
    {
        // le Player est rentré en collision avec quelque chose, mais on ne sait pas encore quoi
        // on check le tag de l'élément avec lequel le Player est rentré en collision 

        if (other.tag == "Asteroid" || other.tag == "Enemy" || other.tag == "goldenAsteroid")
        {
            // si on est bien rentré en collision avec un Enemy, on détruit tout d'abord l'Enemy en question
            Destroy(other.gameObject);

            // on retire une vie au Player
            life--;

            // on met à jour l'affichage de la vie sur le HUD
            main.UpdateLifeHUD(life);

        }

        if (other.tag == "Boss")
        {
            life--;
            main.UpdateLifeHUD(life);
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, -5, 0));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        else if (other.tag == "Coin")
        {
            score++;
            main.UpdateScoreHUD(score);
        }

        else if (other.tag == "bonus")
        {
            if (life < 5)
            {
                life++;
            }
            main.UpdateLifeHUD(life);
        }
    }

    public void addBossReward()
    {
        score += 30;
        main.UpdateScoreHUD(score);
    }
}