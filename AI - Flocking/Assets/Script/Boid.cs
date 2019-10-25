using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public bool drawGizmos = false;

    [SerializeField] private float viewRadius;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;

    [SerializeField] private GameObject direction;
    private Vector3 alignment = Vector3.zero;
    private Vector3 cohesion = Vector3.zero;
    private Vector3 separation = Vector3.zero;

    private Vector3 newDir = Vector3.zero;

    private Collider[] boidsInView;
    [SerializeField] private LayerMask boidLayerMask;

    public Boid(float viewRadius, float rotationSpeed, float movementSpeed, LayerMask boidLayerMask, Vector3 direction)
    {
        this.viewRadius = viewRadius;
        this.rotationSpeed = rotationSpeed;
        this.movementSpeed = movementSpeed;
        this.boidLayerMask = boidLayerMask;
    }

    private void Update()
    {
        boidsInView = Physics.OverlapSphere(transform.position, viewRadius, boidLayerMask);

        CheckAlignment();
        CheckCohesion();
        CheckSeparation();

        ResolveFlockingDirection();

        Move();
    }

    private void CheckAlignment()
    {
        foreach (Collider obj in boidsInView)
        {
            alignment += obj.transform.forward;
        }

        alignment = alignment.normalized;
    }

    private void CheckCohesion()
    {
        foreach (Collider obj in boidsInView)
        {
            cohesion += obj.transform.position;
        }

        cohesion = (cohesion.normalized + transform.position).normalized;
    }
    
    private void CheckSeparation()
    {
        foreach (Collider obj in boidsInView)
        {
            separation += (obj.transform.position - transform.position);
        }

        separation /= boidsInView.Length;
        separation *= -1;
        separation.Normalize();
    }

    private void ResolveFlockingDirection()
    {
        newDir = (alignment + cohesion + separation + (direction.transform.position - transform.position)).normalized;

        transform.forward = Vector3.Lerp(transform.forward, newDir, rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, viewRadius);
        }
    }
}
