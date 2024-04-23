using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody2D bulletPrefab;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            //Raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 15f, Color.green, 10f);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);

            // move target on click
            if (hit.collider != null)
            {
                target.transform.position = new Vector3(hit.point.x, hit.point.y);
                Debug.Log($"hit point : ({hit.point.x}, {hit.point.y}) ");

                //Projectile Shoot
                Vector2 projectile = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);
                Rigidbody2D firedbullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                firedbullet.velocity = projectile;
            }
        }
    }

    //Projectile Math
    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float t)
    {
        Vector2 distance = target - origin;   //(x,y)

        float velocityX = distance.x / t;
        float velocityY = distance.y / t + 0.5f * Mathf.Abs(Physics2D.gravity.y * t);

        Vector2 result = new Vector2(velocityX, velocityY);

        return result;
        
    }
}
