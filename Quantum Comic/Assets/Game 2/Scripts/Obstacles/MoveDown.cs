using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 40.0f;

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}