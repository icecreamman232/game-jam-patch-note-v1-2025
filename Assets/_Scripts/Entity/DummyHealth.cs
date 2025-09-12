using UnityEngine;

public class DummyHealth : Health
{
    protected override void Damage(float damage)
    {
        //base.Damage(damage);
    }

    protected override void AfterTakingDamage(float invulnerabilityTime)
    {
        //base.AfterTakingDamage(invulnerabilityTime);
    }
}
