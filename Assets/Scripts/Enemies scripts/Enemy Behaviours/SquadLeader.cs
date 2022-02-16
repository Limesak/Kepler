using UnityEngine;
using System.Collections;

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

        [Header("Objects in scene")]
        public Transform leftspawn;
        public Transform rightspawn;

        [Header("Instantiated objects")]
        public GameObject minion;
        public GameObject shot;

        void Update(){
            if (transform.position.y > 2)
            { 
                transform.Translate(Vector3.down * 3.5f * Time.deltaTime, Space.World);
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
            int rdmChoice = Random.Range(1, 2);
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
                    if(previousAction.Equals("PrepareAttack")){
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
        }

        private void SpawnMinions(){

            int randomSide = Random.Range(1, 3);
            if (randomSide == 1){
                for(int i = 0; i < minionsPerWave; i++){
                    GameObject sbire = Instantiate(minion, leftspawn.position, leftspawn.rotation);

                }
            }
            else if (randomSide == 2){
                GameObject sbire = Instantiate(minion, rightspawn.position, leftspawn.rotation);
                //sbire.GetComponent<Minion>().checkRight();
            }

            previousAction = "SpawnMinions";
            actionsBfrExposedWeakness++;
        }

        private void RotateBoss(){
            previousAction = "RotateBoss";
            actionsBfrExposedWeakness = 0;
        }

        private void SpawnBasicEnemies(){

        }

        private void PrepareAttack(){

            Instantiate(shot, transform.position, transform.rotation);

            previousAction = "PrepareAttack";
            actionsBfrExposedWeakness++;
        }

        private IEnumerator ReturnToIdle(){
            

            yield return new WaitForSeconds(7);
            StartCoroutine(ChooseAction());
        }
    }
}
