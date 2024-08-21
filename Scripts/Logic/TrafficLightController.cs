using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public List<GameObject> trafficLights;
    public float greenTime = 10f;
    public float yellowTime = 3f;
    private float timer = 0f;
    private bool isSwitch = false;
    private bool isYellow = false;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.CompareTag("TrafficLight");
        }

        for(int i = 0; i < trafficLights.Count; i++)
            SetSignalColor(trafficLights[i], "yellow");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!isSwitch && !isYellow && timer >= yellowTime)
        {
            timer = 0f;
            SetNextSignal();
            isYellow = true;
        }

        if (isSwitch && !isYellow && timer >= yellowTime) 
        {
            timer = 0f;
            SetNextSignal();
            isYellow = true;
        }
        if (!isSwitch && isYellow && timer >= greenTime)
        {
            timer = 0f;
            isSwitch = true;
            SetNextSignal();
            isYellow = false;
        }
        if (isSwitch && isYellow && timer >= greenTime)
        {
            timer = 0f;
            isSwitch = false;
            SetNextSignal();
            isYellow = false;
        }
    }

    private void SetNextSignal()
    {
        for (int i = 0; i < trafficLights.Count; i++)
        {
            if (!isYellow)
            {
                if (((i % 3) != 0))
                {
                    if (!isSwitch)
                        SetSignalColor(trafficLights[i], "red");
                    else
                        SetSignalColor(trafficLights[i], "green");
                }
                else
                {
                    if (!isSwitch)
                        SetSignalColor(trafficLights[i], "green");
                    else
                        SetSignalColor(trafficLights[i], "red");
                }
            }
            else
            {
                SetSignalColor(trafficLights[i], "yellow");
            }
        }
    }

    private void SetSignalColor(GameObject trafficLight, string color)
    {
        Light[] lights = trafficLight.GetComponentsInChildren<Light>();

        foreach (Light light in lights)
        {
            switch (color)
            {
                case "red":
                    light.color = Color.red;
                    break;
                case "yellow":
                    light.color = Color.yellow;
                    break;
                case "green":
                    light.color = Color.green;
                    break;
                default:
                    light.color = Color.black;
                    break;
            }
        }
    }
}