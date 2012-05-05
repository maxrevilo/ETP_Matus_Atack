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

/**
 * DRAW ORDERS:
 * 1° : Objetos de escenario.
 * 2° : Objetos movibles.
 * 3° : Particulas.
 * 5° : SkyMap.
 * 6° : Bloom y PostEffects.
**/

namespace ETP_Matus_Atack
{
    public class GraphicManager : DrawableGameComponent
    {
        FPSCam fpsCamara;
        CameraAnimated camara;
        float fadeSpeed;

        FrameCounter fCount;
        Mundo ciudad;
        Escenario escenarioActual;
        
        Texture2D ropaProta;
        Texture2D ropaMalo;
        Texture2D portada;
        Texture2D instrucciones1;
        Texture2D instrucciones2;
        Texture2D youWin;
        Texture2D youLoose;

        Model3D personaje;
        Model3D[] edificios;
        Model3D rect;
        Model3D fireBall;
        Model3D iceJavel;

        Model skySphere;
        Effect skyEffect;

        SpriteBatch spriteBatch;
        SpriteFont font;

        BloomComponent bloom;

        KeyboardState oldKbs;

        Texture2D terreno;
        Texture2D hud;
        Texture2D thePIXEL;

        //List<ParticleEmiter> emitidores;

        const float distanciaDeVista = 50000;
        const int NEdf = 7;
        const float fadeSlow = 2f;
        const float fadeFast = 10f;
        readonly Vector3 AjusteDeCamara = new Vector3(0,380, 600);
        readonly Vector3 ObjetivoDeCamara = new Vector3(0, 100, -600);
        readonly Rectangle lifeBar = new Rectangle(181, 29, 175, 20);
        readonly Rectangle magicBar = new Rectangle(181,59, 175, 20);

        struct Sentella
        {
            public TimeSpan tiempo;
            public float brillo;
            public bool brillando;
            public Color ambiLigth;
            public Color ligthColor;
        };
        Sentella sentella;

        

        public bool readyToFollow
        { get { return fadeSpeed > fadeSlow; } }


        public GraphicManager(Game game, Mundo Ciudad)
            : base(game)
        {
            camara = new CameraAnimated();
            camara.CameraPosition = new Vector3(9000, 20000, 50000 );
            camara.FadeCameraPosition(21 * Vector3.One, 0);
            fpsCamara = new FPSCam(game, camara);
            camara.FarPlane = float.MaxValue;
            fadeSpeed = fadeSlow;
            this.ciudad = Ciudad;
            fCount = new FrameCounter(game);

            bloom = new BloomComponent(game);

            oldKbs = Keyboard.GetState();

            //emitidores = new List<ParticleEmiter>();
        }

        public override void Initialize()
        {
            escenarioActual = ciudad.Escenas.ElementAt<Escenario>(0);
            Game.Components.Add(fCount);

            base.Initialize();

            //Particulas.ParticleManager.Iniciar(Game);


            personaje.PreferPerPixelLighting(true);

            //ParticleEmiter fuego = new ParticleEmiter(TimeSpan.Zero, new TimeSpan(0,0,0,0,500), Particulas.ParticleManager.Types.fire, Vector3.Zero, camara);
            //emitidores.Add(fuego);

            bloom.Initialize();
            bloom.Settings = BloomSettings.PresetSettings[0];
            bloom.DrawOrder = 6;
            Game.Components.Add(bloom);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            personaje = new Model3D(this.Game, Game.Content.Load<Model>("Assets_Graficos\\Personaje"), camara);
            ambiente(personaje);
            personaje.EnableTexture(true);

            


            edificios = new Model3D[NEdf];
            for (int i = 0; i < NEdf; i++)
            {
                edificios[i] = new Model3D(this.Game, Game.Content.Load<Model>("Assets_Graficos\\Edificios\\Edf" + i), camara);
                ambiente(edificios[i]);
                edificios[i].scale = 0.1f;
                edificios[i].PreferPerPixelLighting(false);
            }

            rect = new Model3D(Game, Game.Content.Load<Model>("rect"), camara);
            ambiente(rect);

            ropaProta = Game.Content.Load<Texture2D>("Assets_Graficos\\Ropas\\Prota1");
            ropaMalo = Game.Content.Load<Texture2D>("Assets_Graficos\\Ropas\\Malo1");

            fireBall = new Model3D(Game, Game.Content.Load<Model>("Assets_Graficos\\fire_cube"), camara);
            ambiente(fireBall);
            fireBall.setAmbientalLight(Color.White);
            fireBall.PreferPerPixelLighting(false);

            iceJavel = new Model3D(Game, Game.Content.Load<Model>("Assets_Graficos\\IceJavelin"), camara);
            ambiente(iceJavel);
            iceJavel.setAmbientalLight(Color.White);
            iceJavel.PreferPerPixelLighting(false);

            skySphere = Game.Content.Load<Model>("Assets_Graficos\\skySphere");
            skyEffect = Game.Content.Load<Effect>("Assets_Graficos\\skySphereFX");
            TextureCube skyMap = Game.Content.Load<TextureCube>("Assets_Graficos\\skymapNigth");
            skyEffect.Parameters["skyMap"].SetValue(skyMap);
            foreach (ModelMesh mesh in skySphere.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = skyEffect;
                }
            }

            portada = Game.Content.Load<Texture2D>("Assets_Graficos\\Portada");
            instrucciones1 = Game.Content.Load<Texture2D>("Assets_Graficos\\instrucciones1");
            instrucciones2 = Game.Content.Load<Texture2D>("Assets_Graficos\\instrucciones2");
            hud = Game.Content.Load<Texture2D>("Assets_Graficos\\hud");
            youWin = Game.Content.Load<Texture2D>("Assets_Graficos\\YouWin");
            youLoose = Game.Content.Load<Texture2D>("Assets_Graficos\\YouLose");

            terreno = Game.Content.Load<Texture2D>("Assets_Graficos\\Terreno");
            rect.setTexture(terreno);

            font = Game.Content.Load<SpriteFont>("Assets_Graficos\\font1");

            thePIXEL = new Texture2D(Game.GraphicsDevice, 1, 1);
            thePIXEL.SetData<Color>(new Color[]{Color.White});


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.B) && !oldKbs.IsKeyDown(Keys.B))
                bloom.Visible = !bloom.Visible;

            //Espera a que este lista la camara:
            if (!readyToFollow && camara.CameraPositionOnTarget(100f))
            {
                fadeSpeed = fadeFast;
            }

            #region actualizar Sentella:
            sentella.tiempo -= gameTime.ElapsedGameTime;
            if (sentella.tiempo.Milliseconds <= 0)
            {
                if (random(0, 1) <= 0.3f)
                    sentella.tiempo = new TimeSpan(0, 0, 0, 0, (int)(1000 * random(0, 0.35f)));
                else
                    sentella.tiempo = new TimeSpan(0, 0, 0, 0, (int)(1000 * random(0.5f, 7)));

                sentella.brillando = true;
                sentella.brillo = 0.15f;
            }
            else if (sentella.brillando)
            {
                sentella.brillo *= 1.2f;
                if (sentella.brillo >= 1) sentella.brillando = false;
            }
            else if (sentella.brillo >= 0.15f)
            {
                sentella.brillo *= 0.8f;
                if (sentella.brillo <= 0.15f) sentella.brillo = 0;
            }
            sentella.ligthColor = new Color(Vector4.One * sentella.brillo);
            float lerp = MathHelper.Lerp(0, 0.75f, sentella.brillo);
            sentella.ambiLigth = new Color(Vector4.One * lerp + (new Color(0, 0, 10)).ToVector4());
            #endregion

            /*foreach (ParticleEmiter emiter in emitidores)
            {
                emiter.Update(gameTime);
            }*/

            oldKbs = kbs;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle screen = new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);

            bool port = EstadoDeJuego.onPortada();
            bool inst1 = EstadoDeJuego.onInstrucciones1();
            bool inst2 = EstadoDeJuego.onInstrucciones2();

            bloom.BeginDraw();

            if (port || inst1 || inst2)
            {
                Texture2D toShow;
                if (port) toShow = portada;
                else if (inst1) toShow = instrucciones1;
                else toShow = instrucciones2;

                //spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                spriteBatch.Draw(toShow, screen , Color.White);
                spriteBatch.DrawString(font, "Please Press Any Key", new Vector2(350, 550), Color.Red);
                spriteBatch.End();

            }
            else
            {

                #region setCamara:
                Matrix orientacion = Matrix.CreateRotationY(ciudad.jugador.angulo);
                if (Keyboard.GetState().IsKeyDown(Keys.RightControl))
                {
                    fpsCamara.Update(gameTime);
                }
                else
                {
                    camara.Update(gameTime);
                    camara.FadeCameraPosition(ciudad.jugador.posicion + Vector3.Transform(AjusteDeCamara, orientacion), fadeSpeed);
                    camara.CameraTarget = ciudad.jugador.posicion + Vector3.Transform(ObjetivoDeCamara, orientacion);
                }
                #endregion

                #region Dibujar Rectangulos
                /*
                foreach (Rectangle rectangulo in escenarioActual.Colisionable.CE)
                {
                    rect.EnableTexture(false);
                    rect.position = new Vector3(rectangulo.Location.X, 0, rectangulo.Location.Y);
                    rect.scales.X = rectangulo.Width;
                    rect.scales.Z = rectangulo.Height;
                    rect.Draw(gameTime);
                }*/
                #endregion

                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                GraphicsDevice.SamplerStates[0] = SamplerState.AnisotropicWrap;
                GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                GraphicsDevice.BlendState = BlendState.Opaque;

                #region Dibujar Escenario:

                rect.EnableTexture(true);
                rect.position = new Vector3(-1000, 0, -15000);
                rect.scale = 18000;
                rect.setAmbientalLight(sentella.ambiLigth);
                rect.setLightColor(sentella.ligthColor, 1);
                rect.Draw(gameTime);

                foreach (Model3D edif in edificios)
                {
                    edif.setAmbientalLight(sentella.ambiLigth);
                    edif.setLightColor(sentella.ligthColor, 1);
                }
                
                foreach (Edificios edif in escenarioActual.Edif)
                {
                    if (Vector3.Distance(edif.Posicion, camara.CameraPosition) <= distanciaDeVista)
                    {
                        edificios[edif.Modelo].position = edif.Posicion;
                        edificios[edif.Modelo].scale = edif.Escala;
                        edificios[edif.Modelo].Draw(gameTime);
                    }
                }

                #endregion

                #region Dibujar Personaje:
                personaje.setAmbientalLight(sentella.ambiLigth);
                personaje.setLightColor(sentella.ligthColor, 1);
                personaje.setTexture(ropaProta);
                personaje.position = ciudad.jugador.posicion;
                personaje.orientation = orientacion;
                personaje.Draw(gameTime);
                #endregion

                #region Dibujar Matus:
                personaje.setTexture(ropaMalo);
                foreach (Matus matu in escenarioActual.Enemigos)
                {
                    if (matu.isAlive && Vector3.DistanceSquared(matu.posicion, camara.CameraPosition) <= distanciaDeVista * distanciaDeVista)
                    {
                        personaje.position = matu.posicion;
                        personaje.orientation = Matrix.CreateRotationY(matu.angulo);
                        personaje.Draw(gameTime);
                    }
                }
                #endregion

                #region Dibujar Balas:
                //personaje.setTexture(ropaMalo);
                Model3D tiroModel;
                foreach (Disparo tiro in ciudad.jugador.Proyectiles)
                {
                    if (!tiro.Ready())
                    {
                        if (tiro is IceCube)
                            tiroModel = iceJavel;
                        else
                            tiroModel = fireBall;

                        tiroModel.position = tiro.Posicion;
                        tiroModel.orientation = Matrix.CreateRotationY(tiro.direction);
                        tiroModel.scale = tiro.size;
                        tiroModel.Draw(gameTime);
                    }
                }
                #endregion

                #region Dibujer SkySphere:
                skyEffect.Parameters["matViewProjection"].SetValue(camara.ViewMatrix * camara.ProjectionMatrix);
                skyEffect.Parameters["matView"].SetValue(camara.ViewMatrix * camara.ProjectionMatrix);
                foreach (ModelMesh mesh in skySphere.Meshes)
                    mesh.Draw();
                #endregion

                #region GUI:
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                //spriteBatch.DrawString(font, "FPS :" + fCount.cuadrosTotales + ", UPS: " + fCount.ActualizacionesTotales, new Vector2(600, 10), Color.Green);
                //spriteBatch.DrawString(font, "CamPos: " + camara.CameraPosition, new Vector2(600, 30), Color.Green);

                if (EstadoDeJuego.onJuego())
                {
                    spriteBatch.Draw(hud, screen, Color.White);

                    Color color = Color.Lerp(Color.Red, Color.Green, ciudad.jugador.vida / Personaje.MaxLife);

                    Rectangle bar = lifeBar;
                    bar.Width = (int) Math.Round(ciudad.jugador.vida * ((float) lifeBar.Width) / Personaje.MaxLife);
                    spriteBatch.Draw(thePIXEL, bar, color);

                    color = Color.Lerp(Color.Gray, Color.Blue, ciudad.jugador.magic / Player.MaxMagic);
                    bar = magicBar;
                    bar.Width = (int)Math.Round(ciudad.jugador.magic * ((float)magicBar.Width) / Player.MaxMagic);
                    spriteBatch.Draw(thePIXEL, bar, color);
                }
                else if (EstadoDeJuego.onWin()) spriteBatch.Draw(youWin, screen, Color.White);
                else spriteBatch.Draw(youLoose, screen, Color.White);

                spriteBatch.End();
                #endregion
            }
            base.Draw(gameTime);
        }

        private void ambiente(Model3D modelo)
        {
            modelo.Initialize();
            modelo.setAmbientalLight( new Color(10, 10, 30));
            modelo.setLightColor(new Color(128, 128, 190), 0);
            modelo.setLight(true, 1);
            modelo.setLightDir(Vector3.Down, 1);
            modelo.setLight(false, 2);
        }
        private Random rand = new Random();
        private float random(float a, float b)
        {
            return ((float)rand.NextDouble()) * (b - a) + a;
        }
        /*
        struct ParticleEmiter
        {
            public TimeSpan remaining;
            public TimeSpan totalTime;
            public Particulas.ParticleManager.Types tipo;
            public Vector3 position;
            public Camera cam;

            public ParticleEmiter(TimeSpan remaining, TimeSpan totalTime, Particulas.ParticleManager.Types tipo, Vector3 position, Camera cam)
            {
                this.remaining = remaining;
                this.totalTime = totalTime;
                this.tipo = tipo;
                this.position = position;
                this.cam = cam;
            }

            public void Update(GameTime gameTime)
            {
                Vector3 pos = position;
                if(cam != null)
                    pos += cam.CameraPosition;

                remaining -= gameTime.ElapsedGameTime;
                if (remaining.TotalMilliseconds <= 10)
                {
                    remaining = totalTime;
                    Particulas.ParticleManager.addParticle(pos, Vector3.Zero, tipo);
                }
            }
        }
        */
    }
}
