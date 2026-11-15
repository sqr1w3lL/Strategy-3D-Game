using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CarAttack : MonoBehaviour
{
    [NonSerialized] public int _health = 100;
    [SerializeField] private float _timeToAttack = 1f;
    public float radius = 70f;
    public GameObject bullet;
    private Coroutine _coroutine = null;


    private void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        if(hitColliders.Length == 0 && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;

            if(gameObject.CompareTag("Enemy"))
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        }

        foreach(var element in hitColliders)
        {
            if ((gameObject.CompareTag("Player") && element.gameObject.CompareTag("Enemy")) || (gameObject.CompareTag("Enemy") && element.gameObject.CompareTag("Player")))
            {
                if (gameObject.CompareTag("Enemy"))
                    GetComponent<NavMeshAgent>().SetDestination(element.transform.position);

                if (_coroutine == null)
                    _coroutine = StartCoroutine(StartAttack(element));
            }
        }
    }

    IEnumerator StartAttack(Collider enemy)
    {
        GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
        obj.GetComponent<BulletController>().position = enemy.transform.position;
        yield return new WaitForSeconds(_timeToAttack);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
