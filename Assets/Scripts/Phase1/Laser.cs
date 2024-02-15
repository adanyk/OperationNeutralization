using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _laserBeam;
    [SerializeField] private Transform _laserOrigin;
    [SerializeField] private float _maxLength;
    private readonly float maxRotateSpeed = 3.5f;

    private float verticalRotation = 0;
    private float horizontalRotation = 0;
    public int minVerticalAngle = -50;
    public int maxVerticalAngle = 18;
    public int maxHorizontalAngle = 50;

    public static Ray laserRay;
    public static Vector3 laserEndPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateLaser();
        UpdateLaserBeam();
    }

    private void RotateLaser()
    {
        var rotateSpeed = CalculateAdjustedSpeed();

        horizontalRotation += Input.GetAxis("Mouse X") * rotateSpeed;
        verticalRotation -= Input.GetAxis("Mouse Y") * rotateSpeed;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -maxHorizontalAngle, maxHorizontalAngle);
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }

    private float CalculateAdjustedSpeed()
    {
        float verticalFactor = Mathf.Cos(verticalRotation / (maxVerticalAngle - minVerticalAngle));
        float horizontalFactor = Mathf.Cos(horizontalRotation / maxHorizontalAngle);
        return maxRotateSpeed * verticalFactor * horizontalFactor;
    }

    private void UpdateLaserBeam()
    {
        laserEndPosition = CalculateLaserEndPoint();
        SetLaserBeamPositions(_laserOrigin.position, laserEndPosition);
    }

    private Vector3 CalculateLaserEndPoint()
    {
        laserRay = new(_laserOrigin.position, _laserOrigin.forward);
        bool isHit = Physics.Raycast(laserRay, out var hit, _maxLength);
        return isHit ? hit.point : _laserOrigin.position + _laserOrigin.forward * _maxLength;
    }

    private void SetLaserBeamPositions(Vector3 startPosition, Vector3 endPosition)
    {
        _laserBeam.SetPosition(0, startPosition);
        _laserBeam.SetPosition(1, endPosition);
    }
}
