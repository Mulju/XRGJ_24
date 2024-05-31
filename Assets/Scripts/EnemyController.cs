using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform pointToGoTo;

    public float enemySpeed = 3;

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
        transform.position = Vector3.MoveTowards(transform.position, pointToGoTo.position, enemySpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Castle"))
        {
            // Play hit animation and deal damage
            GameManager.Instance.UpdateCastleHP(1);
        }
    }
}
