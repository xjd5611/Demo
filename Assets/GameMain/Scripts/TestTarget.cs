using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour
{
    public GameObject arrow;
    public GameObject target;
    public GameObject card;

    public GameObject pointPrefab;
    public Transform pointContent;
    public List<GameObject> points;
    


    private void Start()
    {
        points = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject point = Instantiate(pointPrefab, pointContent);
            points.Add(point);
        }
    }

    private void Update()
    {
        for (int i = 0; i < pointContent.childCount; i++)
        {
            pointContent.GetChild(i).transform.position = quardaticBezier(i * 1.0f / 10);
        }
    }

    private Vector3 PointPos(float t,Vector3 card,Vector3 target)
    {
        Vector3 rightAngle = new Vector3(card.x, target.y, card.z);

        Vector3 a = card + (rightAngle - card) * t;
        Vector3 b = rightAngle + (target - rightAngle) * t;
        return b + (a - b) * t;
    }

    public Vector3 quardaticBezier(float t)
    {
        Vector3 a = card.transform.position;
        Vector3 c = target.transform.position;
        Vector3 b = new Vector3(a.x, c.y, a.z);

        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        return aa + (bb - aa) * t;
    }

}
