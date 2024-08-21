using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Drawing;

public class CarMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform lowFuelWaypoint;
    public Transform copsWaypoint;
    public static NavMeshAgent agent;
    public GameObject gamePanel;
    public List<GameObject> stoplight;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI CoordText;
    public TextMeshProUGUI StatusText;
    public TextMeshProUGUI FuelText;
    public TextMeshProUGUI statusFuelText;
    private System.Random random = new System.Random();
    private int currentWaypointIndex = 0;
    private float stopTime = 10f;
    public bool boolcops = false;
    private bool boolcops2 = false;
    private bool boolfuel = false;
    private float speed;
    private float pastspeed;
    public static float roundedSpeed;
    private string coordinates;
    private float xpos;
    private float ypos;
    private float zpos;
    private uint fuel;
    public static bool isLowFuel = false;
    public static float fuelPercentage;
    private float coordfuelminx;
    private float coordfuelmaxx;
    private float coordfuelminz;
    private float coordfuelmaxz;
    private float coordcopsminx;
    private float coordcopsmaxx;
    private float coordcopsminz;
    private float coordcopsmaxz;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();

        foreach (Transform child in transform)
        {
            child.CompareTag("stopLight");
        }

        for (int i = 0; i < stoplight.Count; i++)
            SetSignalColor(stoplight[i], "black");
        fuel = 1500;

        coordfuelmaxx = lowFuelWaypoint.position.x + 10;
        coordfuelminx = lowFuelWaypoint.position.x - 10;
        coordfuelmaxz = lowFuelWaypoint.position.z + 10;
        coordfuelminz = lowFuelWaypoint.position.z - 10;

        coordcopsmaxx = copsWaypoint.position.x + 20;
        coordcopsminx = copsWaypoint.position.x - 20;
        coordcopsmaxz = copsWaypoint.position.z + 20;
        coordcopsminz = copsWaypoint.position.z - 20;
    }

    void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            if (agent.pathPending)
                return;
            MoveToNextWaypoint();
        }

        Vector3 position = transform.position;
        xpos = Mathf.Round(position.x * 100) / 100;
        ypos = Mathf.Round(position.y * 100) / 100;
        zpos = Mathf.Round(position.z * 100) / 100;
        coordinates = "X: " + xpos + ", Y: " + ypos + ", Z: " + zpos;
        CoordText.text = "Coordinates: " + coordinates;

        speed = agent.velocity.magnitude;
        roundedSpeed = Mathf.Round(speed * 100) / 100;
        roundedSpeed = (float)Mathf.Floor(roundedSpeed);
        SpeedText.text = "Speed: " + roundedSpeed.ToString();

        if (roundedSpeed != 0)
        {
            StatusText.text = "Status: In motion";
        }
        else
        {
            StatusText.text = "Status: Motionless";
        }

        for (int i = 0; i < stoplight.Count; i++)
        {
            if (roundedSpeed >= pastspeed)
            {
                SetSignalColor(stoplight[i], "black");
            }
            else
            {
                SetSignalColor(stoplight[i], "red");
            }
        }
        pastspeed = agent.velocity.magnitude;
        pastspeed = Mathf.Round(pastspeed * 100) / 100;

        if (fuel >= 1)
        {
            fuel--;
        }
        else
        {
            fuel = 0;
        }

        fuelPercentage = (fuel / 5000f) * 100;
        fuelPercentage = (float)Mathf.Floor(fuelPercentage);
        FuelText.text = "Fuel: " + fuelPercentage.ToString();

        if (fuelPercentage <= 20 && !isLowFuel)
        {
            agent.SetDestination(lowFuelWaypoint.position);
            isLowFuel = true;
        }

        if (fuelPercentage <= 0 && !boolcops2)
        {
            agent.isStopped = true;
        }


        if (fuelPercentage > 0 && !boolcops2 && !boolfuel)
        {
            agent.isStopped = false;
        }

        if (fuelPercentage <= 20 && isLowFuel && agent.remainingDistance < agent.stoppingDistance &&
            (xpos >= coordfuelminx && xpos <= coordfuelmaxx) &&
            (zpos >= coordfuelminz && zpos <= coordfuelmaxz))
        {
            fuel = 5000;

            isLowFuel = false;
            boolfuel = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            fuel = 5000;
        }

        if (fuelPercentage > 20 && isLowFuel)
        {
            isLowFuel = false;
            MoveToNextWaypoint();
        }

        if (fuelPercentage <= 20)
        {
            statusFuelText.text = "Critical Fuel Level";
        }
        else
        {
            statusFuelText.text = " ";
        }

        if (boolcops && agent.remainingDistance < agent.stoppingDistance &&
            (xpos >= coordcopsminx && xpos <= coordcopsmaxx) &&
            (zpos >= coordcopsminz && zpos <= coordcopsmaxz))
        {
            boolcops2 = true;
            StartCoroutine(StopAndWait());

        }

    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
            boolcops = random.Next(2) == 0;
        }
            agent.SetDestination(waypoints[currentWaypointIndex].position);
            currentWaypointIndex++;
    }

    IEnumerator StopAndWait()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(stopTime);
        agent.isStopped = false;
        boolcops2 = false;
        boolfuel = false;
    }

    private void SetSignalColor(GameObject stoplight, string color)
    {
        Light[] lights = stoplight.GetComponentsInChildren<Light>();
        foreach (Light light in lights)
        {
            switch (color)
            {
                case "red":
                    light.color = UnityEngine.Color.red;
                    break;
                case "black":
                    light.color = UnityEngine.Color.black;
                    break;
            }
        }
    }
}
