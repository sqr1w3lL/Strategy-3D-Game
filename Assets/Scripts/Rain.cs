using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Rain : MonoBehaviour
{
    public Light directionalLight;
    private ParticleSystem _particleSystem;
    private bool _isRaining = false;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }

    private void Update()
    {
        if (_isRaining && directionalLight.intensity > 0.35f)
            LightIntensity(-1);
        else if (!_isRaining && directionalLight.intensity < 1f)
            LightIntensity(1);
    }

    private void LightIntensity(int mult)
    {
        directionalLight.intensity += Time.deltaTime * 0.1f * mult;
    }

    private IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 15f));

            if (_isRaining)
                _particleSystem.Stop();
            else
                _particleSystem.Play();

            _isRaining = !_isRaining;

        }
    }

}
