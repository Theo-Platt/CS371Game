using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherites base character class
public class BaseAir : BaseCharacter
{

    public void Air()
    {
        CharacterElementType = "Air";
        CharacterDescription = "it's not lit.";
        level = 1;
        hp = 10; 
        attack = 2;
        defense = 4;
        speed = 5;
        accuracy = 5;
    }//end of Air

}//end of BaseAir