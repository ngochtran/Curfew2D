using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[RequireComponent(typeof(Light2D))]

public class WorldLight : MonoBehaviour
{
    public float duration = 5f;

    [SerializeField] private Gradient gradient;
    private Light2D _light;
    private float _startTime = 0;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _startTime = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var timeElapsed = Time.time - _startTime;
        var percentage = Mathf.Sin(timeElapsed / duration * Mathf.PI * 2) * 0.5f + 0.5f;
        percentage = Mathf.Clamp01(percentage);
        _light.color = gradient.Evaluate(percentage);
    }
}
