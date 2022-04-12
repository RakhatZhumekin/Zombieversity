using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject ActionPanel;
    public GameObject ActionHUD;
    public GameObject SpAtkHUD;

    private GameObject PlayerGO;
    private GameObject EnemyGO;

    public Text ActionText;

    public BattleHUD PlayerHUD;
    public BattleHUD EnemyHUD;

    public BattleState State;

    private Unit PlayerUnit;
    private Unit EnemyUnit;

    private void Start()
    {
        State = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle() {
        PlayerGO = Instantiate(PlayerPrefab, PlayerPrefab.transform.position, 
                PlayerPrefab.transform.rotation);
        PlayerUnit = PlayerGO.GetComponent<Unit>();

        EnemyGO = Instantiate(EnemyPrefab, EnemyPrefab.transform.position, 
                EnemyPrefab.transform.rotation);
        EnemyUnit = EnemyGO.GetComponent<Unit>();

        PlayerHUD.SetHUD(PlayerUnit);
        EnemyHUD.SetHUD(EnemyUnit);

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

        StartCoroutine(PlayerAttack("Attack", PlayerUnit.Damage - EnemyUnit.Defence));
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

    public void OnGuardButton() {
        ActionText.text = "Guard";
        StartCoroutine(PlayerGuard());
    }

    public void OnElementalButton(string elemental) {
        int damage = PlayerUnit.Damage - EnemyUnit.Defence;
        string attackText = "";

        switch (elemental.ToLower()) {
            case "fire": {
                damage -= EnemyUnit.FireResistance;
                attackText = "Fire!"; 
                break;
            }
            case "ice": {
                damage -= EnemyUnit.IceResistance;
                attackText = "Ice!"; 
                break;
            }
            case "water": {
                damage -= EnemyUnit.WaterResistance; 
                attackText = "Water!";
                break;
            }
            case "elec": {
                damage -= EnemyUnit.ElectricResistance; 
                attackText = "Electricity!";
                break;
            }
        }

        if (damage < 0) 
            damage = 0;

        StartCoroutine(PlayerAttack(attackText, damage));
    }

    private IEnumerator PlayerAttack(string attackType, int damage) {
        ActionText.text = attackType;

        yield return new WaitForSeconds(2f);

        bool isDead = EnemyUnit.TakeDamage(damage);

        EnemyHUD.SetHP(EnemyUnit.CurrentHP);

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

        EnemyHUD.SetHP(EnemyUnit.CurrentHP);

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

        bool isDead = PlayerUnit.TakeDamage(EnemyUnit.Damage - PlayerUnit.Defence);

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
            Destroy(EnemyGO);
            ActionText.text = "Victory!";
        }
        else if (State == BattleState.LOST) {
            Destroy(PlayerGO);
            ActionText.text = "You ded";
        }
    }
}
