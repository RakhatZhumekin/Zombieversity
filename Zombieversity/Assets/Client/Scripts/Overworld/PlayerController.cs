using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    public bool isMoving;
    private Vector2 input;
    public SceneLoader sceneLoader;

    public GameObject WarningPanel;
    public Text WarningText;

    private Animator animator;

    private void Awake() {
        transform.position = StaticStats.PlayerPosition;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving) {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero) {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                isMoving = true;
            }
        }

        Vector3 endPoint = transform.position + new Vector3(input.x * 0.2f, input.y * 0.2f, 0);
        animator.SetBool("isMoving", isMoving);

        if (isMoving) {
            transform.DOMove(endPoint, 0.5f);
            isMoving = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ZombieController>() != null) {
            StaticStats.PlayerPosition = transform.position;

            StaticStats.ZombieNames.Add(other.gameObject.name);

            if (other.gameObject.tag.Equals("Boss")) {
                sceneLoader.LoadBossBattle();
            }
            else {
                sceneLoader.LoadBattle();
            }
        }
        else {
            switch (other.gameObject.tag) {
                case "Portal": 
                    if (StaticStats.isInside) {
                        StaticStats.isInside = false;
                        sceneLoader.LoadOverworld();
                    }
                    else {
                        StaticStats.isInside = true;
                        sceneLoader.LoadOverworld();
                    }
                    break;
                
                case "Fire":
                    StaticStats.PickedItems.Add(other.name);
                    StartCoroutine(ItemPickupWarning(other.gameObject.tag));
                    Destroy(other.gameObject);
                    Player.FireUsage = Player.MaxFireUsage;
                    break;

                case "Ice":
                    StaticStats.PickedItems.Add(other.name);
                    StartCoroutine(ItemPickupWarning(other.gameObject.tag));
                    Destroy(other.gameObject);
                    Player.IceUsage = Player.MaxIceUsage;
                    break;

                case "Water":
                    StaticStats.PickedItems.Add(other.name);
                    StartCoroutine(ItemPickupWarning(other.gameObject.tag));
                    Destroy(other.gameObject);
                    Player.WaterUsage = Player.MaxWaterUsage;
                    break;

                case "Elec":
                    StaticStats.PickedItems.Add(other.name);
                    StartCoroutine(ItemPickupWarning(other.gameObject.tag));
                    Destroy(other.gameObject);
                    Player.ElecUsage = Player.MaxElecUsage;
                    break;
            }
        }
    }

    private IEnumerator ItemPickupWarning(string element) {
        WarningPanel.SetActive(true);
        WarningText.text = element + " usage fully recovered!";

        yield return new WaitForSeconds(1.5f);

        WarningPanel.SetActive(false);
    }
}
