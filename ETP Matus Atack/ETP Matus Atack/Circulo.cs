using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ETP_Matus_Atack
{
    public struct Circulo
    {
        //Paso 2: Definir las variables.
        #region Variables:
        /// <summary>
        /// Radio del circulo, la mitad del diametro.
        /// </summary>
        public int Radio;
        /// <summary>
        /// Ubicacion de la Coordenada X del centro del Circulo.
        /// </summary>
        public int X;
        /// <summary>
        /// Ubicacion de la Coordenada Y del centro del Circulo.
        /// </summary>
        public int Y;

        /// <summary>
        /// Punto que indica el centro del Circulo.
        /// </summary>
        public Point Centro
        {
            get { return new Point(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        #endregion




        //Paso 3: Definir el constructor:

        #region Constructores:
        /// <summary>
        /// Crea un circulo indicando las coordenadas de su centro y su radio.
        /// </summary>
        /// <param name="X">Coordenada X del Centro</param>
        /// <param name="Y">Coordenada Y del Centro</param>
        /// <param name="Radio">Radio del circulo</param>
        public Circulo(int X, int Y, int Radio)
        {
            this.X = X;
            this.Y = Y;
            this.Radio = Radio;
        }
        #endregion


        #region Funciones:

        //Paso 7: Crear el BoundingRect para graficar.
        /// <summary>
        /// El BoundingRect es el rectangulo de menor tamaño
        /// que encierra el circulo.
        /// </summary>
        /// <returns>Retorna un "Rectangle" que representa el BoundingRect</returns>
        public Rectangle BoundingRect()
        {
            return new Rectangle(X - Radio, Y - Radio, 2 * Radio, 2 * Radio);
        }

        //Paso 4: Interceptar Circulos (Distancia entre Centros).

        /// <summary>
        /// Determina la distancia entre este el centro de este circulo y otro.
        /// </summary>
        /// <param name="circuloB">El otro circulo a comparar.</param>
        /// <returns>Un int que indica aproximadamente la distancia</returns>
        public int Distancia(Circulo circuloB)
        {
            int difX = circuloB.X - X;
            int difY = circuloB.Y - Y;

            int distC = difX * difX + difY * difY;

            return (int)Math.Sqrt(distC);
        }

        //Paso 9: Contencion (Circulos)

        /// <summary>
        /// Determina cuando este Circulo contiene completamente a otro.
        /// </summary>
        /// <param name="circuloB">El Circulo que debe estar contenido.</param>
        /// <returns>Un bool indicando si este Circulo contiene al CirculoB o no</returns>
        public bool Contiene(Circulo circuloB)
        {
            return Distancia(circuloB) + circuloB.Radio <= Radio;
        }

        //Paso 10: Contencion (Puntos)

        /// <summary>
        /// Determina cuando este Circulo contiene a un punto.
        /// </summary>
        /// <param name="punto">El Punto que debe estar contenido.</param>
        /// <returns>Un bool indicando si este Circulo contiene al Punto o no</returns>
        public bool Contiene(Point punto)
        {
            return Contiene(new Circulo(punto.X, punto.Y, 0));
        }

        //Paso 11: Contencion (Rectangulo)

        /// <summary>
        /// Determina cuando este Circulo contiene a un Rectangulo.
        /// </summary>
        /// <param name="punto">El Rectangulo que debe estar contenido.</param>
        /// <returns>Un bool indicando si este Circulo contiene al Rectangulo o no</returns>
        public bool Contiene(Rectangle rect)
        {
            //El Circulo Contiene todas las 4 esquinas del Rectangulo.
            Point[] esquinas = new Point[4]
                       {new Point(rect.Left, rect.Top),
                        new Point(rect.Right, rect.Top),
                        new Point(rect.Left, rect.Bottom),
                        new Point(rect.Right, rect.Bottom)};

            foreach (Point p in esquinas)
            {
                if (!Contiene(p))
                {
                    return false;
                }
            }

            return true;
        }

        //Paso 4.2: Interceptar Circulos.

        /// <summary>
        /// Determina cuando este Circulo Intersepta o Contiene a otro.
        /// </summary>
        /// <param name="circuloB">El otro circulo a comparar</param>
        /// <returns>Un bool indicando si hay Intereseccion o no</returns>
        public bool Intercepta(Circulo circuloB)
        {
            return Distancia(circuloB) <= Radio + circuloB.Radio;
        }

        //Paso 17: Interseccion Circulo-Rectangulo.

        /// <summary>
        /// Determina cuando este Circulo Intersecta o Contiene a un Rectangulo.
        /// </summary>
        /// <param name="rect">El otro rectangulo a comparar</param>
        /// <returns>Un bool indicando si hay Intereseccion o no</returns>
        public bool Intercepta(Rectangle rect)
        {
            //El Rectangulo contiene el centro del Circulo:
            if (rect.Contains(Centro))
            {
                return true;
            }

            //El Circulo Contiene alguna de las 4 esquinas del Rectangulo.
            Point[] esquinas = new Point[4]
                       {new Point(rect.Left, rect.Top),
                        new Point(rect.Right, rect.Top),
                        new Point(rect.Left, rect.Bottom),
                        new Point(rect.Right, rect.Bottom)};

            foreach (Point p in esquinas)
            {
                if (Contiene(p))
                {
                    return true;
                }
            }

            //El Circulo toca alguno de los bordes rectos del Rectangulo:
            //Bordes laterales:
            if (rect.Top <= Y && rect.Bottom >= Y)
            {
                if (MathHelper.Distance(rect.Left, X) <= Radio || MathHelper.Distance(rect.Right, X) <= Radio)
                {
                    return true;
                }
            }

            //Bordes Superior e Inferior:
            if (rect.Left <= X && rect.Right >= X)
            {
                if (MathHelper.Distance(rect.Bottom, Y) <= Radio || MathHelper.Distance(rect.Top, Y) <= Radio)
                {
                    return true;
                }
            }


            return false;
        }

        #endregion
    }
}
