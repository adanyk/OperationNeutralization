using System;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public float timeBetweenShooting, range;
    bool readyToShoot = true;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    private float zOffset = 0f;
    private const float zOffsetStep = 0.001f;

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (readyToShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit rayHit, range, whatIsEnemy))
        {
            Quaternion hitRotation = Quaternion.LookRotation(rayHit.normal);

            GameObject bulletHoleInstance = Instantiate(bulletHoleGraphic, rayHit.point, hitRotation);
            bulletHoleInstance.transform.position += bulletHoleInstance.transform.forward * 0.01f + fpsCam.transform.forward * zOffset;
            bulletHoleInstance.transform.SetParent(rayHit.collider.transform);

            zOffset -= zOffsetStep;

            AffectHitObject(rayHit.collider.gameObject);
        }

        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Invoke(nameof(ResetShot), timeBetweenShooting);
    }

    private void AffectHitObject(GameObject hitObject)
    {
        if (Enum.TryParse(hitObject.tag, true, out Walls _))
        {
            hitObject.GetComponentInParent<Room>().HitWall(hitObject);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
}
