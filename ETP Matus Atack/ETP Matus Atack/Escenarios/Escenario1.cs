using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// No se cuales son los que se usan
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
    public class Escenario1:Escenario
    {
        public Escenario1(Game game, Motor_Colisiones Motor,Player jugador)
            :base(game,Motor,jugador)
        {
            Colisionable = Motor;
            Initialize();

        }

        public void Initialize()
        {
            base.Initialize(FunAuxA("Content/Posiciones_Enemigos/Escenario1.txt"), 
                            FunAuxA("Content/Posiciones_Edificios/Escenario1.txt"), 
                            FunAuxB("Content/Escalas_Enemigos/Escenario1.txt"), 
                            FunAuxB("Content/Escalas_Edificios/Escenario1.txt"), 
                            FunAuxC("Content/Modelos/Modelo_Enemigos.txt"),
                            FunAuxC("Content/Modelos/Modelo_Edificios.txt"));
            base.Inicializar_Edificios();
            base.Inicializar_Enemigos();
            Colisionable.FunAuxRec("Content/Zonas_Prohibidas/Escenario1.txt");
        }

        private Vector3[] FunAuxA(string filename)
        {
            //string path = Path.Combine(StorageContainer.TitleLocation, filename);
            Vector3[] Temp;
            int i = 0;
            string line = "";
            String[] Vector;
            //using (StreamReader tr = new StreamReader(path))
            using (StreamReader tr = new StreamReader(TitleContainer.OpenStream(filename)) )
            {
                line = tr.ReadLine();
                Temp = new Vector3[int.Parse(line)];
                line = tr.ReadLine();

                while (line != null)
                {
                    Vector = line.Split(' ');
                    Temp[i] = new Vector3(float.Parse(Vector[0]), float.Parse(Vector[1]), float.Parse(Vector[2]));
                    i++;
                    line = tr.ReadLine();
                }
            }
            return Temp;
        }
        private float[]   FunAuxB(string filename)
        {
            //string path = Path.Combine(StorageContainer.TitleLocation, filename);
            float[] Temp;
            int i = 0;
            string line = "";
            //using (StreamReader tr = new StreamReader(path))
            using (StreamReader tr = new StreamReader(TitleContainer.OpenStream(filename)))
            {
                line = tr.ReadLine();
                Temp = new float[int.Parse(line)];
                line = tr.ReadLine();

                while (line != null)
                {

                    Temp[i] = float.Parse(line);
                    i++;

                    line = tr.ReadLine();
                }
            }
            return Temp;
        }
        private int[]     FunAuxC(string filename)
        {
            //string path = Path.Combine(StorageContainer.TitleLocation, filename);
            int[] Temp;
            int i = 0;
            string line = "";
            //using (StreamReader tr = new StreamReader(path))
            using (StreamReader tr = new StreamReader(TitleContainer.OpenStream(filename)))
            {
                line = tr.ReadLine();
                Temp = new int[int.Parse(line)];
                line = tr.ReadLine();

                while (line != null)
                {

                    Temp[i] = int.Parse(line);
                    i++;

                    line = tr.ReadLine();
                }
            }
            return Temp;
        }
    }
}
