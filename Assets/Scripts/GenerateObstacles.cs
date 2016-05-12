using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    [SerializeField]
    private Transform obstacle = null;

    [SerializeField]
    private Transform planet = null;

    [SerializeField]
    private LayerMask ground = 0;

    [SerializeField]
    private int numberOfObstacles = 3;

    [SerializeField]
    private int raysRadius = 20;

    private float maxSurfDiff = 30;

    // Use this for initialization
    private void Start()
    {
        for (int i = 0; i < numberOfObstacles; ++i)
        {
            float intervalLenght = 2 * Mathf.PI / numberOfObstacles;
            float arc = Random.Range(i * intervalLenght, (i + 1) * intervalLenght);
            Vector2 rayOrigin = new Vector2(Mathf.Cos(arc), Mathf.Sin(arc)) * raysRadius;
            RaycastHit2D[] hitInfos = Physics2D.RaycastAll(rayOrigin, -rayOrigin, raysRadius, ground);
            Vector2 surfaceNormal = hitInfos[Mathf.FloorToInt(Random.Range(0, hitInfos.Length))].normal;
            if (Mathf.Abs(Vector2.Angle(surfaceNormal, rayOrigin)) < maxSurfDiff)
            {
                Vector2 surfacePos = hitInfos[Mathf.FloorToInt(Random.Range(0, hitInfos.Length))].point;
                Transform obs = Instantiate(obstacle, surfacePos, Quaternion.identity) as Transform;
                obs.up = surfaceNormal;
                obs.SetParent(planet);
            }
            else
            {
                --i;
            }
        }
    }
}