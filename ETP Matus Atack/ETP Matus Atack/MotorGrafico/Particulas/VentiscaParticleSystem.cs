#region File Description
//-----------------------------------------------------------------------------
// SmokePlumeParticleSystem.cs
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
    /// Custom particle system for creating a giant plume of long lasting smoke.
    /// </summary>
    class VentiscaParticleSystem : ParticleSystem
    {
        public VentiscaParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "Particulas//ventisca";

            settings.MaxParticles = 270;

            settings.Duration = TimeSpan.FromSeconds(3);

            settings.MinHorizontalVelocity = 0.2f;
            settings.MaxHorizontalVelocity = 0.3f;

            settings.MinVerticalVelocity = 0;
            settings.MaxVerticalVelocity = 0.1f;

            settings.Gravity = new Vector3(0, 0, 0);

            settings.EndVelocity = 1.5f;

            settings.MinColor = Color.White;
            settings.MaxColor = Color.White;

            settings.MinRotateSpeed = -0.01f;
            settings.MaxRotateSpeed = 0.01f;

            settings.MinStartSize = 50;
            settings.MaxStartSize = 70;

            settings.MinEndSize = 50;
            settings.MaxEndSize = 100;
        }
    }
}
