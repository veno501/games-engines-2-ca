using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveAndFlee : MonoBehaviour
{
    Arrive arrive;
    Flee flee;
    Boid boid;

    void Start() {
        arrive = GetComponent<Arrive>();
        flee = GetComponent<Flee>();
        boid = GetComponent<Boid>();
    }

    void Update() {
        if (gameObject.name[0] == 'h' && arrive.targetGameObject) {
            if (arrive.weight == 1f && Vector3.Distance(arrive.targetPosition, transform.position) <= 15f) {
                arrive.weight = 0f;
                flee.weight = 1f;
                boid.mass /= 2f;
                flee.targetGameObject = arrive.targetGameObject;
            }
            else if (flee.weight == 1f && Vector3.Distance(arrive.targetPosition, transform.position) >= 30f) {
                arrive.weight = 1f;
                flee.weight = 0f;
                boid.mass *= 2f;
            }
        }
        Vector3 bounds = new Vector3(0,0,350)-transform.position;
        boid.velocity += bounds.normalized*bounds.sqrMagnitude/(450*450);
    }
}
