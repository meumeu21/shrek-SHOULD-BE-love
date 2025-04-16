public interface IManaSystem
{
    // bool UseMana(int amount);

    public bool UseSufficientMana(int usedMana);
    public void RestoreMana(int amount);
    public int GetCurrentMana();
}