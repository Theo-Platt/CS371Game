using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName; //name of the unit
    public int unitLevel; //level of the unit

    public int attack; //base attack damage of the unit
    public int defense; // (00, 80)            //base defense of the unit

    public int speed; //base speed of the unit

    public int accuracy; //helps calculate chance to hit
    public int evasiveness; //helps calculate chance to hit

    public bool isEarthType; //Unit is or is not Earth type
    public bool isWaterType; //Unit is or is not Water type
    public bool isAirType; //Unit is or is not Air type
    public bool isFireType; //Unit is or is not Fire type

}