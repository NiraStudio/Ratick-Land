using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
     void GetHit(float dmg);
}
public interface IAttackable
{
     void Attack();
}