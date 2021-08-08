using IA8p.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA8p
{
   
        public class Matriz
        {
            public int[][] matriz;
            int pts;
            int lado;
            public Matriz()
            {
                matriz = new int[3][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
                pts = 0;
                lado = 0;
            }
            public Matriz(int[][] m, int score,int dir) 
            { 
                matriz = m; 
                pts = score;
                lado = dir;
            }

            public bool Final()
            {
                bool fim = true;
                int[,] final = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (matriz[i][j] != final[i, j])
                            fim = false;
                    }
                }
                return fim;
            }

            /*Calcula a distancia de cada peça de forma individual*/
            public int Distancia()
            {
                int desordem = 0;
                int[] xy = new int[2];      
                int[,] final = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        xy = buscarNum(final[i, j]);
                        int a = i - xy[0];
                        int b = j - xy[1];
                        desordem += Math.Abs(a + b);
             
                    }
                }
                return desordem;
            }

            /*Faz uma comparação do quadro atual com o quadro ideal/final*/
            public int Desordem()
            {
                int desordem = 0;
                int[,] final = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        desordem += Math.Abs(matriz[i][j] - final[i, j]);                      
                    }
                }
                return desordem;
            }

            public int[] buscarNum(int num) // Informa a localização (x,y)
            {
                int[] coor = new int[2];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (matriz[i][j] == num)
                        {
                            coor[0] = i;
                            coor[1] = j;
                        }
                    }
                }

                return coor;
            }

            /* Lados Mapeados
            4↑
            2↓
            3→
            1←
            */

            /*pos[lin,col]
              dirAnt p/ Evitar ir voltar pro mesmo lugar*/

            public void Embaralha(int Fim)
            {
                int ant =10;
                Random rdn = new Random();
                int r = 0;
                for (int i = 0; i < Fim; i++)
                {
                    r = rdn.Next(1,5);
                    
                    if (Math.Abs(ant - r) == 2)
                        i--;
                    else
                        Move(r, ant);
                    ant = r;
                }
            }
            public bool Move(int dir, int dirAnt)
            {

                Boolean flag = false;
                int[] pos = buscarNum(0);/* Retornar linha e Coluna onde esta o Zero*/
                if (Math.Abs(dir - dirAnt) != 2)
                {
                    switch (dir)
                    {
                        case 1:
                            if (pos[1] > 0)/*Esquerda*/
                            {
                                int aux = matriz[pos[0]][pos[1]];
                                matriz[pos[0]][pos[1]] = matriz[pos[0]][pos[1] - 1];
                                matriz[pos[0]][pos[1] - 1] = aux;
                                flag = true;
                            } break;
                        case 3:
                            if (pos[1] < 2)/*Direita*/
                            {
                                int aux = matriz[pos[0]][pos[1]];
                                matriz[pos[0]][pos[1]] = matriz[pos[0]][pos[1] + 1];
                                matriz[pos[0]][pos[1] + 1] = aux;
                                flag = true;
                            } break;
                        case 4:
                            if (pos[0] > 0)/*Cima*/
                            {
                                int aux = matriz[pos[0]][pos[1]];
                                matriz[pos[0]][pos[1]] = matriz[pos[0] - 1][pos[1]];
                                matriz[pos[0] - 1][pos[1]] = aux;
                                flag = true;
                            } break;
                        case 2:

                            if (pos[0] < 2)/*Baixo*/
                            {
                                int aux = matriz[pos[0]][pos[1]];
                                matriz[pos[0]][pos[1]] = matriz[pos[0] + 1][pos[1]];
                                matriz[pos[0] + 1][pos[1]] = aux;
                                flag = true;
                            } break;
                    }
                }
                else
                    MessageBox.Show("Quis Ir p/ Msm Lugar");

                return flag;
            }
        
    }
}
