using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBattleSystem : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public GameObject PlayerPrefab;
    public GameObject BossPrefab;
    public GameObject ActionPanel;
    public GameObject ActionHUD;
    public GameObject SpAtkHUD;
    public GameObject AnalyzePanel;

    private GameObject PlayerGO;
    private GameObject BossGO;

    public Text ActionText;
    public Text AnalyzeText;

    public BattleHUD PlayerHUD;
    public BattleHUD BossHUD;
    public MoveHUD MoveHUD;
    
    public Button FireButton;
    public Button IceButton;
    public Button WaterButton;
    public Button ElecButton;

    public BattleState State;

    private Player PlayerUnit;
    private Boss BossUnit;

    private SpriteRenderer bossSprite;

    public Button[] ActionHUDButtons;

    private void Start() {
        StartCoroutine(SetupBossBattle());
    }

    private void Update() {
        FireButton.enabled = Player.FireUsage == 0 ? false : true;
        IceButton.enabled = Player.IceUsage == 0 ? false : true;
        WaterButton.enabled = Player.WaterUsage == 0 ? false : true;
        ElecButton.enabled = Player.ElecUsage == 0 ? false : true;
    }

    private IEnumerator SetupBossBattle() {
        PlayerGO = Instantiate(PlayerPrefab, PlayerPrefab.transform.position, 
                PlayerPrefab.transform.rotation);
        PlayerUnit = PlayerGO.GetComponent<Player>();

        BossGO = Instantiate(BossPrefab, BossPrefab.transform.position,
                BossPrefab.transform.rotation);
        BossUnit = BossGO.GetComponent<Boss>();

        bossSprite = BossGO.GetComponent<SpriteRenderer>();

        PlayerHUD.SetHUD(PlayerUnit);
        BossHUD.SetHUD(BossUnit);

        yield return new WaitForSeconds(2f);

        State = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn() {
        ActionText.text = "Choose an action!";
        ActionPanel.SetActive(true);
    }

    private IEnumerator PlayerAttack(string attackType) {
        ActionText.text = attackType;
        State = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        bool isDead = BossUnit.TakeDamage(PlayerUnit.PhysDamage);

        BossHUD.SetHP(BossUnit.CurrentHP);

        State = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        if (isDead) {
            State = BattleState.WON;
            EndBattle();
        }
        else {
            StartCoroutine(BossTurn());
        }
    }

    private IEnumerator PlayerElementalAttack(string element) {
        bool isDead = false;
        switch (element.ToLower()) {
            case "fire": {
                ActionText.text = "Fire!";
                State = BattleState.ENEMYTURN;

                yield return new WaitForSeconds(2f);

                isDead = BossUnit.TakeFireDamage(PlayerUnit.FireDamage);

                Player.FireUsage -= 1;
                MoveHUD.FireCurrentUsage.text = Player.FireUsage + "/";

                break;
            }
            case "ice": {
                ActionText.text = "Ice!";
                State = BattleState.ENEMYTURN;

                yield return new WaitForSeconds(2f);

                isDead = BossUnit.TakeIceDamage(PlayerUnit.IceDamage);

                Player.IceUsage -= 1;
                MoveHUD.IceCurrentUsage.text = Player.IceUsage + "/";

                break;
            }
            case "water": {
                ActionText.text = "Water!";
                State = BattleState.ENEMYTURN;

                yield return new WaitForSeconds(2f);

                isDead = BossUnit.TakeWaterDamage(PlayerUnit.WaterDamage);

                Player.WaterUsage -= 1;
                MoveHUD.WaterCurrentUsage.text = Player.WaterUsage + "/";

                break;
            }
            case "elec": {
                ActionText.text = "Electricity!";
                State = BattleState.ENEMYTURN;

                yield return new WaitForSeconds(2f);

                isDead = BossUnit.TakeElecDamage(PlayerUnit.ElecDamage);

                Player.ElecUsage -= 1;
                MoveHUD.ElecCurrentUsage.text = Player.ElecUsage + "/";

                break;
            }
        }

        BossHUD.SetHP(BossUnit.CurrentHP);

        State = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        if (isDead) {
            State = BattleState.WON;
            EndBattle();
        }
        else {
            StartCoroutine(BossTurn());
        }
    }

    private IEnumerator PlayerGuard() {
        int prevDefence = PlayerUnit.Defence;
        PlayerUnit.Defence += 5;

        State = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        StartCoroutine(BossTurn());

        yield return new WaitForSeconds(4f);
        PlayerUnit.Defence = prevDefence;
    }

    public void OnAttackButton() {
        if (State != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack("Attack"));
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
        if (State != BattleState.PLAYERTURN)
            return;

        StartCoroutine(Analyze());
    }

    public void OnGuardButton() {
        if (State != BattleState.PLAYERTURN)
            return;

        ActionText.text = "Guard";
        StartCoroutine(PlayerGuard());
    }

    public void OnElementalButton(string element) {
        if (State != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerElementalAttack(element));
    }

    private IEnumerator Analyze() {
        ActionText.text = "Analyzing";

        AnalyzeText.text = BossUnit.UnitDescription;
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

    private IEnumerator BossTurn() {
        OnBackButton();
        
        ActionText.text = "Boss Turn!";

        yield return new WaitForSeconds(1.5f);

        switch (StaticStats.order) {
            case 0:
                ActionText.text = "Boss Attack!";

                yield return new WaitForSeconds(3f);

                bool isDead = PlayerUnit.TakeDamage(BossUnit.PhysDamage);

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

                StaticStats.order++;
                break;

            case 1:
                BossFireWeakness();

                ActionText.text = "Boss became weak to fire!";

                yield return new WaitForSeconds(1.5f);

                StaticStats.order++;
                break;

            case 2:
                BossIceWeakness();

                ActionText.text = "Boss became weak to ice!";

                yield return new WaitForSeconds(1.5f);

                StaticStats.order++;
                break;

            case 3:
                BossWaterWeakness();

                ActionText.text = "Boss became weak to water!";

                yield return new WaitForSeconds(1.5f);

                StaticStats.order++;
                break;

            case 4:
                BossElecWeakness();

                ActionText.text = "Boss became weak to electricity!";

                yield return new WaitForSeconds(1.5f);

                StaticStats.order = 0;
                break;
            } 

        State = BattleState.PLAYERTURN;
        PlayerTurn();  
    }

    private void EndBattle() {
        if (State == BattleState.WON) {
            Destroy(BossGO);

            ActionText.text = "Victory!";
            StaticStats.isPrologue = false;
            sceneLoader.LoadNarration();
        }
        else if (State == BattleState.LOST) {
            Destroy(PlayerGO);
            StaticStats.PlayerPosition = new Vector3(0f, -3f, 0f);
            StaticStats.ZombieNames.RemoveAt(StaticStats.ZombieNames.Count - 1);
            ActionText.text = "You're dead";

            sceneLoader.LoadOverworld();
        }
    }

    private void BossFireWeakness() {
        BossUnit.BecomeWeakFire();
        bossSprite.color = Color.blue;
    }
    
    private void BossIceWeakness() {
        BossUnit.BecomeWeakIce();
        bossSprite.color = Color.red;
    }

    private void BossWaterWeakness() {
        BossUnit.BecomeWeakWater();
        bossSprite.color = Color.yellow;
    }

    private void BossElecWeakness() {
        BossUnit.BecomeWeakElec();
        bossSprite.color = Color.cyan;
    }
}