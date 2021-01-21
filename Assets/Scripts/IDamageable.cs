using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 interface IDamageable
{

     int Health
    { get; set; }
    int Power
    { get; set; }

    
     void Damage(int damage);

    


}
