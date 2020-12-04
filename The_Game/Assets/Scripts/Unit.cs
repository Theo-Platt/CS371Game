using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName; //name of the unit
    public int unitLevel; //level of the unit

    public float maxHP;//max hp of the unit
    public float currentHP; //current hp of the unit    (Min: 0)  (Max: maxHP)

    public float attack; //base attack damage of the unit    (Min: 5 )  (Max: 100 + (level * 2.5) )
    public float defense; //base defense of the unit    (Min: 0)  (Max: 80)

    public int speed; //base speed of the unit    (Min: 0)  (Max: 20)

    public float accuracy; //helps calculate chance to hit    (Min: 20)  (Max: 80)
    public float evasiveness; //helps calculate chance to hit    (Min: 20)  (Max: 80)

    public enum typeList {fire, water, earth, air}
    public typeList type;
    // public bool isEarthType; //Unit is or is not Earth type
    // public bool isWaterType; //Unit is or is not Water type
    // public bool isAirType; //Unit is or is not Air type
    // public bool isFireType; //Unit is or is not Fire type


    public bool TakeDamage(Unit attacker)
    {
        bool weakness = isWeak(attacker);
        bool resist = isResist(attacker);
        float damageModifier = (float)1.0;
        if(weakness)
            damageModifier += (float)0.25;
        if(resist)
            damageModifier -= (float)0.25;
        
        currentHP -= (attacker.attack-((attacker.attack*defense) / 100)) * damageModifier;

        if(currentHP <= 0)
            return true;
        return false;
    }

    private bool isWeak(Unit attacker)
    {
        if(type == typeList.fire)
            if(attacker.type == typeList.water)
                return true;
        
        if(type == typeList.water)
            if(attacker.type == typeList.earth)
                return true;
        
        if(type == typeList.earth)
            if(attacker.type == typeList.air)
                return true;
        
        if(type == typeList.air)
            if(attacker.type == typeList.fire)
                return true;
        
        return false;
    }

    private bool isResist(Unit attacker)
    {
        if(type == typeList.fire)
            if(attacker.type == typeList.air)
                return true;
        
        if(type == typeList.water)
            if(attacker.type == typeList.fire)
                return true;
        
        if(type == typeList.earth)
            if(attacker.type == typeList.water)
                return true;
        
        if(type == typeList.air)
            if(attacker.type == typeList.earth)
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
    public bool curse(int stat, Unit attacker)
    {
        //0: heal (Min: 1)
        if(stat == 0)
        {
            currentHP -= attacker.unitLevel + (float)2.5;
            if(currentHP <= 1)
            {
                currentHP = 1;
                return true;
            }
        }
        
        //1: attack (Min: 5 )
        if(stat == 1)
        {
            attack -= 10 + attacker.unitLevel;
            if(attack <= 5)
            {
                attack = 5;
                return true;
            }
        }
    
        //2: defense (Min: 0)
        if(stat == 2)
        {
            defense -= 10 + attacker.unitLevel;
            if(defense <= 0)
            {
                defense = 0;
                return true;
            }
        }

        //3: speed (Min: 0)
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
            accuracy -= attacker.unitLevel + 5;
            if(accuracy <= 20)
            {
                accuracy = 20;
                return true;
            }
        }

        //5: evasiveness (Min: 20)
        if(stat == 5)
        {
            evasiveness -= attacker.unitLevel + 5;
            if(evasiveness <= 20)
            {
                evasiveness = 20;
                return true;
            }
        }
        return false;
    }

    //ensure the minimum and maximum stats are not out of bounds.
    public void ensureStats()
    {   
        
        if(currentHP > maxHP)
            currentHP=maxHP;
        else if(currentHP < 0)
            //you shouldnt ever make it here, if somehow the code reaches this point, there are bigger problems somewhere else...
        
        if(attack > 100 + (unitLevel * 2.5))
            attack = (float)(100 + (unitLevel * 2.5));
        else if(attack < 5)
            attack = 5;

        if(defense > 80)
            defense = 80;
        else if(defense < 0)
            defense = 0;

        if(speed > 20)
            speed = 20;
        else if( speed < 0)
            speed = 0;

        if(accuracy > 80)
            accuracy = 80;
        else if(accuracy < 20)
            accuracy = 20;

        if(evasiveness > 80)
            evasiveness = 80;
        else if(evasiveness < 20)
            evasiveness = 20;
        

    }
}