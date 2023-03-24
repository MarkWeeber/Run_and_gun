namespace Run_n_gun.Space
{
    public class Enums
    {

    }

    public enum GameState
    {
        OnMainMenu,
        InGamePaused,
        InGameActive,
        PlayerDead,
        LevelVictory,
        LevelGameOver
    }

    public enum DamagerType
    {
        Melee,
        Projectile
    }

    public enum EnemySpotState
    {
        NoTarget,
        TargetIsVisible,
        AlertedOnTarget,
        TargetLost
    }
}