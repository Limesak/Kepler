using AsteroidBelt.Interfaces.Shop;
using AsteroidBelt.Player_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace AsteroidBelt.Interfaces
{
    public class Main : SingletonPersistent<Main>
    {
        public GameObject player;
        public float secondsBeforeBoss;
        private float timeLeft;

        [Header("UI references")]
        [SerializeField] private Slider lifeSlider;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text endScreenText;
        [SerializeField] private TMP_Text killText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private Animator phasesAnnounce;

        [Header("Game current state")]
        public string stateOfPlay;
        public string phaseOfBattle;
        public int enemiesToFight;
        private int enemiesLeft;
        [HideInInspector] public int killCount = 0;
        public int currentLevel;
        public int currentScore;
        [HideInInspector] public bool playerHasBombs;
        public bool phaseDone;

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
            phaseDone = false;
            stateOfPlay = "Active_Game";
            startScreen.SetActive(false);
            endScreen.SetActive(false);
            player.SetActive(true);
            phaseOfBattle = "Field_Phase";
            currentLevel = 1;
            UpdateLifeHUD(player.GetComponent<Player>().life);
            enemiesLeft = enemiesToFight;
            timeLeft = secondsBeforeBoss;
        }

        void Update(){        
            UpdateScoreHUD();

            if(stateOfPlay.Equals("Paused_Game")){
                pauseScreen.SetActive(true);
            }
            else{
                pauseScreen.SetActive(false);
            }

            if(timeLeft > 0){
                timeLeft -= Time.deltaTime;
            }

            timerText.text = "" + (int)timeLeft;

            if(timeLeft <= 0 && !phaseDone){
                phaseDone = true;
                UpdatePhase();
            }

            if(killCount == enemiesToFight){
                shopManager.OpenShop();
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
            enemiesLeft = enemiesToFight - killCount;
            UpdateKillText();
        }

        public void UpdatePhase(){
            if (phaseOfBattle.Equals("Field_Phase")){
                phaseOfBattle = "Squad_Phase";
                phasesAnnounce.SetTrigger("BossAppears");
            }
            else if (phaseOfBattle.Equals("Squad_Phase")){
                EndGame();
            }
        }

        public void NextLevel(){
            phaseOfBattle = "Field_Phase";
            enemiesLeft = enemiesToFight;
            UpdateKillText();
            currentLevel++;
        }

        public void ResetKills(){
            killCount = 0;
        }

        public void UpdateLifeHUD(int newLifeValue){
            lifeSlider.value = newLifeValue;
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