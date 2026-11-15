using System;
using System.Collections;
using UnityEngine;

public class AutoCarCreate : MonoBehaviour
{
    [NonSerialized]
    public bool IsEnemy = false;
    
    public GameObject carPrefab;
    [SerializeField] private float _time = 5f;

    private void Start()
    {
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        for(int i = 0; i <= 3; i++)
        {
            yield return new WaitForSeconds(_time);

            Vector3 pos = new Vector3(
                transform.GetChild(0).position.x + UnityEngine.Random.Range(-3f, 3f),
                transform.GetChild(0).position.y,
                transform.GetChild(0).position.z + UnityEngine.Random.Range(-3f, 3f));
            GameObject spawn = Instantiate(carPrefab, pos, Quaternion.identity);

            if(IsEnemy)
                spawn.tag = "Enemy";
        }
    }
}
