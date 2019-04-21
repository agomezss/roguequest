using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public bool IsAnimating;

    public GameObject Player;
    public GameObject Target;


    public void DamageEffect()
    {

    }

    public void WaterEffect()
    {

    }

    public void PoisonEffect()
    {

    }

    public void NearDeathEffect()
    {

    }

    public void HealLifeEffect()
    {

    }

    public void HealManaEffect()
    {

    }

    public void SlowDownEffect()
    {

    }

    public void ThunderEffect()
    {

    }

    public void GetDark()
    {

    }

    public void GetNormal()
    {

    }

    public void Animate()
    {

    }

    public void ResumePlay()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        var newPosition = new Vector3(Target.transform.position.x, Target.transform.position.y, -100);
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.98f);
    }
}
