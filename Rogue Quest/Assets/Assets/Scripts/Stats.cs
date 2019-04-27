using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string Name;

    public float Life;
    private float TemporaryLife;
    private float TemporaryLifeDuration;

    public float RecoverLifeSpeed;
    private float TemporaryRecoverLifeSpeed;
    private float TemporaryRecoverLifeSpeedDuration;

    public float Mana;
    private float TemporaryMana;
    private float TemporaryManaDuration;

    public float RecoverManaSpeed;
    private float TemporaryRecoverManaSpeed;
    private float TemporaryRecoverManaSpeedDuration;

    public float Speed;
    private float TemporarySpeed;
    private float TemporarySpeedDuration;

    public float JumpSpeed;
    private float TemporaryJumpSpeed;
    private float TemporaryJumpSpeedDuration;

    public float ClimbSpeed;
    private float TemporaryClimbSpeed;
    private float TemporaryClimbSpeedDuration;

    public float Strength;
    private float TemporaryStrength;
    private float TemporaryStrengthDuration;

    public float Defense;
    private float TemporaryDefense;
    private float TemporaryDefenseDuration;

    public float Intelligence;
    private float TemporaryIntelligence;
    private float TemporaryIntelligenceDuration;

    public float IntelligenceResistance;
    private float TemporaryIntelligenceResistance;
    private float TemporaryIntelligenceResistanceDuration;

    public float LavaResistance;
    private float TemporaryLavaResistance;
    private float TemporaryLavaResistanceDuration;

    public float PoisonResistance;
    private float TemporaryPoisonResistance;
    private float TemporaryPoisonResistanceDuration;

    public float WaterResistance;
    private float TemporaryWaterResistance;
    private float TemporaryWaterResistanceDuration;

    public void GetDamage(float damage)
    {
        var rawDamage = damage - GetDefense();

        if (rawDamage <= 0)
        {
            BroadcastMessage("ShowInfo", "Defended!", SendMessageOptions.DontRequireReceiver);
            return;
        }

        var rest = TemporaryLife - rawDamage;
        TemporaryLife -= rawDamage;

        rest = rest < 0 ? rest * -1f : rest;

        if (rest <= 0)
        {
            BroadcastMessage("ShowInfo", "Defended!", SendMessageOptions.DontRequireReceiver);
            return;
        }

        Life -= Mathf.Abs(rest);

        BroadcastMessage("ShowInfo", $"-{rest} LIFE", SendMessageOptions.DontRequireReceiver);
        BroadcastMessage("GotDamageFX", SendMessageOptions.DontRequireReceiver);

        if (Life < 0)
        {
            Life = 0;
        }
    }

    public float GetLife()
    {
        return Life + TemporaryLife;
    }

    public float GetRecoverLifeSpeed()
    {
        return RecoverLifeSpeed + TemporaryRecoverLifeSpeed;
    }

    public float GetMana()
    {
        return Mana + TemporaryMana;
    }

    public float GetRecoverManaSpeed()
    {
        return RecoverManaSpeed + TemporaryRecoverManaSpeed;
    }

    public float GetSpeed()
    {
        return Speed + TemporarySpeed;
    }

    public float GetJumpSpeed()
    {
        return JumpSpeed + TemporaryJumpSpeed;
    }

    public float GetClimbSpeed()
    {
        return ClimbSpeed + TemporaryClimbSpeed;
    }

    public float GetStrength()
    {
        return Strength + TemporaryStrength;
    }

    public float GetDefense()
    {
        return Defense + TemporaryDefense;
    }

    public float GetIntelligence()
    {
        return Intelligence + TemporaryIntelligence;
    }

    public float GetIntelligenceResistance()
    {
        return IntelligenceResistance + TemporaryIntelligenceResistance;
    }

    public float GetLavaResistance()
    {
        return LavaResistance + TemporaryLavaResistance;
    }

    public float GetPoisonResistance()
    {
        return PoisonResistance + TemporaryPoisonResistance;
    }

    public float GetWaterResistance()
    {
        return WaterResistance + TemporaryWaterResistance;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDurations();
    }

    void UpdateDurations()
    {
        if (TemporaryLifeDuration > 0)
        {
            TemporaryLifeDuration--;
        }
        else
        {
            TemporaryLife = 0;
        }

        if (TemporaryRecoverLifeSpeedDuration > 0)
        {
            TemporaryRecoverLifeSpeedDuration--;
        }
        else
        {
            TemporaryRecoverLifeSpeed = 0;
        }

        if (TemporaryManaDuration > 0)
        {
            TemporaryManaDuration--;
        }
        else
        {
            TemporaryMana = 0;
        }

        if (TemporaryRecoverManaSpeedDuration > 0)
        {
            TemporaryRecoverManaSpeedDuration--;
        }
        else
        {
            TemporaryRecoverManaSpeed = 0;
        }

        if (TemporarySpeedDuration > 0)
        {
            TemporarySpeedDuration--;
        }
        else
        {
            TemporarySpeed = 0;
        }

        if (TemporaryJumpSpeedDuration > 0)
        {
            TemporaryJumpSpeedDuration--;
        }
        else
        {
            TemporaryJumpSpeed = 0;
        }

        if (TemporaryLifeDuration > 0)
        {
            TemporaryLifeDuration--;
        }
        else
        {
            TemporaryLife = 0;
        }

        if (TemporaryClimbSpeedDuration > 0)
        {
            TemporaryClimbSpeedDuration--;
        }
        else
        {
            TemporaryClimbSpeed = 0;
        }

        if (TemporaryStrengthDuration > 0)
        {
            TemporaryStrengthDuration--;
        }
        else
        {
            TemporaryStrength = 0;
        }

        if (TemporaryDefenseDuration > 0)
        {
            TemporaryDefenseDuration--;
        }
        else
        {
            TemporaryDefense = 0;
        }

        if (TemporaryIntelligenceDuration > 0)
        {
            TemporaryIntelligenceDuration--;
        }
        else
        {
            TemporaryIntelligence = 0;
        }

        if (TemporaryIntelligenceResistanceDuration > 0)
        {
            TemporaryIntelligenceResistanceDuration--;
        }
        else
        {
            TemporaryIntelligenceResistance = 0;
        }

        if (TemporaryLavaResistanceDuration > 0)
        {
            TemporaryLavaResistanceDuration--;
        }
        else
        {
            TemporaryLavaResistance = 0;
        }

        if (TemporaryPoisonResistanceDuration > 0)
        {
            TemporaryPoisonResistanceDuration--;
        }
        else
        {
            TemporaryPoisonResistance = 0;
        }

        if (TemporaryWaterResistanceDuration > 0)
        {
            TemporaryWaterResistanceDuration--;
        }
        else
        {
            TemporaryWaterResistance = 0;
        }

    }
}
