/*
name: SwirlingTheAbyss
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Nation/Originul.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class SwirlingTheAbyss
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();
    public Fiendshard_Story Fiendshard = new Fiendshard_Story();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        STA();

        Core.SetOptions(false);
    }

    public readonly string[] Rewards =
 {
        "ArchFiend Deathlord Armet",
        "ArchFiend Deathlord Horned Armet",
        "ArchFiend Deathlord Helmet",
        "ArchFiend Deathlord Mask",
        "ArchFiend Deathlord Locks",
        "ArchFiend Deathlord Shag",
        "DeathLord Cloak of Nulgath",
        "Void Soul of Nulgath",
        "DeathLord Spines of Nulgath",
        "Deathless Wings of Nulgath",
    };

    public void STA(string? item = null)
    {
        if (item != null && Core.CheckInventory(item))
            return;


        if (item != null)
            Core.Logger($"Farming {item}.");
        else Core.Logger("Farming Swirling the Abyss Quest Rewards.");
        Fiendshard.Fiendshard_QuestlineP1();
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Rewards);
        ItemBase? Item = Bot.Inventory.Items.FirstOrDefault(x => x.Name == item);
        if (item == null)
        {
            int i = 1;
            while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
            {
                Nation.FarmBloodGem(10);
                Nation.FarmUni13(3);
                Nation.SwindleBulk(75);
                Nation.FarmDarkCrystalShard(50);
                Nation.FarmGemofNulgath(50);
                Nation.FarmVoucher(true);

                Core.EnsureAccept(7899);
                Core.EnsureCompleteChoose(7899);
                Core.ToBank(Rewards);
                Core.Logger($"Completed x{i++}");
            }
        }
        else
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(Item?.Name))
            {
                Nation.FarmBloodGem(10);
                Nation.FarmUni13(3);
                Nation.SwindleBulk(75);
                Nation.FarmDarkCrystalShard(50);
                Nation.FarmGemofNulgath(50);
                Nation.FarmVoucher(true);

                Core.EnsureAccept(7899);
                Core.EnsureComplete(7899, Item!.ID);

            }
        }
    }

}
