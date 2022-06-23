using UnityEngine;

public class Constants : MonoBehaviour
{
    //TAGs
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_PLAYER = "Player";
    public const string TAG_BOSS = "Boss";

    //INPUTs
    public const string INPUT_MOUSE_LEFT = "Fire1";
    
    //ANIMATORs
    public const string ANIMATOR_ATTACKING = "Attacking";
    public const string ANIMATOR_RUNNING = "Running";
    public const string ANIMATOR_DIE = "Die";

    //OTHERs
    public const int RAY_LENGTH = 100;

    //GAME PAUSE/RESUME
    public const int GAME_RESUME = 1;
    public const int GAME_PAUSE = 0;
    
    //Player Weapon
    public const int BULLET_DAMAGE_IN_ZOMBIE = 1;
    
    //Enemys Generate 
    public const int ENEMY_GENERATE_RADIO_TO_GENERATE_RANDOM_POSITION = 3;
    public const int ENEMY_GENERATE_DISTANCE_BETWEEN_PLAYER_AND_ENEMY_TO_SPAWN = 20;
    public const int ENEMY_GENERATE_TIME_TO_INCRESE_NUMBER_OF_MAX_ENEMYS = 10;
    
    //Medkit
    public const int MEDKIT_AMOUNT_HEAL = 15;
    public const int MEDKIT_TIME_SELF_DESTROY = 5;
    
    //Zombie
    public const double ZOMBIE_DISTANCE_TO_CHASE = 2.5;
    public const double ZOMBIE_DISTANCE_TO_WANDER = 15;
    public const float ZOMBIE_TIME_BETWEEN_WANDER_AGAIN = 4;
    public const float ZOMBIE_ERROR_RATE_DISTANCE = 0.05f;
    public const int ZOMBIE_RADIO_TO_GENERATE_RANDOM_POSITION = 10;
    public const float ZOMBIE_CHANCE_TO_GENERATE_MED_KIT = 0.1f;

    //Player prefs
    public const string PLAYER_PREFS_MAX_TIME_SURVIVE = "MaxTimeSurvive";
    
    //Boss Generate
    public const int BOSS_GENERATE_TIME_BETWEEN_GENERATE = 60;
}
