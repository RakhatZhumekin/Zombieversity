using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    public bool isMoving;
    private Vector2 input;
    public SceneLoader sceneLoader;

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
        StaticStats.PlayerPosition = transform.position;
        sceneLoader.LoadBattle();
    }
}
