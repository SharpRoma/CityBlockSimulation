using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static CarMovement;

public class triggerlogic : MonoBehaviour
{
    public GameObject trafficLight;
    private GameObject car;
    private bool isontrigger = false;
    public Vector3 direction;
    private Vector3 fuelStation;
    public Transform lowFuelWaypoint;
    void Start()
    {
        car = GameObject.Find("car");
    }

    void Update()
    {
        if (isontrigger)
        {
            if (trafficLight.GetComponentInChildren<Light>().color == Color.red)
            {
                car.GetComponent<NavMeshAgent>().enabled = false;
            }
            if (trafficLight.GetComponentInChildren<Light>().color == Color.yellow)
            {
                car.GetComponent<NavMeshAgent>().enabled = false;
            }
            if (trafficLight.GetComponentInChildren<Light>().color == Color.green)
            {
                car.GetComponent<NavMeshAgent>().enabled = true;
                if (fuelPercentage <= 20 && !isLowFuel)
                {
                    agent.SetDestination(lowFuelWaypoint.position);
                    isLowFuel = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == car)
        {
            if (Vector3.Angle(car.GetComponent<NavMeshAgent>().velocity.normalized, direction) < 65)
            {
                isontrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isontrigger = false;
    }
}
