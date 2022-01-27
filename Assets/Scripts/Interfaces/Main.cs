using AsteroidBelt.Interfaces.Shop;
using AsteroidBelt.Player_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AsteroidBelt.Interfaces
{
    public class Main : SingletonPersistent<Main>
    {
        public GameObject player;

        [Header("UI references")]
        [SerializeField] private TextMeshPro lifeText;
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private TextMeshPro endScreenText;
        [SerializeField] private TextMeshPro killText;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private GameObject startScreen;

        [Header("Game current state")]
        public string stateOfPlay;
        public string phaseOfBattle;
        public int enemiesLeft = 25;
        [HideInInspector] public int killCount = 0;
        public int currentLevel;
        public int currentScore;
        [HideInInspector] public bool playerHasBombs;

        PauseAction pauseAction;
        ShopManager shopManager;

        public override void Awake(){
            base.Awake();
            pauseAction = new PauseAction();
        }

        private void OnEnable(){
            if(_instance == this){
                pauseAction.Enable();
            }
        }

        private void OnDisable(){
            if(_instance == this){
                pauseAction.Disable();
            }
        }

        void Start () {
            pauseAction.Pause.PauseGame.performed += _ => ChoosePauseAction();
            startScreen.SetActive(true);
            stateOfPlay = "Start_Screen";
            shopManager = ShopManager.Instance;
        }

        // Lance la partie en appuyant sur start sur le premier écran
        public void StartGame(){
            stateOfPlay = "Active_Game";
            startScreen.SetActive(false);
            endScreen.SetActive(false);
            player.SetActive(true);
            phaseOfBattle = "Field_Phase";
            currentLevel = 1;
            UpdateLifeHUD(player.GetComponent<Player>().life);
        }

        void Update(){        
            UpdateScoreHUD();

            if(killCount == 25){
                UpdatePhase();
                ResetKills();
            }
        }

        private void ChoosePauseAction(){
            switch (stateOfPlay){
                case "Start_Screen":
                    StartGame();
                    break;
                case "Active_Game":
                    PauseGame();
                    break;
                case "Paused_Game":
                    if(!shopManager.shopIsOpen){
                        ResumeGame();
                    }
                    break;
                case "Game_Over":
                    RestartGame();
                    break;
            }
        }

        public void PauseGame(){
            Time.timeScale = 0;
            stateOfPlay = "Paused_Game";
        }

        public void ResumeGame(){
            Time.timeScale = 1;
            stateOfPlay = "Active_Game";
        }

        public void AddOneKill(){
            killCount++;
            enemiesLeft = 25 - killCount;
            UpdateKillText();
        }

        public void UpdatePhase(){
            if (phaseOfBattle.Equals("Field_Phase")){
                phaseOfBattle = "Squad_Phase";
            }
            else if (phaseOfBattle.Equals("Squad_Phase")){
                shopManager.OpenShop();
            }
        }

        public void NextLevel(){
            phaseOfBattle = "Field_Phase";
            enemiesLeft = 25;
            currentLevel++;
        }

        public void ResetKills(){
            killCount = 0;
        }

        public void UpdateLifeHUD(int newLifeValue){
            lifeText.text = "" + newLifeValue;
        }

        public void UpdateScoreHUD(){
            scoreText.text = "" + currentScore;
        }

        public void UpdateEndScreenText(){
            endScreenText.text = "Your final score is " + scoreText.text;
        }

        public void UpdateKillText(){
            killText.text = "" + enemiesLeft;
        }

        public void EndGame(){
            endScreen.SetActive(true);
            stateOfPlay = "Game_Over";
        }

        public void RestartGame(){
            ResetKills();
            UpdateScoreHUD();
            endScreen.SetActive(false);
            stateOfPlay = "Start_Screen";
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            startScreen.SetActive(true);
        }
    }
}