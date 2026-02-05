using System;
using System.Collections;
using System.Text;
using System.Runtime.CompilerServices;

namespace yokai.Plugin
{
    public class CardTraitUpgradeByStatusScaling : CardTraitState
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            return new PropDescriptions
            {
                [CardTraitFieldNames.ParamStatusEffects.GetFieldName()] = new PropDescription("Status to multiply by.")
            };
        }


        /**
            I had to borrow this from the Sandscourged mod as the Pyreblooded Ability code doesn't appear to work for targeted spells.
            Changed the name of the trait to prevent possible conflicts with Sandscourged mod.
            */
        public override void OnApplyingCardUpgradeToUnit(CardState thisCard, CharacterState targetUnit, CharacterTriggerState? characterTriggerState, CardUpgradeState upgradeState, ICoreGameManagers coreGameManagers)
        {
            base.OnApplyingCardUpgradeToUnit(thisCard, targetUnit, characterTriggerState, upgradeState, coreGameManagers);
            string statusId = GetParamStatusEffects().First().statusId;
            int stackCount = targetUnit.GetStatusEffectStacks(statusId);
            upgradeState.SetAttackDamage(upgradeState.GetAttackDamage() * stackCount);
            upgradeState.SetAdditionalHP(upgradeState.GetAdditionalHP() * stackCount);
        }
    }
}