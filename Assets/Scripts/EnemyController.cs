using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 3;

    [SerializeField] private GameObject collisionDetection;

    private Animator animator;

    private Transform pointToGoTo;
    private bool castleReached = false;

    private void Start()
    {
        pointToGoTo = GameObject.FindGameObjectWithTag("Castle").transform;
        animator = GetComponent<Animator>();

        transform.rotation = Quaternion.LookRotation(
            Vector3.RotateTowards(
                transform.forward,
                pointToGoTo.position - transform.position,
                360, 0.0f));

    }

    private void Update()
    {
        if (!castleReached)
        {
            //transform.position = Vector3.MoveTowards(transform.position, pointToGoTo.position, enemySpeed * Time.deltaTime);
        }
    }

    public void Die()
    {
        animator.SetBool("Dead", true);
        FeetSpawner.Instance.OnEnemyKilled();
        GameManager.Instance.UpdateScore(10);
        StopAllCoroutines();
        DoWithDelay(2, DestroyThis);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void DealDamage()
    {
        // Play hit animation and deal damage
        GameManager.Instance.UpdateCastleHP(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Castle"))
        {
            castleReached = true;
            StartCoroutine(DealPeriodicDamage());

            animator.SetBool("MoveTowards", false);
            animator.SetBool("Attacking", true);
        }
    }

    IEnumerator DealPeriodicDamage()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            DealDamage();
        }
    }

    IEnumerator DoWithDelay(float delay, Action onComplete)
    {
        yield return new WaitForSeconds(delay);
        onComplete();
    }
}
