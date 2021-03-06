﻿using System.Collections.Generic;
using Game.Fighters;
using Game.GameObjects;

namespace Game.Controllers
{
    public class CombatController
    {
        private readonly Fighter first;
        private readonly Fighter second;
        private bool wasCompletedFirst;
        private bool wasCompletedSecond;

        public CombatController(Fighter first, Fighter second)
        {
            this.first = first;
            this.second = second;
        }

        public void CheckForCombat(List<GameObject> gameObjects)
        {
            if (first.IsAttacking)
            {
                if (!wasCompletedFirst)
                {
                    HandleDamage(first, second);
                    wasCompletedFirst = true;
                }
            }
            else
                wasCompletedFirst = false;
            if (second.IsAttacking)
            {
                if (!wasCompletedSecond)
                {
                    HandleDamage(second, first);
                    wasCompletedSecond = true;
                }
            }
            else
                wasCompletedSecond = false;
            foreach (var obj in gameObjects)
            {
                
            }   
        }

        private void HandleDamage(Fighter attacker, Fighter defender)
        {
            if (defender.Body.Bottom < attacker.Body.Bottom - attacker.Body.Height / 2)
                return;

            if (attacker.LookingRight)
            {
                if (defender.IsBlocking && !defender.LookingRight) return;
                if (defender.Body.Contains(attacker.Body.Right + attacker.AttackRange, attacker.Body.Y + attacker.Body.Height / 4))
                    defender.HealthPoints -= attacker.AttackDamage;
            }
            else
            {
                if (defender.IsBlocking && defender.LookingRight) return;
                if (defender.Body.Contains(attacker.Body.Left - attacker.AttackRange, attacker.Body.Y + attacker.Body.Height / 4))
                    defender.HealthPoints -= attacker.AttackDamage;
            }
        }

        private void HandleDamage(GameObject obj, Fighter target)
        {
            
        }
    }
}
