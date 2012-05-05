#region File Description
//-----------------------------------------------------------------------------
// NieveParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace ETP_Matus_Atack.Particulas
{
    /// <summary>
    /// Custom particle system for creating the fiery part of the explosions.
    /// </summary>
    class SplashParticleSystem : ParticleSystem
    {
        public SplashParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "Particulas//splash";

            settings.MaxParticles = 30;

            settings.Duration = TimeSpan.FromSeconds(4.5f);
            settings.DurationRandomness = 0;

            settings.MinHorizontalVelocity = 0.1f;
            settings.MaxHorizontalVelocity = 0.5f;

            settings.MinVerticalVelocity = -0.2f;
            settings.MaxVerticalVelocity = 0.3f;

            settings.Gravity = Vector3.Down;

            settings.EndVelocity = 0.1f;

            settings.MinColor = new Color(100, 100, 170);
            settings.MaxColor = new Color(128, 128, 190);


            settings.MinRotateSpeed = -0.3f;
            settings.MaxRotateSpeed = 0.7f;

            settings.MinStartSize = 20f;
            settings.MaxStartSize = 50f;

            settings.MinEndSize = 70f;
            settings.MaxEndSize = 100f;

        }
    }
}
