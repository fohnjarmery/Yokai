using ShinyShoe.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace yokai.Plugin.code.RoomModifiers
{
    /// <summary>
    /// Not designed to be used directly. This drives the Haunt status effects implementation and is controlled by that status effect.
    /// </summary>
    public class RoomStateHauntModifier : RoomStateModifierBase, IRoomStateModifier, IRoomStateDamageModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        public bool IsPreviewModeCopy { get; set; }
        public int DamageModifier = 0;
        public Team.Type TeamType = Team.Type.None;

        public RoomStateHauntModifier CopyForPreview()
        {
            RoomStateHauntModifier roomStateHauntModifier = new();
            CopyBaseStateForPreview(roomStateHauntModifier);
            roomStateHauntModifier.DamageModifier = DamageModifier;
            roomStateHauntModifier.TeamType = TeamType;
            roomStateHauntModifier.IsPreviewModeCopy = true;
            return roomStateHauntModifier;
        }

        public int GetModifiedAttackDamage(Damage.Type damageType, CharacterState attackerState, bool requestingForCharacterStats, ICoreGameManagers coreGameManagers)
        {
            if (requestingForCharacterStats)
            {
                return GetDynamicInt(attackerState);
            }
            return 0;
        }

        public int GetModifiedMagicPowerDamage(ICoreGameManagers coreGameManagers)
        {
            return 0;
        }

        public override int GetDynamicInt(CharacterState characterContext)
        {
            if (characterContext.GetTeamType() == Team.Type.Monsters)
            {
                return DamageModifier;
            }
            return 0;
        }

        public override bool GetShowTooltip()
        {
            return false;
        }
    }
}