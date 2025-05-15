using System;
using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;
using UnityEngine;

namespace Whisper_Command
{
    class privateMessage : ChatCommand
    {
        public override string[] CommandAliases() => new string[] { "whisper", "w" };
        public override string Description() => "sends a whisper to a player";
        public override string[][] Arguments() => new string[][] { new string[] { "%player_name" } };
        public override void Execute(string arguments)
        {
            string Message = arguments.ToLower().TrimStart(' ');
            PLPlayer destinationPlayer = GetPlayer(Message, out bool isPlayerID);
            if (destinationPlayer == null)
            {
                failMessage();
                return;
            }
            PLPlayer localPlayer = PLNetworkManager.Instance.LocalPlayer;
            if (destinationPlayer.IsBot)
            {
                if(UnityEngine.Random.Range(0, 100) == 0)
                {
                    Messaging.Echo(localPlayer, "<color=#" + PLPlayer.GetClassHexColorFromID(destinationPlayer.GetClassID()) + ">" + destinationPlayer.GetPlayerName(false) + " <" + destinationPlayer.GetClassName() + "></color> : 01000100 01101111 01101110 00100111 01110100 00100000 01110111 01101000 01101001 01110011 01110000 01100101 01110010 00100000 01100010 01101111 01110100 01110011 00100010");
                }
                else
                {
                    Messaging.Echo(localPlayer, "Don't whisper bots");
                }
                return;
            }
            
            if (!isPlayerID)
            {
                if (destinationPlayer.GetPlayerName(false).Length > arguments.Length)
                {
                    failMessage();
                    return;
                }
                Message = arguments.Substring(destinationPlayer.GetPlayerName(false).Length);
            } 
            else
            {
                if (destinationPlayer.GetPlayerID().ToString().Length > arguments.Length)
                {
                    failMessage();
                    return;
                }
                Message = arguments.Substring(destinationPlayer.GetPlayerID().ToString().Length);
            }
            if (string.IsNullOrWhiteSpace(Message))
            {
                failMessage();
                return;
            }
            Messaging.Echo(destinationPlayer, $"<color={Config.TextColour.Value}>[Whisper from] </color><color=#" + PLPlayer.GetClassHexColorFromID(localPlayer.GetClassID()) + ">" + localPlayer.GetPlayerName(false) + " <" + localPlayer.GetClassName() + "></color> : " + Message);
            Messaging.Echo(localPlayer, $"<color={Config.TextColour.Value}>[Whisper to] </color><color=#" + PLPlayer.GetClassHexColorFromID(destinationPlayer.GetClassID()) + ">" + destinationPlayer.GetPlayerName(false) + " <" + destinationPlayer.GetClassName() + "></color> : " + Message);
        }
        private static PLPlayer GetPlayer(string argument, out bool isPlayerID)
        {
            PLPlayer player = null;
            isPlayerID = false;
            if (Int32.TryParse(argument.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries)[0], out int num))
            {
                isPlayerID = true;
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
                if (selector != null && selector.TeamID == 0 && argument.ToLower().StartsWith(selector.GetPlayerName(false).ToLower()))
                {
                    player = selector;
                    return player;
                }
            }
            return player;
        }
        private static void failMessage()
        {
            Messaging.Echo(PLNetworkManager.Instance.LocalPlayer, "whisper (player name | playerid) (message)");
        }
    }

}

