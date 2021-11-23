using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour 
{

	/* PROPRIETES DU MAIN */

	// référence vers le prefab de l'Enemy, dont on va se servir pour Instantier tous les Enemy du jeu
	// la variable est en "public", on peut donc la régler dans l'editor
	public Transform asteroidPrefab;
    public Transform collectiblePrefab;
    public GameObject SquadLeader;

	// champ texte du HUD qui affiche la valeur de vie du Player
	// la variable est en "public", on peut donc la régler dans l'editor
	public TextMeshPro lifeText;

    public TextMeshPro scoreText;

    public TextMeshPro endScreenText;

    public TextMeshPro killText;

	// référence vers le gameObject de l'écran de fin, que l'on va activer lorsque la partie se terminera
	// la variable est en "public", on peut donc la régler dans l'editor
	public GameObject endScreen;

	// référence vers le gameObject de l'écran de début, qu'on va désactiver lorsque la partie commencera 
	// la variable est en "public", on peut donc la régler dans l'editor
	public GameObject startScreen;

	// la variable gameTimer va nous servir à tracker le temps qui passe pour pouvoir spawner les enemy
	float gameTimer;

    // temps de spawn des collectibles
    float collectibleTimer;

	// on règle ici l'intervalle de temps entre les spawn des Enemy
	public float spawnDelay = 1.5f;

    private int SpawnGo = 1;

    // intervalle de temps entre les spawn de collectible
    public float spawnCollectibleDelay = 3f;

	// bool qui indique si la partie est en cours ou si le joueur est sur un menu
	bool isPlaying;

    // bool qui indique la vague
    bool fieldPhase;
    bool squadPhase;

    bool IssquadPhaseStarted = false;

    public GameObject spawner;

    public GameObject bossSpawner;

    public GameObject player;

    // tracks number of killed opponents
    public int killCount = 0;
    public int enemiesLeft = 25;



	/* FONCTIONS DU MAIN */

	// la fonction start est appellée automatiquement au début du jeu
	void Start () 
	{
		// on affiche l'écran start en activant le gameObject correspondant
		// SetActive() via code équivaut à cocher/décocher la box en haut à gauche de l'inspector d'un gameObject
		startScreen.SetActive(true);        
	}

	// fonction qui lance la partie, appellée lorsque le joueur clique sur le bouton de l'écran start
	public void StartGame()
	{
		// on cache l'écran start en désactivant le gameObject correspondant
		startScreen.SetActive(false);

		// on passe le bool isPlaying à true pour indiquer que la partie vient de commencer
		isPlaying = true;

        fieldPhase = true;
        squadPhase = false;
	}

	// la fonction Update s'effectue automatiquement à chaque frame
	// l'Update du Main gère nottament le spawn des Enemy
	// à chaque frame, la variable gameTimer va s'incrémenter
	// lorque la valeur de gameTimer est supérieure ou égale à celle de spawnDelay, on Instantiate un Enemy
	void Update () 
	{
		// on execute le code de spawn des Enemy seulement si la partie a été lancée, c'set à dire si le bool isPlaying est true
		if(isPlaying)
		{
            player.SetActive(true);

			// à chaque frame, on incrémente gameTimer pour représenter le temps qui passe
			// Time.deltatime représente le temps écoulé depuis la dernière frame
			gameTimer = gameTimer + Time.deltaTime;

            collectibleTimer = collectibleTimer + Time.deltaTime;

            if (SpawnGo == 1 && fieldPhase)
            {
                spawner.SetActive(true);
                IssquadPhaseStarted = false;
            }

            else if(SpawnGo == 0 && !fieldPhase)
            {
                spawner.SetActive(false);
            }

            // à chaque frame, on vérifie la valeur de gameTimer
            if (gameTimer >= spawnDelay && fieldPhase)
			{
                SpawningEnemies();				
			}

            /* if(collectibleTimer >= spawnCollectibleDelay)
            {
                SpawningCollectibles();
            } */

            if(killCount == 25)
            {
                UpdatePhase();
                resetKills();
                spawner.SetActive(false);
            }

            if (squadPhase && !IssquadPhaseStarted)
            {
                bossSpawner.GetComponent<spawningBoss>().SpawnTheBoss();
                IssquadPhaseStarted = true;
            }
        }
        if (Input.GetButtonDown("Start") && isPlaying)
        {
            pausegame();
        }
    }

    public void pausegame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void SpawningEnemies()
    {
        int SpaceRocksNumber = Random.Range(1, 3);
        int SpaceRocks = 0;
        //si gameTimer est supérieur ou égal à spawnDelay, on Instaniate un enemy à partir du prefab passé en paramètre
        // on stock l'enemy que l'on vient de créer sur la scène dans la variable newEnemy
        for (SpaceRocks = 0 ; SpaceRocks < SpaceRocksNumber ; SpaceRocks++)
        {
            Transform newEnemy = Instantiate(asteroidPrefab).transform;

            // on créé deux variables enemyPositionX et enemyPositionY qui représentent la position que l'on va attribuer à l'Enemy nouvellement créé
            // la valeur de enemyPositionY est 6 pour que l'Enemy apparaisse juste en dehors de l'écran au dessus de la zone de jeu
            float enemyPositionX = Random.Range(-17f, 17f);
            float enemyPositionY = 10f;

            // on assigne les variables créée au dessus au transform de l'Enemy pour régler sa position (0 en x, 6 en y)
            newEnemy.transform.position = new Vector3(enemyPositionX, enemyPositionY, 0);

        }

        // puisque l'on vient de faire spawner un Enemy, on erset la valeur de gametimer à 0
        // si on ne le fait pas, gameTimer sera toujours supérieur à spawnTimer lors de la prochaine frame, et le script fera ainsi spawner un nouvel Enemy
        gameTimer = 0;
    }

    public void SpawningCollectibles()
    {
        Transform newCollectible = Instantiate(collectiblePrefab).transform;

        float collectiblePositionX = Random.Range(-17f, 17f);
        float collectiblePositionY = 10f;

        newCollectible.transform.position = new Vector3(collectiblePositionX, collectiblePositionY, 0);

        collectibleTimer = 0;
    }

    public void AddOneKill()
    {
        killCount++;
        enemiesLeft = 25 - killCount;
        UpdateKillText();
    }

     public void UpdatePhase()
    {
        if (fieldPhase)
        {
            fieldPhase = false;
            squadPhase = true;
            SpawnGo = 0;
            
        }
        else if (squadPhase)
        {
            fieldPhase = true;
            squadPhase = false;
            spawner.SetActive(true);
            SpawnGo = 1;
        }
    }

    public void resetKills()
    {
        killCount = 0;
    }

	// la fonction UpdateLifeHUD permet de changer l'affichage du champ texte représentant la vie sur le HUD
	public void UpdateLifeHUD(int newLifeValue)
	{
		// pour convertir un int (newLifeValue) en string et l'afficher en texte, on utilise l'opération "" + int
		lifeText.text = "" + newLifeValue;
	}

    public void UpdateScoreHUD(int newScoreValue)
    {
        scoreText.text = "" + newScoreValue;
    }

    public void UpdateEndScreenText()
    {
        endScreenText.text = "Your final score is " + scoreText.text;
    }

    public void UpdateKillText()
    {
        killText.text = "" + enemiesLeft;
    }

	public void EndGame()
	{
		// on passe le bool isPlaying à false pour signaler que la partie est terminée et stopper le spawn des Enemy
		isPlaying = false;

		endScreen.SetActive(true);

        spawner.SetActive(false);
    }

}