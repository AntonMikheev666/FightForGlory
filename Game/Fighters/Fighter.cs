﻿using System.Drawing;
using Game.BaseStructures.Enums;
using Game.Controllers;
using Game.GameInformation;

namespace Game.Fighters
{
    public abstract class Fighter
    {
        public PlayerNumber Number { get; set; }
        public bool IsFrozen { get; set; }
        public string Name { get; set; }
        public float HealthPoints { get; set; }
        public float ManaPoints { get; set; }
        public float AttackDamage { get; set; }
        public float AttackRange { get; set; }
        public bool OnGround { get; set; }
        public RectangleF Body { get; set; }
        public bool LookingRight { get; set; }
        public bool IsBlocking { get; protected set; }
        public bool IsAttacking { get; protected set; }
        public FighterMotionState State { get; set; }

        public void ToTheGround()
        {
            if (Body.Y < GameSettings.Resolution.Y / 1.5f)
                Body = Body.Move(0, 15);
            else
                OnGround = true;
        }

        public void Jump()
        {
            if (IsAttacking || IsBlocking)
                return;
            if (!OnGround || !(Body.Y / 2 > 200)) return;
            OnGround = false;
            Body = Body.Move(0, -300);
        }

        public void Move(int dx, Fighter opponent)
        {
            if (IsAttacking || IsBlocking)
                return;
            if (!IsMovementAllowed(dx, 0, opponent)) return;
            Body = Body.Move(dx, 0);
            if (!IsMovementAllowed(dx, 0, opponent)) return;
            Body = Body.Move(dx, 0);
        }

        public void Attack()
        {
            if (IsAttacking || IsBlocking)
                return;

            IsAttacking = true;
            AttackCooldown();
        }

        public void Block()
        {
            if (IsAttacking || IsBlocking)
                return;
            IsBlocking = true;
            BlockCooldown();
        }

        protected bool IsMovementAllowed(float dx, float dy, Fighter opponent)
        {
            var newFighterPos = new RectangleF(Body.X + dx, Body.Y + dy, Body.Width, Body.Height);

            var notAllowed = newFighterPos.IntersectsWith(opponent.Body) || IsOutsideScreen(dx, dy);
            return !notAllowed;
        }

        protected bool IsOutsideScreen(float dx, float dy)
        {
            var newFighterPos = new RectangleF(Body.X + dx, Body.Y + dy, Body.Width, Body.Height);

            var leftScreenBorder = new RectangleF(-1, 0, 1, GameSettings.Resolution.Y);
            var rightScreenBorder = new RectangleF(GameSettings.Resolution.X - 1, 0, 1, GameSettings.Resolution.Y);

            return newFighterPos.IntersectsWith(leftScreenBorder) || newFighterPos.IntersectsWith(rightScreenBorder);
        }

        public abstract void BlockCooldown();
        public abstract void AttackCooldown();
        public abstract ComboController GetComboController();
        public abstract void ManaRegeneration();
    }
}