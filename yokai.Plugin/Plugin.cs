using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using I2.Loc;
using Microsoft.Extensions.Configuration;
using ShinyShoe.Logging;
using SimpleInjector;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Card;
using TrainworksReloaded.Base.CardUpgrade;
using TrainworksReloaded.Base.Character;
using TrainworksReloaded.Base.Class;
using TrainworksReloaded.Base.Effect;
using TrainworksReloaded.Base.Localization;
using TrainworksReloaded.Base.Prefab;
using TrainworksReloaded.Base.Trait;
using TrainworksReloaded.Base.Trigger;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Impl;
using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TrainworksReloaded.Base.Extensions;
using static BalanceData;
using System.Reflection;


namespace yokai.Plugin
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger = new(MyPluginInfo.PLUGIN_GUID);
        
        // Plugin startup logic. This function is automatically called when your plugin initializes
        public void Awake()
        {
            Logger = base.Logger;

            var builder = Railhead.GetBuilder();
            builder.Configure(
                MyPluginInfo.PLUGIN_GUID,
                c =>
                {
                    // Be sure to include any new json files if you add more.
                    c.AddMergedJsonFile(
                        "json/global.json",

                        // Class Stuff
                        "json/class.json",

                        //Champions
                        "json/champions/champion_kitsune.json",
                        "json/champions/champion_tengu.json",

                        //Units
                        "json/units/unit_boroboroton.json",
                        "json/units/unit_ippondatara.json",
                        "json/units/unit_kodama.json",
                        "json/units/unit_oni.json",
                        "json/units/unit_wanyudo.json",
                        "json/units/unit_qilin.json",
                        "json/units/unit_kasaobake.json",
                        "json/units/unit_nurikabe.json",
                        "json/units/unit_umibozu.json",
                        "json/units/unit_yukionna.json",
                        "json/units/unit_orochi.json",
                        "json/units/unit_chochinobake.json",
                        "json/units/unit_kappa.json",
                        "json/units/unit_shogoro.json",

                        //Spells
                        "json/spells/spell_darkness.json",
                        "json/spells/spell_parade.json",
                        "json/spells/spell_possession.json",
                        "json/spells/spell_shudder.json",
                        "json/spells/spell_snaketrail.json",
                        "json/spells/spell_tanukicharm.json",
                        "json/spells/spell_wailingecho.json",
                        "json/spells/spell_purityritual.json",
                        "json/spells/spell_furiousanger.json",
                        "json/spells/spell_sacredtrees.json",
                        "json/spells/spell_nightmare.json",
                        "json/spells/spell_frightfullaughter.json",
                        "json/spells/spell_thunderingdrums.json",
                        "json/spells/spell_bagofwind.json",
                        "json/spells/spell_shrinedance.json",
                        "json/spells/spell_thousanddemonmarch.json",
                        "json/spells/spell_onmyojitome.json",
                        "json/spells/spell_sesshosekispike.json",
                        "json/spells/spell_itsumadeitsumade.json",
                        "json/spells/spell_gashadokurosstride.json",
                        "json/spells/spell_draconictyphoon.json",
                        "json/spells/spell_heartstopper.json",

                        //Enhancers
                        "json/enhancers/enhancer_Fearstone.json",

                        //Equipment
                        "json/equipment/equipment_divinebranch.json",
                        "json/equipment/equipment_kusarigama.json",

                        //Rooms
                        "json/rooms/room_demonparade.json",
                        "json/rooms/room_toriigate.json",

                        //Artifacts
                        "json/relics/dojigourd.json",
                        "json/relics/noblepurse.json",
                        "json/relics/sudamaseal.json",
                        "json/relics/tengufeather.json",
                        "json/relics/yokaiincense.json",
                        "json/relics/paradebanner.json",
                        "json/relics/scampuss.json",
                        "json/relics/hannyamask.json",
                        "json/relics/relic9.json",
                        "json/relics/onimask.json",
                        "json/relics/relic11.json"
                    );
                }
            );

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            
            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

        }
    }
}
