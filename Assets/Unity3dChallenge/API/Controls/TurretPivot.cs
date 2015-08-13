using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(BulletHandler))]
public class TurretPivot : MonoBehaviour
{
    public float TurnSpeed = 5f;
    public float ReloadTime = 1f;
    public UnityEvent OnFire;

    private Quaternion _desiredRotation;
    private float _nextShootTime;
    private float _nextRandomRotateTime;
    private float _randomRotateInterval = 3.0f;
    private TargetHandler _targetHandler;
    private BulletHandler _bulletHandler;

    public void Start()
    {
        _nextShootTime = Time.time;
        _bulletHandler = GetComponent<BulletHandler>();
    }

    public void Update()
    {
        if (_targetHandler == null) return;
        if (_targetHandler.CurrentTarget == null)
        {
            ResetNextShootTime();
            ResetRandomRotateTime();
        }
        RotateTurret();
        ShootAtTarget();
    }

    public void AssignTargetHandler(TargetHandler targetHandler)
    {
        _targetHandler = targetHandler;
    }

	private void ResetNextShootTime()
    {
        if (Time.time > _nextShootTime)
            _nextShootTime = Time.time;
    }

    private void ResetRandomRotateTime()
    {
        if (Time.time < _nextRandomRotateTime) return;
        _nextRandomRotateTime = Time.time + _randomRotateInterval;
        var position = new Vector3(Random.Range(-10.0F, 10.0F), 0, Random.Range(-10.0F, 10.0F));
        _desiredRotation = GetDesiredRotation(position);
    }

    private void RotateTurret()
    {
        if (_targetHandler.CurrentTarget != null)
            _desiredRotation = GetDesiredRotation(_targetHandler.CurrentTarget.transform.position);
        UpdateRotation(_desiredRotation);
    }

    private Quaternion GetDesiredRotation(Vector3 target)
    {
        var pos_x = target.x - transform.position.x;
        var pos_z = target.z - transform.position.z;

        var aimPoint = new Vector3(pos_x, 0, pos_z);
        return Quaternion.LookRotation(aimPoint);
    }

    private void UpdateRotation(Quaternion desiredRotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * TurnSpeed);
    }

    private void ShootAtTarget()
    {
        if (_targetHandler.CurrentTarget == null) return;
        if (!CanFire())
        {
            ResetNextShootTime();
            return;
        }
        Fire();
        _nextShootTime += ReloadTime;
    }

    private bool CanFire()
    {
        if (Time.time < _nextShootTime) return false;
        if (!IsAimingAtTarget()) return false;
        return true;
    }

    private bool IsAimingAtTarget()
    {
        var difference = Quaternion.Angle(_desiredRotation, transform.rotation);
        if (difference > 5) return false;
        return true;
    }

    private void Fire()
    {
        _nextRandomRotateTime = Time.time + _randomRotateInterval;
        _bulletHandler.Fire();
        if (OnFire != null)
            OnFire.Invoke();
    }
}
