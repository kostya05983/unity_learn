﻿using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float gridSize = 10f;

    void Update()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.y = Mathf.RoundToInt(transform.position.y / gridSize) * gridSize;

        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;


        transform.position = new Vector3(snapPos.x, snapPos.y, snapPos.z);
    }
}