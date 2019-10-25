using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    [SerializeField] private int initalAmount;

    [SerializeField] private float viewRadius;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private LayerMask boidLayerMask;

    public Boid[] boids;
}
