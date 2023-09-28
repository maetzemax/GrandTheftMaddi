public class UpgradeCard : CharacterStats {

    public void ChooseUpgrade() {
        Player.instance.maxHealth += maxHealth;
        Player.instance.healthRegenerationRate += healthRegenerationRate;
        Player.instance.currentHealth += currentHealth;
        Player.instance.attackRange += attackRange;
        Player.instance.attackDamage += attackDamage;
        Player.instance.attackSpeed += attackSpeed;
        Player.instance.movementSpeed += movementSpeed;

        GameManager.currentGameState = GameManager.GameState.Ingame;
    }
}
