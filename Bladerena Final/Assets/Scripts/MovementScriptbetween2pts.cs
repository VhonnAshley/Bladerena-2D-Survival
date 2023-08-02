using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementScriptbetween2pts : Damage
{
    [Range(0,5)]
    public float objSpeed;


    [Range(0, 5)]
    public float waitDuration;

    int speedMultiplier = 1;

    Vector3 targetPos;

    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;


    private void Awake()
    {
        
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;
    }

    private void Update()
    {
        
        var step = speedMultiplier*objSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        if (transform.position == targetPos)
        {
            AudioManager.Instance.PlaySFX("saw");
            NextPoint();

        }
    }

    void NextPoint() { 
        if (pointIndex == pointCount - 1) //Arrive at the last point
        {
            direction = -1;

        }
        
        if (pointIndex == 0) //Arrived first point
        {
            direction = 1;
        }

        pointIndex = pointIndex + direction;
        targetPos = wayPoints[pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;

    }

 

}
