using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace ETP_Matus_Atack
{
    public class Model3D : Microsoft.Xna.Framework.DrawableGameComponent
    {


        //Modelo de la malla 3D:
        public Model model;
        //Camara del Objeto:
        public Camera camera;
        //Orientacion:
        public Matrix orientation;
        //Posicion:
        public Vector3 position;
        //Scales
        public Vector3 scales;
        //Scale:
        public float scale
        {
            set{ scales = Vector3.One * value; }
        }
    

        public Model3D(Game game, Model model, Camera camera)
            : base(game)
        {
            this.model = model;
            this.camera = camera;
            orientation = Matrix.Identity;
            position = new Vector3();
            scales = Vector3.One;
        }

        #region Extraer a clase mas baja en jerarquia:
        public void setTexture(Texture2D textura)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.Texture = textura;
                }
            }

        }

        public void EnableTexture(bool enable)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = enable;
                }
            }

        }

        public void setAmbientalLight(Color color)
        {
            Vector3 V3Color = color.ToVector3();

            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.AmbientLightColor = V3Color;
                }
            }

        }

        public void setLight(bool enable, int index)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    switch (index)
                    {
                        case 0:
                            effect.DirectionalLight0.Enabled = enable;
                            break;
                        case 1:
                            effect.DirectionalLight1.Enabled = enable;
                            break;
                        case 2:
                            effect.DirectionalLight2.Enabled = enable;
                            break;
                    }

                }
            }

        }

        public void setLightColor(Color color, int index)
        {
            Vector3 V3Color = color.ToVector3();


            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    switch (index)
                    {
                        case 0:
                            effect.DirectionalLight0.DiffuseColor = V3Color;
                            break;
                        case 1:
                            effect.DirectionalLight1.DiffuseColor = V3Color;
                            break;
                        case 2:
                            effect.DirectionalLight2.DiffuseColor = V3Color;
                            break;
                    }
                    
                }
            }

        }

        public void setLightDir(Vector3 dir, int index)
        {

            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    switch (index)
                    {
                        case 0:
                            effect.DirectionalLight0.Direction = dir;
                            break;
                        case 1:
                            effect.DirectionalLight1.Direction = dir;
                            break;
                        case 2:
                            effect.DirectionalLight2.Direction = dir;
                            break;
                    }
                }
            }

        }


        public void setFog(float FogStart, float FogEnd, Color FogColor)
        {
            setFog(FogStart, FogEnd, FogColor, false);
        }


        public void setFog(float FogStart, float FogEnd, Color FogColor, bool Enabled)
        {
            Vector3 V3FogColor = FogColor.ToVector3();

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.FogStart = FogStart;
                    effect.FogEnd = FogEnd;
                    effect.FogColor = V3FogColor;
                    effect.FogEnabled = Enabled;
                }
            }

        }


        public void EnableFog()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.FogEnabled = true;
                }
            }
        }


        public void PreferPerPixelLighting(bool enable)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.PreferPerPixelLighting = enable;
                }
            }
        }
        #endregion

        public override void Initialize()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                }
            }


            base.Initialize();
        }


        public override void Draw(GameTime gameTime)
        {
            if (model != null)
            {
                DrawModel(model, camera, orientation, position);
            }

            base.Update(gameTime);
        }


        public virtual void DrawModel(Model model, Camera Cam, Matrix Orientation, Vector3 Position)
        {
            Matrix[] boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            Matrix worldMatrix = Matrix.CreateScale(scales) * Orientation * Matrix.CreateTranslation(Position);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransforms[mesh.ParentBone.Index] * worldMatrix;
                    effect.View = Cam.ViewMatrix;
                    effect.Projection = Cam.ProjectionMatrix;

                }
                mesh.Draw();
            }
        }
    }
}