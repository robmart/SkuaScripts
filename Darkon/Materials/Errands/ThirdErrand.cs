//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class ThirdErrand
{
    public IScriptInterface Bot { get; set; }
    public CoreBots Core => CoreBots.Instance;
    public CoreDarkon Darkon = new CoreDarkon();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Darkon.ThirdErrand();

        Core.SetOptions(false);
    }
}