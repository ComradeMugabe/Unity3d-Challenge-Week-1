using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetHandler : MonoBehaviour
{
    public Killable LastTarget
    {
        get 
        {
            if (_targets.Count == 0)
                return null;
            return _targets[_targets.Count - 1];
        }
    }

    public Killable CurrentTarget
    {
        get
        {
            if (_targets.Count == 0)
                return null;
            return _targets[0];
        }
    }

    private List<Killable> _targets;

    public TargetHandler()
    {
        _targets = new List<Killable>();
    }

    public void Update()
    {
        IsCurrentTargetValid();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Enemy") return;
        AddGameObjectIfTarget(col.gameObject);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != "Enemy") return;
        RemoveGameObjectIfTarget(col.gameObject);
    }

    private void AddGameObjectIfTarget(GameObject target)
    {
        var killable = target.GetComponent<Killable>();
        if (killable == null) return;

        _targets.Add(killable);
    }

    private void RemoveGameObjectIfTarget(GameObject target)
    {
        var killable = target.GetComponent<Killable>();
        if (killable == null) return;

        RemoveTarget(killable);
    }

    private void RemoveTarget(Killable target)
    {
        _targets.Remove(target);
    }

    private void IsCurrentTargetValid()
    {
        if (CurrentTarget != null && CurrentTarget.IsDead)
            RemoveTarget(CurrentTarget);
    }
}
