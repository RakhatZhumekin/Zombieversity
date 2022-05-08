using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public BattleManager battleManager;

    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject ActionPanel;
    public GameObject ActionHUD;
    public GameObject SpAtkHUD;
    public GameObject AnalyzePanel;

    private GameObject PlayerGO;

    public Text ActionText;
    public Text AnalyzeText;

    public BattleHUD PlayerHUD;
    public MoveHUD MoveHUD;

    public Button[] ActionHUDButtons;
    public GameObject[] EnemyGOs;
    public Unit[] EnemyUnits;
    public BattleHUD[] EnemyHUDs;

    public Button FireButton;
    public Button IceButton;
    public Button WaterButton;
    public Button ElecButton;

    public BattleState State;

    private Player PlayerUnit;

    private void Start()
    {
        State = BattleState.START;

        EnemyGOs = new GameObject[battleManager.numOfZombies];
        EnemyUnits = new Unit[EnemyGOs.Length];

        for (int i = 0; i < EnemyGOs.Length; i++) { 
            EnemyHUDs[i].gameObject.SetActive(true);
        }

        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        FireButton.enabled = Player.FireUsage == 0 ? false : true;
        IceButton.enabled = Player.IceUsage == 0 ? false : true;
        WaterButton.enabled = Player.WaterUsage == 0 ? false : true;
        ElecButton.enabled = Player.ElecUsage == 0 ? false : true;
    }

    private IEnumerator SetupBattle() {
        PlayerGO = Instantiate(PlayerPrefab, PlayerPrefab.transform.position, 
                PlayerPrefab.transform.rotation);
        PlayerUnit = PlayerGO.GetComponent<Player>();

        for (int i = 0; i < EnemyGOs.Length; i++) {
            GameObject enemy = Instantiate(battleManager.Zombies[battleManager.randomIndex],
                StaticStats.EnemyPositions[i], 
                EnemyPrefab.transform.rotation);

            EnemyGOs[i] = enemy;

            EnemyUnits[i] = enemy.GetComponent<Unit>();
        }

        PlayerHUD.SetHUD(PlayerUnit);

        for (int i = 0; i < EnemyGOs.Length; i++) { 
            EnemyHUDs[i].SetHUD(EnemyUnits[i]);
        }

        yield return new WaitForSeconds(2f);

        State = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn() {
        ActionText.text = "Choose an action!";
        ActionPanel.SetActive(true);
    }

    public void OnAttackButton() {
        if (State != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack("Attack", PlayerUnit.PhysDamage));
    }

    public void OnSpecialButton() {
        if (State != BattleState.PLAYERTURN)
            return;

        ActionHUD.SetActive(false);
        SpAtkHUD.SetActive(true);
    }

    public void OnBackButton() {
        ActionHUD.SetActive(true);
        SpAtkHUD.SetActive(false);
    }

    public void OnAnalyzeButton() {
        StartCoroutine(Analyze());
    }

    public void OnGuardButton() {
        ActionText.text = "Guard";
        StartCoroutine(PlayerGuard());
    }

    public void OnElementalButton(string element) {
        StartCoroutine(PlayerElementalAttack(element));
    }

    private IEnumerator Analyze() {
        ActionText.text = "Analyzing";

        AnalyzeText.text = EnemyUnits[0].UnitDescription;
        AnalyzePanel.SetActive(true);

        foreach (Button btn in ActionHUDButtons)
        {
            btn.enabled = false;
        }

        yield return new WaitForSeconds(5f);

        AnalyzePanel.SetActive(false);
        ActionText.text = "Choose an action!";

        foreach (Button btn in ActionHUDButtons)
        {
            btn.enabled = true;
        }
    }

    private IEnumerator PlayerAttack(string attackType, int damage) {
        ActionText.text = attackType;

        yield return new WaitForSeconds(2f);

        bool isDead = false;

        for (int i = 0; i < EnemyGOs.Length; i++) {
            isDead = EnemyUnits[i].TakeDamage(damage);
        }

        for (int i = 0; i < EnemyGOs.Length; i++) {
            EnemyHUDs[i].SetHP(EnemyUnits[i].CurrentHP);
        }

        State = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        if (isDead) {
            State = BattleState.WON;
            EndBattle();
        }
        else {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator PlayerElementalAttack(string element) {
        bool isDead = false;
        switch (element.ToLower()) {
            case "fire": {
                ActionText.text = "Fire!";

                yield return new WaitForSeconds(2f);

                for (int i = 0; i < EnemyGOs.Length; i++) {
                    isDead = EnemyUnits[i].TakeFireDamage(PlayerUnit.FireDamage);
                }

                Player.FireUsage -= 1;
                MoveHUD.FireCurrentUsage.text = Player.FireUsage + "/";

                break;
            }
            case "ice": {
                ActionText.text = "Ice!";

                yield return new WaitForSeconds(2f);

                for (int i = 0; i < EnemyGOs.Length; i++) {
                    isDead = EnemyUnits[i].TakeIceDamage(PlayerUnit.IceDamage);
                }

                Player.IceUsage -= 1;
                MoveHUD.IceCurrentUsage.text = Player.IceUsage + "/";

                break;
            }
            case "water": {
                ActionText.text = "Water!";

                yield return new WaitForSeconds(2f);

                for (int i = 0; i < EnemyGOs.Length; i++) {
                    isDead = EnemyUnits[i].TakeWaterDamage(PlayerUnit.WaterDamage);
                }

                Player.WaterUsage -= 1;
                MoveHUD.WaterCurrentUsage.text = Player.WaterUsage + "/";

                break;
            }
            case "elec": {
                ActionText.text = "Electricity!";

                yield return new WaitForSeconds(2f);

                for (int i = 0; i < EnemyGOs.Length; i++) {
                    isDead = EnemyUnits[i].TakeElecDamage(PlayerUnit.ElecDamage);
                }

                Player.ElecUsage -= 1;
                MoveHUD.ElecCurrentUsage.text = Player.ElecUsage + "/";

                break;
            }
        }

        for (int i = 0; i < EnemyGOs.Length; i++) {
            EnemyHUDs[i].SetHP(EnemyUnits[i].CurrentHP);
        }

        State = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        if (isDead) {
            State = BattleState.WON;
            EndBattle();
        }
        else {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator PlayerGuard() {
        int prevDefence = PlayerUnit.Defence;
        PlayerUnit.Defence += 5;

        State = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());

        yield return new WaitForSeconds(4f);
        PlayerUnit.Defence = prevDefence;
    }

    private IEnumerator EnemyTurn() {
        OnBackButton();
        
        ActionText.text = "Enemy Turn!";
        yield return new WaitForSeconds(3f);

        ActionText.text = "Enemy Attack!";

        bool isDead = false;

        for (int i = 0; i < EnemyGOs.Length; i++) {
           isDead = PlayerUnit.TakeDamage(EnemyUnits[i].PhysDamage);
        }

        PlayerHUD.SetHP(PlayerUnit.CurrentHP);

        yield return new WaitForSeconds(1f);

        if (isDead) {
            State = BattleState.LOST;
            EndBattle();
        }
        else {
            State = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle() {
        if (State == BattleState.WON) {
            for (int i = 0; i < EnemyGOs.Length; i++) {
                Destroy(EnemyGOs[i]);
            }

            ActionText.text = "Victory!";
            sceneLoader.LoadOverworld();
        }
        else if (State == BattleState.LOST) {
            Destroy(PlayerGO);
            StaticStats.PlayerPosition = new Vector3(0f, -3f, 0f);
            StaticStats.ZombieNames.RemoveAt(StaticStats.ZombieNames.Count - 1);
            ActionText.text = "You ded";
        }
    }
}
