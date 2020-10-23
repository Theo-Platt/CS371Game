using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName; //name of the unit
    public int unitLevel; //level of the unit

    public float maxHP;
    public float currentHP;

    public float attack; //base attack damage of the unit
    public float defense; //base defense of the unit

    public int speed; //base speed of the unit

    public float accuracy; //helps calculate chance to hit
    public float evasiveness; //helps calculate chance to hit

    public bool isEarthType; //Unit is or is not Earth type
    public bool isWaterType; //Unit is or is not Water type
    public bool isAirType; //Unit is or is not Air type
    public bool isFireType; //Unit is or is not Fire type


    public bool TakeDamage(float dmg, bool earth, bool water, bool fire, bool air)
    {
        bool weakness = isWeak(earth,water,fire,air);
        bool resist = isResist(earth,water,fire,air);
        float damageModifier = (float)1.0;
        if(weakness)
            damageModifier += (float)0.25;
        if(resist)
            damageModifier -= (float)0.25;
        
        currentHP -= (dmg-((dmg*defense) / 100)) * damageModifier;

        if(currentHP <= 0)
            return true;
        return false;
    }

    private bool isWeak(bool earth, bool water, bool fire, bool air)
    {
        if(isFireType)
            if(water)
                return true;
        
        if(isWaterType)
            if(earth)
                return true;
        
        if(isEarthType)
            if(air)
                return true;
        
        if(isAirType)
            if(fire)
                return true;
        
        return false;
    }

    private bool isResist(bool earth, bool water, bool fire, bool air)
    {
        if(isFireType)
            if(air)
                return true;
        
        if(isWaterType)
            if(fire)
                return true;
        
        if(isEarthType)
            if(water)
                return true;
        
        if(isAirType)
            if(earth)
                return true;
        
        return false;
    }


    // if true: stat max has been met
    public bool enchant(int stat)
    {
        //0: heal (Max: maxHP)
        if(stat == 0)
        {
            currentHP += maxHP * (float)0.15;
            if(currentHP >= maxHP)
            {
                currentHP = maxHP;
                return true;
            }
        }
        
        //1: attack (Max: 100 + (level * 2.5) )
        if(stat == 1)
        {
            attack += 10 + unitLevel;
            if(attack >= (100 + (unitLevel * (float)2.5)))
            {
                attack = (100 + (unitLevel * (float)2.5));
                return true;
            }
        }
    
        //2: defense (Max: 80)
        if(stat == 2)
        {
            defense += 10 + unitLevel;
            if(defense >= 80)
            {
                defense = 80;
                return true;
            }
        }

        //3: speed (Max: 20)
        if(stat == 3)
        {
            speed += 3;
            if(speed >= 20)
            {
                speed = 20;
                return true;
            }
        }

        //4: accuracy (Max: 80)
        if(stat == 4)
        {
            accuracy += unitLevel + 5;
            if(accuracy >= 80)
            {
                accuracy = 80;
                return true;
            }
        }

        //5: evasiveness (Max: 80)
        if(stat == 5)
        {
            evasiveness += unitLevel + 5;
            if(evasiveness >= 80)
            {
                evasiveness = 80;
                return true;
            }
        }
        return false;
    }
    

    // if true: stat min has been met
    public bool curse(int stat, float level)
    {
        //0: heal (Min: 1)
        if(stat == 0)
        {
            currentHP -= level + (float)2.5;
            if(currentHP <= 1)
            {
                currentHP = 1;
                return true;
            }
        }
        
        //1: attack (Min: 5 )
        if(stat == 1)
        {
            attack -= 10 + level;
            if(attack <= 5)
            {
                attack = 5;
                return true;
            }
        }
    
        //2: defense (Min: 0)
        if(stat == 2)
        {
            defense -= 10 + level;
            if(defense <= 0)
            {
                defense = 0;
                return true;
            }
        }

        //3: speed (Max: 0)
        if(stat == 3)
        {
            speed -= 3;
            if(speed <= 0)
            {
                speed = 0;
                return true;
            }
        }

        //4: accuracy (Min: 20)
        if(stat == 4)
        {
            accuracy -= level + 5;
            if(accuracy <= 20)
            {
                accuracy = 20;
                return true;
            }
        }

        //5: evasiveness (Max: 20)
        if(stat == 5)
        {
            evasiveness -= level + 5;
            if(evasiveness <= 20)
            {
                evasiveness = 20;
                return true;
            }
        }
        return false;
    }


}