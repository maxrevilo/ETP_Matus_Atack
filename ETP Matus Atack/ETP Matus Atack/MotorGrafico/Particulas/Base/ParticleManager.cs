using System;
using Microsoft.Xna.Framework;

namespace ETP_Matus_Atack.Particulas
{
    public class ParticleManager
    {
        NieveParticleSystem nieve;
        VentiscaParticleSystem ventisca;
        SplashParticleSystem splash;
        ExplosionSmokeParticleSystem explotion;

        public enum Types
        {
            fire,
            cold,
            rain
        }

        private static ParticleManager singlenton;
        public static void Iniciar(Game game)
        {
            singlenton = new ParticleManager(game);
        }

        private ParticleManager(Game game)
        {

            nieve = new ETP_Matus_Atack.Particulas.NieveParticleSystem(game, game.Content);

            game.Components.Add(nieve);


            ventisca = new VentiscaParticleSystem(game, game.Content);

            game.Components.Add(ventisca);


            splash = new SplashParticleSystem(game, game.Content);

            game.Components.Add(splash);


            explotion = new ExplosionSmokeParticleSystem(game, game.Content);

            game.Components.Add(explotion);
        
        }

        public static void addParticle(Vector3 pos, Vector3 vel, Types tipo)
        {
            switch(tipo){
                case Types.cold:
                    addVentisca(pos, vel);
                    break;
                case Types.fire:
                    addExplotion(pos, vel);
                    break;
                case Types.rain:
                    break;
            }
        }

        public static void addNieve(Vector3 pos, Vector3 vel)
        {
            singlenton.nieve.AddParticle(pos, vel);
        }

        public static void addVentisca(Vector3 pos, Vector3 vel)
        {
            singlenton.ventisca.AddParticle(pos, vel);
        }

        public static void addSplash(Vector3 pos, Vector3 vel)
        {
            singlenton.splash.AddParticle(pos, vel);
        }

        public static void addExplotion(Vector3 pos, Vector3 vel)
        {
            singlenton.explotion.AddParticle(pos, vel);
        }

        public static void Draw(Camera camara)
        {
            singlenton.nieve.SetCamera(camara.ViewMatrix, camara.ProjectionMatrix);
            singlenton.ventisca.SetCamera(camara.ViewMatrix, camara.ProjectionMatrix);
            singlenton.splash.SetCamera(camara.ViewMatrix, camara.ProjectionMatrix);
            singlenton.explotion.SetCamera(camara.ViewMatrix, camara.ProjectionMatrix);
        }

    }
}
