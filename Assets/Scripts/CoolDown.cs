using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : MonoBehaviour
{
   public Animator _coolDown;
   public void Cooldown()
   {
      _coolDown.SetTrigger("CoolDown");
   }
}
