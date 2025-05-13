using System;
using CodeStage.AntiCheat.ObscuredTypes;
using PulsarModLoader;
using PulsarModLoader.MPModChecks;

namespace Whisper_Command
{
    public class Mod : PulsarMod
    {
        public override string Version => "1.1.0";
        public override string Author => "OnHyex";
        public override string ShortDescription => "Adds a command to whisper (/whisper)";
        public override string Name => "WhisperCommand";
        public override string HarmonyIdentifier()
        {
            return $"{Author}.{Name}";
        }
        public override int MPRequirements => (int)MPRequirement.None;
    }
}
