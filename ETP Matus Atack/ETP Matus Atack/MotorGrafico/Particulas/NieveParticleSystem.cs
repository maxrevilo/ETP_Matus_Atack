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
    class NieveParticleSystem : ParticleSystem
    {
        public NieveParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "Particulas//nieve";

            settings.MaxParticles = 200;

            settings.Duration = TimeSpan.FromSeconds(3);
            settings.DurationRandomness = 0;

            settings.MinHorizontalVelocity = 0.3f;
            settings.MaxHorizontalVelocity = 0.6f;

            settings.MinVerticalVelocity = -1;
            settings.MaxVerticalVelocity = -6;

            settings.Gravity = -Vector3.Up;

            settings.EndVelocity = 3;

            settings.MinColor = new Color(100, 100, 170);
            settings.MaxColor = new Color(128, 128, 190);


            settings.MinRotateSpeed = 0;
            settings.MaxRotateSpeed = 0;

            settings.MinStartSize = 0.3f;
            settings.MaxStartSize = 0.3f;

            settings.MinEndSize = 0.3f;
            settings.MaxEndSize = 0.3f;

        }
    }
}
