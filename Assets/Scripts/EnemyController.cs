using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 3;

    [SerializeField] private GameObject collisionDetection;


    private Transform pointToGoTo;
    private bool castleReached = false;

    private void Start()
    {
        pointToGoTo = GameObject.FindGameObjectWithTag("Castle").transform;

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
            transform.position = Vector3.MoveTowards(transform.position, pointToGoTo.position, enemySpeed * Time.deltaTime);
        }
    }

    private void DealDamage()
    {
        // Play hit animation and deal damage
        GameManager.Instance.UpdateCastleHP(1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Castle"))
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Castle"))
        {
            castleReached = true;
            StartCoroutine(DealPeriodicDamage());
        }
    }

    IEnumerator DealPeriodicDamage()
    {
        yield return new WaitForSeconds(1);
        DealDamage();
    }
}
