//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/JoinLegion[UndeadWarrior].cs
//cs_include Scripts/Story/NecroDungeon.cs


using RBot;

public class LegionExercise1
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public JoinLegion JoinLegion = new JoinLegion();
    public CoreStory Story = new CoreStory();

    new string[] Rewards = { "Undead Champion Blade", "Legendary Golden Death Blade" };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        if (!Core.CheckInventory("Undead Champion"))
            JoinLegion.JoinLegionQuests();

        Exercise(Rewards);
        Core.ToBank(Rewards);

        Core.SetOptions(false);
    }

    public void Exercise(string[] item = null)
    {
        Core.Logger("Disclaimer: Percentages are randomized, just made purely for fun. i cba making it an actualy %age");

        Random rnd = new Random();
        int Dice = rnd.Next(1, 101);   // creates a number from 1 to 100

        //-------------------------------------------------------------------------------------------------------

        if (item != null)
        {
            if (Core.CheckInventory(item)) { return; }
            Core.AddDrop(item);
        }

        if (item == null)
        {
            if (Core.CheckInventory(Rewards)) { return; }
            Core.AddDrop(Rewards);
        }

        int i = 1;

        var displayPercentage = $"{(decimal)Dice / 100:P}";


        Core.Logger($"Potato Prediction Inc. Decided: {displayPercentage} is The Chance for Desired Rewards.");


        while (!Core.CheckInventory(new[] { "Undead Champion Blade", "Legendary Golden Death Blade" }))
        {
            Core.EnsureAccept(821);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("lair", "Water Draconian", "Flawless DracoHorn", 40, isTemp: false, publicRoom: false);
            Core.HuntMonster("lair", "Golden Draconian", "Golden DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Dark Draconian", "Dark DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Bronze Draconian", "Mammoth DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Water Draconian", "Water DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Venom Draconian", "Venom DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("lair", "Purple Draconian", "Iron DracoHeart", isTemp: true, publicRoom: false);
            Core.HuntMonster("Uppercity", "Chaotic Draconian", "Chaotic DracoHeart", isTemp: true, publicRoom: false);
            Core.EnsureComplete(821);
            Core.Logger($"Finished Quest {i++} Times");
        }

        Core.Logger($"Farming {item} Took {i++} Times");

        if (i++ > Dice)
            Core.Logger($"Perdiction: {displayPercentage} May have been a bit Low");
        if (i++ < Dice)
            Core.Logger($"Perdiction: {displayPercentage} Was waaaay to high... Congratulations!");
    }

}