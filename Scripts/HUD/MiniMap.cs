using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject Map;
    private bool mapOpen=false;

    void Start()
    {
        Map.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)&&mapOpen==false)
        {
            Map.SetActive(true);
            mapOpen=true;
        }
        else if(Input.GetKeyDown(KeyCode.M)&&mapOpen==true)
        {
            Map.SetActive(false);
            mapOpen=false;
        }

       MapPosition();
    }

    void MapPosition(){
        Vector3 newPos = PlayerTransform.position;
        newPos.y=transform.position.y;
        transform.position=newPos;
    }
}
