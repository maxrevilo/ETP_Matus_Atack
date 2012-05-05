using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace ETP_Matus_Atack
{
    public class FPSCam : GameComponent
    {
        public Camera camara;

        Vector2 ViewAngles = new Vector2(-(float)Math.PI / 2, 0 * (float)Math.PI / 2);

        public float AngSpeed = 10;

        public float MoveSpeed = 500;

        public FPSCam(Game game, Camera camera):base(game)
        {
            this.camara = camera;

            camera.setViewMatrix(Vector3.Zero, Vector3.Zero, Vector3.Up);

            float aspectRadio = game.GraphicsDevice.Viewport.AspectRatio;

            camera.setProjectionMatrix(MathHelper.ToRadians(45), aspectRadio, 0.3f, 100000);
        }

        public override void Initialize()
        {
            Mouse.SetPosition(Game.GraphicsDevice.Viewport.Width/2
                , Game.GraphicsDevice.Viewport.Height/2);

            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();

            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float HalfWidht = Game.GraphicsDevice.Viewport.Width / 2;

            float HalfHeight = Game.GraphicsDevice.Viewport.Height / 2;

            ViewAngles -= new Vector2(Mouse.GetState().X - HalfWidht, Mouse.GetState().Y - HalfHeight) / AngSpeed * time;
            ViewAngles.Y = MathHelper.Clamp(ViewAngles.Y, -MathHelper.PiOver2 + 0.01f, MathHelper.PiOver2 - 0.01f);


            Mouse.SetPosition((int)HalfWidht, (int)HalfHeight);


            Matrix Orientation = Matrix.CreateFromYawPitchRoll(ViewAngles.X, ViewAngles.Y, 0);


            if (kbs.IsKeyDown(Keys.Up))
            {
                camara.CameraPosition += Orientation.Forward * MoveSpeed * time;
            }
            if (kbs.IsKeyDown(Keys.Down))
            {
                camara.CameraPosition += Orientation.Backward * MoveSpeed * time;
            }
            if (kbs.IsKeyDown(Keys.Right))
            {
                camara.CameraPosition += Orientation.Right * MoveSpeed * time;
            }
            if (kbs.IsKeyDown(Keys.Left))
            {
                camara.CameraPosition += Orientation.Left * MoveSpeed * time;
            }


            if (kbs.IsKeyDown(Keys.Space))
            {
                camara.CameraPosition += Orientation.Up * MoveSpeed * time; ;
            }
            if (kbs.IsKeyDown(Keys.C))
            {
                camara.CameraPosition += Orientation.Down * MoveSpeed * time; ;
            }

            if (kbs.IsKeyDown(Keys.V))
            {
                camara.FieldOfView += time * MathHelper.ToRadians(20) ;
            }
            if (kbs.IsKeyDown(Keys.B))
            {
                camara.FieldOfView -= time * MathHelper.ToRadians(20);
            }

            if (kbs.IsKeyDown(Keys.F2))
                Game.GraphicsDevice.RasterizerState.FillMode = FillMode.WireFrame;
                //Game.GraphicsDevice.RenderState.FillMode = FillMode.WireFrame;
            if (kbs.IsKeyUp(Keys.F2))
                Game.GraphicsDevice.RasterizerState.FillMode = FillMode.Solid;
                //Game.GraphicsDevice.RenderState.FillMode = FillMode.Solid;

            camara.CameraTarget = camara.CameraPosition + Orientation.Forward;


            base.Update(gameTime);
        }


    }
}
