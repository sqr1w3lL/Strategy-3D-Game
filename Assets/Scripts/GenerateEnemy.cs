using UnityEngine;
using System.Collections;

public class GenerateEnemy : MonoBehaviour
{
    [SerializeField] private float _time = 10f;
    public Transform[] points;
    public GameObject enemyFactory;

    private void Start()
    {
        StartCoroutine(SpawnFactory());
    }

    IEnumerator SpawnFactory()
    {
        for (int i = 0; i < points.Length; i++)
        {
            yield return new WaitForSeconds(_time);
            GameObject spawn = Instantiate(enemyFactory);
            Destroy(spawn.GetComponent<PlaceObjects>());
            spawn.transform.position = points[i].position;
            spawn.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
            spawn.GetComponent<AutoCarCreate>().enabled = true;
            spawn.GetComponent<AutoCarCreate>().IsEnemy = true;
        }
    }



}
