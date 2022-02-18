using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class SquadLeader : EnemyHitHandler 
    {
        [Header("Combat stats")]
        [SerializeField] private float delayBetweenActions = 4f;
        public int minionsPerWave;
        private bool fightStarted;
        private string previousAction;
        private int actionsBfrExposedWeakness = 0;
        private int weakPointsRemaining = 0;

        [Header("Objects in scene")]
        public Transform leftspawn;
        public Transform rightspawn;
        public Transform[] leftMinionsSpot;
        public Transform[] rightMinionsSpot;

        [Header("Instantiated objects")]
        public GameObject minion;
        public GameObject shot;

        void Update(){
            if (transform.position.y > 2)
            { 
                transform.Translate(Vector3.down * 12f * Time.deltaTime, Space.World);
            }
            else
            {
                if(!fightStarted){
                    fightStarted = true;
                    StartCoroutine(ChooseAction());
                }
            }

            checkDeath(health, 0f);
        }

        private IEnumerator ChooseAction(){
            yield return new WaitForSeconds(delayBetweenActions);
            int rdmChoice = Random.Range(1, 3);
            switch (actionsBfrExposedWeakness){
                case 2:
                    RotateBoss();
                    break;
                case 1:
                    if(previousAction.Equals("SpawnMinions")){
                        if(rdmChoice.Equals(2)){
                            PrepareAttack();
                        }
                        else{
                            RotateBoss();
                        }
                    }
                    else if(previousAction.Equals("PrepareAttack")){
                        if(rdmChoice.Equals(2)){
                            SpawnMinions();
                        }
                        else{
                            RotateBoss();
                        }
                    }
                    break;
                case 0:
                    if(rdmChoice.Equals(1)){
                        SpawnMinions();
                    }
                    else{
                        PrepareAttack();
                    }
                    break;
            }

            StartCoroutine(EndPhase());
        }

        private void SpawnMinions(){
            Debug.Log("spawn minion");

            int randomSide = Random.Range(1, 3);

            StartCoroutine(DelayedSpawns());

            IEnumerator DelayedSpawns(){
                if (randomSide == 1){
                    for(int i = 0; i < minionsPerWave; i++){
                        yield return new WaitForSeconds(0.2f);
                        GameObject sbire = Instantiate(minion, leftspawn.position, leftspawn.rotation);
                        sbire.GetComponent<SquadMinions>().SetAttackPosition(leftMinionsSpot[i].position, true);
                    }
                }
                else if (randomSide == 2){
                    for(int i = 0; i < minionsPerWave; i++){
                        yield return new WaitForSeconds(0.2f);
                        GameObject sbire = Instantiate(minion, rightspawn.position, leftspawn.rotation);
                        sbire.GetComponent<SquadMinions>().SetAttackPosition(rightMinionsSpot[i].position, false);
                    }
                }                
            }

            previousAction = "SpawnMinions";
            actionsBfrExposedWeakness++;
        }

        private void RotateBoss(){
            Debug.Log("rotate boss");

            Vector3 rotationLeft = new Vector3(0f,0f, 90f);
            Vector3 rotationRight = new Vector3(0f,0f, 90f);
            Vector3 goToRotation = rotationLeft;

            switch (weakPointsRemaining){
                case 2:
                    goToRotation = rotationLeft;
                    break;
                case 1:
                    goToRotation = rotationRight;
                    break;
            }

            transform.DORotate(goToRotation, 0.5f, RotateMode.Fast);

            previousAction = "RotateBoss";
            actionsBfrExposedWeakness = 0;
        }

        private void SpawnBasicEnemies(){
            Debug.Log("spawn basic");
        }

        private void PrepareAttack(){
            Debug.Log("Attack");
            Instantiate(shot, transform.position, transform.rotation);

            previousAction = "PrepareAttack";
            actionsBfrExposedWeakness++;
        }

        private IEnumerator EndPhase(){
            Debug.Log("End Phase");
            yield return new WaitForSeconds(10f);
            ReturnToIdle();
        }

        private void ReturnToIdle(){
            Debug.Log("Return to idle");
            Vector3 baseRotation = new Vector3(0f, 0f, 0f);
            transform.DORotate(baseRotation, 0.5f, RotateMode.Fast);

            StartCoroutine(ChooseAction());
        }
    }
}
