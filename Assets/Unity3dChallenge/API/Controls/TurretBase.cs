using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TargetHandler))]
public class TurretBase : MonoBehaviour 
{
    public Transform TurretSpawn;

    public List<TurretPivot> Turrets;

    private TargetHandler _targetHandler;
    private TurretPivot _currentTurret;

    void Start()
    {
        _targetHandler = gameObject.GetComponent<TargetHandler>();
        GetNextTurret();
    }

    public void Upgrade()
    {
        GetNextTurret();
    }

    private void GetNextTurret()
    {
        if (Turrets.Count == 0) return;
        if (_currentTurret != null)
            Destroy(_currentTurret.gameObject);
        _currentTurret = Instantiate(Turrets[0]);
        _currentTurret.gameObject.transform.SetParent(transform);
        _currentTurret.gameObject.transform.position = TurretSpawn.position;
        _currentTurret.AssignTargetHandler(_targetHandler);
        Turrets.Remove(Turrets[0]);
    }
}
