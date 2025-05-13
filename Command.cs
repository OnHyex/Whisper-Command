using System;
using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;

namespace Whisper_Command
{
    class privateMessage : ChatCommand
    {
        public override string[] CommandAliases() => new string[] { "whisper", "w" };
        public override string Description() => "sends a whisper to a player";
        public override string[][] Arguments() => new string[][] { new string[] { "%player_name" } };
        public override void Execute(string arguments)
        {
            string[] argument = arguments.ToLower().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (argument.Length < 1 || string.IsNullOrWhiteSpace(argument[0]))
            {
                Messaging.Echo(PLNetworkManager.Instance.LocalPlayer, "whisper (player name | playerid) (message)");
                return;
            }
            PLPlayer destinationPlayer = GetPlayer(argument[0]);
            PLPlayer localPlayer = PLNetworkManager.Instance.LocalPlayer;
            string Message = argument[1];
            if (!argument[0].Equals(destinationPlayer.GetPlayerName()))
            {
                int lengthExtraWordsInName = destinationPlayer.GetPlayerName().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries)[1].Length;
                Message = Message.Substring(lengthExtraWordsInName + 1);
            }
            
            Messaging.Echo(destinationPlayer, "<color=#00ffffff>[Whisper] </color><color=#" + PLPlayer.GetClassHexColorFromID(localPlayer.GetClassID()) + ">" + localPlayer.GetPlayerName() + " <" + localPlayer.GetClassName() + "></color> : " + Message);
            Messaging.Echo(localPlayer, "<color=#00ffffff>[Whisper] </color><color=#" + PLPlayer.GetClassHexColorFromID(localPlayer.GetClassID()) + ">" + localPlayer.GetPlayerName() + " <" + localPlayer.GetClassName() + "></color> : " + Message);
        }
        internal static PLPlayer GetPlayer(string argument)
        {
            PLPlayer player = null;
            if (Int32.TryParse(argument, out int num))
            {
                foreach (PLPlayer selector in PLServer.Instance.AllPlayers)
                {
                    if (selector != null && selector.TeamID == 0 && selector.GetPlayerID() == num)
                    {
                        player = selector;
                        return player;
                    }
                }
            }
            foreach (PLPlayer selector in PLServer.Instance.AllPlayers)
            {
                if (selector != null && selector.TeamID == 0 && selector.GetPlayerName(false).ToLower().StartsWith(argument))
                {
                    player = selector;
                    return player;
                }
            }
            Messaging.Echo(PLNetworkManager.Instance.LocalPlayer, "Invalid PlayerName or PlayerID");
            return player;
        }
    }

}

