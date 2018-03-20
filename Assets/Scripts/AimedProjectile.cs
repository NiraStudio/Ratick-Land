﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedProjectile : MonoBehaviour, IHitable
{
    public float speed,diffrenceAngular;

    float orginalRange, yRange, damage;
    GameObject target;
    bool shoot;
    Vector2 t;
    // Use this for initialization
    void Start()
    {
        t = target.transform.position - transform.position;
        t.Normalize();


        yRange = Mathf.Lerp(yRange, 0, 4 * Time.deltaTime);
        speed += 2 * Time.deltaTime;

        var dir = target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= diffrenceAngular;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        yRange = speed * 2;
        orginalRange = yRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (target==null||target.activeInHierarchy==false)
        {
            Destroy(gameObject);
            return;
        }


        t = target.transform.position - transform.position;
        t.Normalize();


        yRange = Mathf.Lerp(yRange, 0, 4 * Time.deltaTime);
        speed += 2 * Time.deltaTime;

        var dir = target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= diffrenceAngular;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector2 b = t * speed * Time.deltaTime;
        b.y += yRange * Time.deltaTime;

        transform.position = (Vector2)transform.position + b;
        if (Vector2.Distance(transform.position, target.transform.position) < 0.3f)
            Die();

    }
    public void Spawn(GameObject t, float d)
    {
        target = t;
        damage = d;
        speed = Vector2.Distance(transform.position, target.transform.position);
        shoot = true;
    }

    public void GetHit(float dmg)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        target.SendMessage("GetHit", damage);
        Destroy(gameObject);
    }
}

