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
    class FireParticleSystem : ParticleSystem
    {
        public FireParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "Particulas//smoke";

            settings.MaxParticles = 500;

            settings.Duration = TimeSpan.FromSeconds(0.5f);
            settings.DurationRandomness = 0;

            settings.MinStartSize = 2;
            settings.MaxStartSize = 2;

            settings.MinEndSize = 0;
            settings.MaxEndSize = 0;

            // Use additive blending.
            settings.SourceBlend = Blend.SourceAlpha;
            settings.DestinationBlend = Blend.One;
        }
    }
}
