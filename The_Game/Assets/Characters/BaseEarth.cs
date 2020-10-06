using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherites base character class
public class BaseEarth : BaseCharacter
{

    public void Earth()
    {
        CharacterElementType = "Earth";
        CharacterDescription = "it rocks.";
        level = 1;
        hp = 10; 
        attack = 2;
        defense = 4;
        speed = 5;
        accuracy = 5;
    }//end of Earth

}//end of BaseEarth