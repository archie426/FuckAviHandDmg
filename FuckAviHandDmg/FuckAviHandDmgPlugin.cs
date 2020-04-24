using System;
using System.Linq;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace FuckAviHandDmg
{
    public class FuckAviHandDmgPlugin : RocketPlugin
    {
        public override void LoadPlugin() => DamageTool.damagePlayerRequested += DamageToolOndamagePlayerRequested;
        


        public override void UnloadPlugin(PluginState state = PluginState.Unloaded) => DamageTool.damagePlayerRequested -= DamageToolOndamagePlayerRequested;
        

        private void DamageToolOndamagePlayerRequested(ref DamagePlayerParameters parameters, ref bool shouldallow)
        {
            if (parameters.cause != EDeathCause.GUN || parameters.cause != EDeathCause.MELEE || !shouldallow)
                return;

            var playerParameters = parameters;
            foreach (InventorySearch search in parameters.player.inventory.search(EItemType.GUN).Where(search => search.jar.interactableItem.item.id == playerParameters.player.equipment.itemID))
                playerParameters.player.inventory.askDropItem(UnturnedPlayer.FromPlayer(playerParameters.player).CSteamID, search.page, search.jar.x, search.jar.y);
            
            UnturnedChat.Say(UnturnedPlayer.FromPlayer(parameters.player), "You dropped your weapon as you have been disabled by " + UnturnedPlayer.FromCSteamID(parameters.killer).DisplayName);
            
        }

        
        
        
    }
}