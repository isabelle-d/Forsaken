using UnityEngine;

public class Player_Ranged : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;

    protected override void Init()
    {
        weilder = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 shootDir = (mousePos - firePoint.position).normalized;

        GameObject bulletObj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        if (bulletObj.TryGetComponent(out Bullet bullet))
        {
            bullet.Initialize(shootDir, bulletSpeed);
        }
    }
}