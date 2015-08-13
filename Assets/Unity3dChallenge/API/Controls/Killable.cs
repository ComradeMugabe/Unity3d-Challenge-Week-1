using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Killable : MonoBehaviour 
{
    public float HitPoints = 10;
    public UnityEvent DeathEvent;
    public bool IsDead
    {
        get { return HitPoints < 0; }
    }
	
	void Update () 
    {
        if (IsDead)
            KillSelf();
	}

    public void DoDamage(float dmg)
    {
        HitPoints -= dmg;
    }

    private void KillSelf()
    {
        if (DeathEvent == null) return;
        DeathEvent.Invoke();
    }
}