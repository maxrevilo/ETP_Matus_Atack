using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ETP_Matus_Atack
{
    public class Geom
    {

          
        /// <summary>
        /// indica la direccion en la que A es tocado por b (inclusive si se solapan).
        /// </summary>
        /// <param name="A">Rectangulo A</param>
        /// <param name="B">Rectangulo B</param>
        /// <returns>"u" -> si A es contactado por arriba.
        /// "d" -> si A es contactado por abajo. 
        /// "l" -> si A es contactado por la izquierda. 
        /// "r" -> si A es contactado por la derecha.</returns>
        public static char indicarDireccion(Rectangle A, Rectangle B)
        {
            return indicarDireccion(A, B, Rectangle.Intersect(A,B) );
        }
        
        /// <summary>
        /// indica la direccion en la que A es tocado por B (inclusive si se solapan)
        /// ya habiendose especificado el rectangulo interseccion entre A y B.
        /// </summary>
        /// <param name="A">Rectangulo A</param>
        /// <param name="B">Rectangulo B</param>
        /// <param name="Inter">Rectangulo interseccion</param>
        /// <returns>"u" -> si A es contactado por arriba.
        /// "d" -> si A es contactado por abajo. 
        /// "l" -> si A es contactado por la izquierda. 
        /// "r" -> si A es contactado por la derecha.</returns>
        public static char indicarDireccion(Rectangle A, Rectangle B, Rectangle Inter)
        {
            //A NO contiene horizontalmente a B.
            Boolean H = !(A.Right >= B.Right && A.Left <= B.Left);
            //A NO contiene Verticalmente a B
            Boolean V = !(A.Top <= B.Top && A.Bottom >= B.Bottom);

            float tamro= (float)A.Height/A.Width;

            if (H && V)
                if (Inter.Height <= Inter.Width) return solV(A, B);
                else return solH(A, B);
            else if (H) return solH(A, B);
            else if (V) return solV(A, B);
            else
            {//Totalmente conteido
                if (Math.Abs(B.Center.Y - A.Center.Y) >= tamro * Math.Abs(B.Center.X - A.Center.X))
                    return solV(A, B);
                else
                    return solH(A, B);
            }
        }

        private static char solH(Rectangle A, Rectangle B)
        {
            if (A.Center.X >= B.Center.X) return 'l';
            else return 'r';
        }

        private static char solV(Rectangle A, Rectangle B)
        {
            if (A.Center.Y >= B.Center.Y) return 'u';
            else return 'd';
        }



        /// <summary>
        /// Distancia entre dos puntos p1(x1, y1) y p2(x2, y2).
        /// </summary>
        /// <param name="x1">Coordenada x del primer punto</param>
        /// <param name="y1">Coordenada y del primer punto</param>
        /// <param name="x2">Coordenada x del segundo punto</param>
        /// <param name="y2">Coordenada y del segundo punto</param>
        /// <returns>un flotante representando la distancia entre los dos puntos</returns>
        public static float dist(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        /// <summary>
        /// Distancia entre dos puntos p1 y p2
        /// </summary>
        /// <param name="p1">Punto uno</param>
        /// <param name="p2">Punto dos</param>
        /// <returns>un flotante representando la distancia entre los dos puntos</returns>
        public static float dist(Point p1, Point p2)
        {
            return dist(p1.X, p1.Y, p2.X, p2.Y);
        }

        /// <summary>
        /// Modulo de el punto p1 (o distancia al punto po(0,0)
        /// </summary>
        /// <param name="p1">Punto a calcular el modulo</param>
        /// <returns>flotante representando el mudulo de p1</returns>
        public static float modulo(float x1, float y1)
        {
            return (float)Math.Sqrt(x1*x1 + y1*y1);
        }

        /// <summary>
        /// Modulo de el punto p1 (o distancia al punto po(0,0)
        /// </summary>
        /// <param name="p1">Punto a calcular el modulo</param>
        /// <returns>flotante representando el mudulo de p1</returns>
        public static float modulo(Point p1)
        {
            return modulo(p1.X, p1.Y);
        }

        /// <summary>
        /// Crea una ecuacion que va desde <paramref name="Xi"/> hasta <paramref name="Xf"/> como los valores 
        /// de <paramref name="X"/> y <paramref name="Yi"/> hasta <paramref name="Yf"/> como el rango de 
        /// valores de salida, esta ecuacion es evaluada en <paramref name="x"/> y retorna un double.
        /// 
        /// Para <paramref name="x"/>=<paramref name="Xi"/> la funcion retorna <paramref name="Yi"/>.
        /// Para <paramref name="x"/>=<paramref name="Xf"/> la funcion retorna <paramref name="Yf"/>.
        /// 
        /// El parametro <paramref name="curvatura"/> define la curvatura de la ecuacion, si es 1 la ecuacion es
        /// lineal, si es mayor a 1 es concava hacia abajo, si es menor a 1 es concavo hacia arriba.
        /// </summary>
        /// <param name="x">Punto donde se evaluara la ecuacion</param>
        /// <param name="Xi">Minimo valor para evaluar la ecuacuion (si x es menor a Xi el resultado sera Yi)</param>
        /// <param name="Xf">Maximo valor para evaluar la ecuacuion (si x es mayor a Xf el resultado sera Yf)</param>
        /// <param name="Yi">Minimo valor que devolvera la ecuacion</param>
        /// <param name="Yf">Maximo valor que devolvera la ecuacion</param>
        /// <param name="curvatura">Curvatura de la ecuacion</param>
        /// <returns>Un doble que representa el valor de la ecuacion evaluada en <paramref name="x"/></returns>
        public static double ecuacionDeCurva(double x, double Xi, double Xf, double Yi, double Yf, double curvatura)
        {
            if (x <= Xi) return Yi;
            else if (x >= Xf) return Yf;
            else
            {
                double ancho = Math.Pow((Math.Pow(Yf, 1 / curvatura) - Math.Pow(Yi, 1 / curvatura)) / (Xi - Xf), curvatura);
                double corrido = Math.Pow(Yi / ancho, 1 / curvatura) + Xi;

                return ancho * Math.Pow(corrido - x, curvatura);
            }
        }
    }
}
