using Conductor.RoomModifiers;
using ShinyShoe;
using ShinyShoe.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using yokai.Plugin.code.RoomModifiers;

namespace yokai.Plugin
{
    class StatusEffectPanicState : StatusEffectState, IDamageStatusEffect
    {
        public override bool TestTrigger(StatusEffectState.InputTriggerParams inputTriggerParams, StatusEffectState.OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            if (inputTriggerParams.associatedCharacter != null && inputTriggerParams.associatedCharacter.IsAlive)
            {
                this.associatedCharacter = inputTriggerParams.associatedCharacter;
            }
            else
            {
                this.associatedCharacter = null;
            }
            CharacterState? characterState = associatedCharacter;
            this.stacks = ((characterState != null) ? characterState.GetStatusEffectStacks(base.GetStatusId()) : 0);
            return this.stacks > 0 && this.associatedCharacter != null;
        }

        protected override IEnumerator OnTriggered(StatusEffectState.InputTriggerParams inputTriggerParams, StatusEffectState.OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            CoreSignals.DamageAppliedPlaySound.Dispatch(Damage.Type.Poison);
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            int damageAmount = this.GetDamageAmount(this.stacks);
            CharacterState? target = associatedCharacter;
            CombatManager.ApplyDamageToTargetParameters parameters = default(CombatManager.ApplyDamageToTargetParameters);
            parameters.damageType = Damage.Type.Poison;
            StatusEffectData sourceStatusEffectData = base.GetSourceStatusEffectData();
            parameters.affectedVfx = ((sourceStatusEffectData != null) ? sourceStatusEffectData.GetOnAffectedVFX() : null);
            parameters.relicState = inputTriggerParams.suppressingRelic;
            yield return combatManager.ApplyDamageToTarget(damageAmount, target, parameters);
            yield break;
        }

        public override int GetEffectMagnitude(int stacks = 1)
        {
            return this.GetDamageAmount(stacks);
        }

        private int GetDamageAmount(int stacks)
        {
            return (base.GetParamInt() + this.relicManager.GetModifiedStatusMagnitudePerStack("yokai.plugin_panic", base.GetAssociatedCharacter().GetTeamType())) * stacks;
        }

        public const string StatusId = "panic";

        private CharacterState? associatedCharacter;
        private int stacks;
    }
}