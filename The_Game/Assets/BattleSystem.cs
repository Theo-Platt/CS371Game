using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, MIDDLE, PLAYERTURN, CALCULATING, ENEMYTURN, WIN, LOSS}
public class BattleSystem : MonoBehaviour
{
    //player and enemy Game Objects
    public GameObject player;
    public GameObject enemy;

    //spawn locations for player and enemy
    public Transform playerLocation;
    public Transform enemyLocation;

    //Unit objects for player and enemy
    Unit playerUnit;
    Unit enemyUnit;

    //Text for the Dialogue Box
    public Text dialogueText;
    public Text enchantText;
    public Text curseText;
    string curseType;
    string enchantType;
    
    //Modifier calculation for which curse/enchant is given.
    int enchantModifier;
    int curseModifier;

    //hepls keep track of middle
    bool playerFirst;
    bool enemyFirst;

    //the player and enemy UI HUDs
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    //Current Battle State
    public BattleState state;

    //sprite handling stuff
    GameObject spellSpriteAnimation;
    public GameObject fireAttackSprite;
    public GameObject EarthAttackSprite;
    public GameObject WaterAttackSprite;
    public GameObject AirAttackSprite;
    public GameObject enchantSprite;
    public GameObject curseSprite;
    
    // Start called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        // SetupBattle();
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGo  = Instantiate(player, playerLocation);
        GameObject enemyGo = Instantiate(enemy, enemyLocation);
        
        playerUnit = playerGo.GetComponent<Unit>();
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " appeared!";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        playerUnit.ensureStats();
        enemyUnit.ensureStats();
        yield return new WaitForSeconds(3f);
 
        state = BattleState.MIDDLE;
        Middle();
    }

    //
    //This function handles turn rotation
    //
    void Middle()
    {   

        if(playerUnit.speed > enemyUnit.speed)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn(true);
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn(true));   
        }
    }

    //
    //These functions handle player and enemy turns.
    //
    void PlayerTurn(bool wentFirst)
    {
        playerFirst = wentFirst;
        enchantModifier =Random.Range(0,6);
        switch(enchantModifier)
        {
            case 0:
                enchantText.text = "Enchant: Healing";
                enchantType = "Health";
                break;
            case 1:
                enchantText.text = "Enchant: Damage";
                enchantType = "Damage";
                break;
            case 2:
                enchantText.text = "Enchant: Defense";
                enchantType = "Defense";
                break;
            case 3:
                enchantText.text = "Enchant: Speed";
                enchantType = "Speed";
                break;
            case 4:
                enchantText.text = "Enchant: Accuracy";
                enchantType = "Accuracy";
                break;
            case 5:
                enchantText.text = "Enchant: Evasiveness";
                enchantType = "Evasiveness";
                break;
        }

        curseModifier =Random.Range(0,6);
        switch(curseModifier)
        {
            case 0:
                curseText.text = "Curse: Health";
                curseType = "Health";
                break;
            case 1:
                curseText.text = "Curse: Damage";
                curseType = "Damage";
                break;
            case 2:
                curseText.text = "Curse: Defense";
                curseType = "Defense";
                break;
            case 3:
                curseText.text = "Curse: Speed";
                curseType = "Speed";
                break;
            case 4:
                curseText.text = "Curse: Accuracy";
                curseType = "Accuracy";
                break;
            case 5:
                curseText.text = "Curse: Evasiveness";
                curseType = "Evasiveness";
                break;
        }

        dialogueText.text = "Choose an action: ";
        
    }

    IEnumerator EnemyTurn(bool wentFirst)
    {
        enemyFirst = wentFirst;
        int modifier =Random.Range(0,6);
        string enemyStatChoice = "";
        switch(modifier)
        {
            case 0:
                enemyStatChoice = "Health";
                break;
            case 1:
                enemyStatChoice = "Damage";
                break;
            case 2:
                enemyStatChoice = "Defense";
                break;
            case 3:
                enemyStatChoice = "Speed";
                break;
            case 4:
                enemyStatChoice = "Accuracy";
                break;
            case 5:
                enemyStatChoice = "Evasiveness";
                break;
        }
        int whatToDo =Random.Range(0,4);
        dialogueText.text = "It's " + enemyUnit.unitName +"'s turn!";
        yield return new WaitForSeconds(3f);
        bool hits = doesHit(enemyUnit.accuracy, playerUnit.evasiveness);
        bool isDead = false;
        GameObject sprite;
        switch(whatToDo)
        {
            //attack player
            case 0:
                dialogueText.text = enemyUnit.unitName + " attacks with a spell!";
                yield return new WaitForSeconds(3f);
                if(hits)
                {
                    //damage player
                    isDead = playerUnit.TakeDamage(enemyUnit.attack, enemyUnit.isEarthType, enemyUnit.isWaterType, enemyUnit.isFireType, enemyUnit.isAirType);
                    playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
                    dialogueText.text = "The attack hit!";
                    sprite = spellSprite(playerLocation, enemyUnit, 0);
                    yield return new WaitForSeconds(3f);
                    spellSpriteFinish(sprite);
                }else
                {
                    dialogueText.text = "The attack missed!";
                    yield return new WaitForSeconds(3f);
                }
                break;
                //attack player
            case 1:
                dialogueText.text = enemyUnit.unitName + " attacks with a spell!";
                yield return new WaitForSeconds(3f);
                if(hits)
                {
                    //damage player
                    isDead = playerUnit.TakeDamage(enemyUnit.attack, enemyUnit.isEarthType, enemyUnit.isWaterType, enemyUnit.isFireType, enemyUnit.isAirType);
                    playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
                    dialogueText.text = "The attack hit!";
                    sprite = spellSprite(playerLocation, enemyUnit, 0);
                    yield return new WaitForSeconds(3f);
                    spellSpriteFinish(sprite);
                }else
                {
                    dialogueText.text = "The attack missed!";
                    yield return new WaitForSeconds(3f);
                }
                break;
            //enchant self
            case 2:
                bool max = enemyUnit.enchant(modifier);
                enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);
                sprite = spellSprite(enemyLocation, enemyUnit, 1);
                if(max){
                    dialogueText.text = enemyUnit.unitName + " raised their "+enemyStatChoice+" it to max with an enchantment!";
                }else{
                    dialogueText.text = enemyUnit.unitName + " raised their "+enemyStatChoice+" with an enchantment!";
                }
                yield return new WaitForSeconds(3f);
                spellSpriteFinish(sprite);
                break;
            //curse player
            case 3:
                bool min = playerUnit.curse(modifier, enemyUnit.unitLevel);
                playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
                sprite = spellSprite(playerLocation, enemyUnit, 2);
                if(min){
                    dialogueText.text = enemyUnit.unitName + " cursed your "+enemyStatChoice+", lowering it to minimum!";
                }else{
                    dialogueText.text = enemyUnit.unitName + " cursed your "+enemyStatChoice+", lowering it!";
                }
                yield return new WaitForSeconds(3f);
                spellSpriteFinish(sprite);
                break;
        }
        
        //if player dead
        if(playerUnit.currentHP <= 0)
        {
            state = BattleState.LOSS;
            EndBattle();
        }
        else if(enemyFirst)
        {
            enemyFirst = false;
            state = BattleState.PLAYERTURN;
            PlayerTurn(false);
        }else
        {
            state = BattleState.MIDDLE;
            Middle();
        }
    }





    //
    // These functions all handle the player attacks/enchants/curses
    //

    //spell button press
    public void OnSpellButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;
        state = BattleState.CALCULATING;
        StartCoroutine(PlayerAttack());
    }

    //spell button resolution
    IEnumerator PlayerAttack()
    {
        //calculate if attack hits
        bool hits = doesHit(playerUnit.accuracy, enemyUnit.evasiveness);
        bool isDead = false;
        if(hits)
        {
            //damage enemy
            isDead = enemyUnit.TakeDamage(playerUnit.attack, playerUnit.isEarthType, playerUnit.isWaterType, playerUnit.isFireType, playerUnit.isAirType);
            enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);
            dialogueText.text = "The attack hit!";
            GameObject sprite = spellSprite(enemyLocation, playerUnit, 0);
            yield return new WaitForSeconds(3f);
           spellSpriteFinish(sprite);
        }else
        {
            dialogueText.text = "The attack missed!";
            yield return new WaitForSeconds(3f);
        }
        
        //if enemy dead
        if(isDead)
        {
            state = BattleState.WIN;
            EndBattle();
        }else if(playerFirst)
        {
            playerFirst = false;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn(false));
        }else
        {
            state = BattleState.MIDDLE;
            Middle();
        }
        //state handling

    }

    //Enchant button press
    public void OnEnchantButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;
        state = BattleState.CALCULATING;
        StartCoroutine(PlayerEnchant());
    }

    //Enchant button resolution
    IEnumerator PlayerEnchant()
    {
        //buff player stat
        bool max = playerUnit.enchant(enchantModifier);
        if(max){
            playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
            dialogueText.text = "You enchanted your " + enchantType + ", maxing it out!";
        }else{
            playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
            dialogueText.text = "You enchanted your " + enchantType + ", raising it!";
        }
        GameObject sprite = spellSprite(playerLocation, playerUnit, 1);
        yield return new WaitForSeconds(3f);
        spellSpriteFinish(sprite);
        //if stat max

        //state handling
        if(playerFirst)
        {
            playerFirst = false;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn(false));
        }else
        {
            state = BattleState.MIDDLE;
            Middle();
        }
    }

    //Curse button press
    public void OnCurseButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;
        state = BattleState.CALCULATING;
        StartCoroutine(PlayerCurse());
    }

    //Curse button resolution
    IEnumerator PlayerCurse()
    {
        //debuff enemy stat
        bool min = enemyUnit.curse(enchantModifier, playerUnit.unitLevel);
        if(min){
            enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);
            dialogueText.text = "You cursed " + enemyUnit.unitName + "'s " + curseType + ", lowering it completely!";
        }else{
            enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);
            dialogueText.text = "You cursed " + enemyUnit.unitName + "'s " + curseType + ", lowering it!";
        }
        GameObject sprite = spellSprite(enemyLocation, playerUnit, 2);
        yield return new WaitForSeconds(3f);
        spellSpriteFinish(sprite);
        //if stat max

        //state handling
        if(playerFirst)
        {
            playerFirst = false;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn(false));
        }else
        {
            state = BattleState.MIDDLE;
            Middle();
        }
    }




    bool doesHit(float accuracy, float evasiveness)
    {
        if(Random.Range(0, 101) <= accuracy && Random.Range(0,101) >= evasiveness)
            return true;
        return false;
    }







    //
    //end battle
    //
    void EndBattle()
    {
        if(state == BattleState.LOSS)
        {
            dialogueText.text = "You lost the battle...";
            //wait
            //decrease moneys or something
            //exits battle system into map or something
        }
        else if(state == BattleState.WIN)
        {
            dialogueText.text = "You won the battle!";
            //wait
            //increase xp/mones if needed or something
            //exits battle system into map or something
        }
    }


    //
    //attack/spell/enchant sprite handling (this doesnt work right)
    //

    public GameObject spellSprite(Transform targetLocation, Unit attacker, int action)
    {
        //if action is 0: spell
            if(action ==0){
            if(attacker.isFireType)
                spellSpriteAnimation = Instantiate(fireAttackSprite, targetLocation);
            else if(attacker.isEarthType)
                spellSpriteAnimation = Instantiate(EarthAttackSprite, targetLocation);
            else if(attacker.isWaterType)
                spellSpriteAnimation = Instantiate(WaterAttackSprite, targetLocation);
            else if(attacker.isAirType)
                spellSpriteAnimation = Instantiate(AirAttackSprite, targetLocation);
        }
        //if action is 1: enchant
        else if(action ==1){
            spellSpriteAnimation = Instantiate(enchantSprite, targetLocation);
        }
        //if action is 2: curse
        else{
            spellSpriteAnimation = Instantiate(curseSprite, targetLocation);
        }
        return spellSpriteAnimation;
    }

    void spellSpriteFinish(GameObject _spellSpriteAnimation)
    {
        Destroy(_spellSpriteAnimation);
    }
}