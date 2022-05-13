using GameFramework.Event;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;

[ExecuteInEditMode]
public class RingLayout : MonoBehaviour
{
    public float radius = 5f;
    public float angleStep = 10f;

    public bool childSizeControl = false;
    public float childHeight = 100f;
    public float childWight = 100f;

    private void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            float angle = -angleStep * (transform.childCount - 1) + i * 2 * angleStep;
            child.transform.position = transform.position +
                new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * radius, Mathf.Cos(angle * Mathf.Deg2Rad) * radius - radius, -100);
            child.transform.rotation = Quaternion.Euler(0, 0, -angle);
            if(childSizeControl)
                child.GetComponent<RectTransform>().sizeDelta = new Vector2(childWight, childHeight);
        }
    }

}
