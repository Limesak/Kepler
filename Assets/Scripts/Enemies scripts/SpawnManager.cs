using AsteroidBelt.Interfaces;
using UnityEngine;

namespace AsteroidBelt.Enemies_scripts
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("List of enemies")]
        [SerializeField] private GameObject standardFighter;
        [SerializeField] private GameObject asteroid;
        [SerializeField] private GameObject goldenAsteroid;
        [SerializeField] private GameObject levelBoss;

        [Header("State of spawning")]
        public bool bossSpawned;

        [Header("Properties of spawn")]
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private float standardFighterDelay;
        [SerializeField] private float asteroidDelay;
        [SerializeField] private float goldenAsteroiddDelay;
        private float fighterTimer;
        private float asteroidTimer;
        private float goldenTimer;

        Main main;

        private void Start(){
            main = Main.Instance;
            fighterTimer = standardFighterDelay;
            asteroidTimer = asteroidDelay;
            goldenTimer = goldenAsteroiddDelay;
        }

        private void Update(){
            if(main.stateOfPlay.Equals("Active_Game")){
                fighterTimer -= Time.deltaTime;
                asteroidTimer -= Time.deltaTime;
                goldenTimer -= Time.deltaTime;

                switch (main.phaseOfBattle){
                    case "Field_Phase":
                        SpawnAnEnemy();
                        SpawnAnAsteroid();
                        SpawnGoldenAsteroid();
                        bossSpawned = false;
                        break;
                    case "Squad_Phase":
                        SpawnTheBoss();
                        break;
                }
            }
        }

        private void SpawnAnEnemy(){
            if(fighterTimer <= 0){
                Transform newFighter = Instantiate(standardFighter).transform;
                float posx = Random.Range(-11f, 11f);
                newFighter.position = new Vector2(posx, spawnTransform.position.y);

                standardFighterDelay = Random.Range(0.5f, 3f);
                fighterTimer = standardFighterDelay;
            }        
        }

        private void SpawnAnAsteroid(){
            if(asteroidTimer <= 0){
                int numberAsteroids = Random.Range(1, 3);

                for(int i = 0; i < numberAsteroids; i++){
                    Transform newAsteroid = Instantiate(asteroid).transform;
                    float posx = Random.Range(-25f, 25f);
                    newAsteroid.position = new Vector2(posx, spawnTransform.position.y);
                }
                asteroidDelay = Random.Range(0.6f, 1.8f);
                asteroidTimer = asteroidDelay;
            }
        }

        private void SpawnGoldenAsteroid(){
            if(goldenTimer <= 0){
                Transform newGolden = Instantiate(goldenAsteroid).transform;
                float posx = Random.Range(-15f, 15f);
                newGolden.position = new Vector2(posx, spawnTransform.position.y);

                goldenAsteroiddDelay = Random.Range(7.4f, 13.5f);
                goldenTimer = goldenAsteroiddDelay;
            }            
        }

        private void SpawnTheBoss(){
            if(!bossSpawned){
                levelBoss.SetActive(true);
                bossSpawned = true;
            }
        }
    }
}