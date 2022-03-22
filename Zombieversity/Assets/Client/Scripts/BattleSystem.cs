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

        StartCoroutine(PlayerAttack());
    }

    private IEnumerator PlayerAttack() {
        ActionText.text = "Attack";
        bool isDead = EnemyUnit.TakeDamage(PlayerUnit.Damage);

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

    private IEnumerator EnemyTurn() {
        ActionText.text = "Enemy Turn!";
        yield return new WaitForSeconds(3f);

        ActionText.text = "Enemy Attack!";

        bool isDead = PlayerUnit.TakeDamage(EnemyUnit.Damage);

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
