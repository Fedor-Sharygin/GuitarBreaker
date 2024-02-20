using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowTick : MonoBehaviour, IMusicTick
{
    private void Update()
    {
        Move();
    }

    [SerializeField]
    private float m_Speed;
    public void Move()
    {
        transform.localPosition += m_Speed * Time.deltaTime * Vector3.down;
    }
}
