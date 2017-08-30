//Handles bullets created by turrets.
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    public float speed = 50f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public void Seek (Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        //If there is no target, then the bullet should be removed (useful for when a target is destroyed by another turret).
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        //Moves bullet and rotates it assuming that the target has not been hit.
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }


    void HitTarget()
    {
        //Impact effect for when the bullet hits a target, and how long the effect should last.
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        //If a turret has splash damage, then use explode, else destroy targer (need a damage function).
        if (explosionRadius > 0f)
        {
            Explode();
        } else
        {
            Damage(target);
        }

        //Remove bullet
        Destroy(gameObject);
    }

    //Gathers colliders with the enemy tag to damage them.
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    //Placeholder damage function.
    void Damage (Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    //Used to see the size of an explosion in the scene.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
