﻿using DiscordChat.Settings;
using Obsidian.API;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordChat.Commands
{
    public class DiscordCommandModule
    {

        #region /discord <reload|debug|commands>
        [Command("DiscordChat", "discord")]
        [CommandInfo("DiscordChat available commands.", "/discord <reload|debug|commands>")]

        public async Task DiscordAsync(CommandContext Context) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/DiscordChat <reload|debug|commands>"));

        [CommandOverload]
        public async Task DiscordAsync(CommandContext Context, [Remaining] string args_)
        {
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            var chatMessage = IChatMessage.Simple("");
            switch (args[0].ToLower())
            {
                #region commands
                case "commands":
                    // List all of the commands of this plugin
                    //It will be necessary when the plugin system works fully and I does not really need to hard code this into the server to work properly.
                    chatMessage = IChatMessage.Simple("");
                    var cmds_prefix = IChatMessage.CreateNew();
                    cmds_prefix.Text = $"{ChatColor.Gray}DiscordChat {ChatColor.Red}{Globals.VersionFull}{ChatColor.Gray} commands:";
                    chatMessage.AddExtra(cmds_prefix);
                    var cmds_list = IChatMessage.Simple("");
                    foreach (var cmd in Globals.Commands)
                    {
                        var commandName = IChatMessage.Simple("\n");
                        commandName.AddExtra(Globals.RenderClickableCommand(cmd.Key));

                        var commandInfo = IChatMessage.CreateNew();
                        commandInfo.Text = $"{ChatColor.Gray}:{ChatColor.Reset} {cmd.Value}{ChatColor.Reset}";

                        cmds_list.AddExtra(commandName);
                        cmds_list.AddExtra(commandInfo);
                    }
                    chatMessage.AddExtra(cmds_list);
                    break;
                #endregion

                #region debug
                case "debug":
                    var msg = $"{ChatColor.Red}Welcome to the {ChatColor.Obfuscated}debug{ChatColor.Reset}{ChatColor.Red}!";
#if DEBUG
                    msg += $"{ChatColor.DarkGreen} // You're looking soo pretty today. :3";
#endif
                    await Context.Player.SendMessageAsync(msg);
                    break;
                #endregion

                #region reload
                case "reload":
                    //It will be necessary when the plugin system works fully and I does not really need to hard code this into the server to work properly.
                    chatMessage = IChatMessage.Simple("");
                    var message = IChatMessage.CreateNew();
                    message.Text = $"{ChatColor.Gray}DiscordChat {ChatColor.Red}{Globals.VersionFull}{ChatColor.Gray} successfully reloaded.";
                    //#if DEBUG
                    message.Text += $" {ChatColor.DarkGreen}// This command does not actually do anything right now, beside telling you that it does not do anything. ";
                    //#endif

                    chatMessage.AddExtra(message);
                    break;
                #endregion

                #region Not valid args[0]
                default:
                    chatMessage = Globals.RenderCommandUsage("/DiscordChat <reload|debug|commands>");
                    break;
                    #endregion
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

    }
}
