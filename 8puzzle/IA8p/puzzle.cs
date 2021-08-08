using IA8p.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA8p
{
    public partial class puzzle : Form
    {
        public puzzle()
        {
            InitializeComponent();
        }
        public void Embaralha()
        {
        }
        private void bttIniciar_Click(object sender, EventArgs e)
        {
            Questao q = new Questao();
            q.ShowDialog();
            int E =(int) q.qnt;

            int[][] aux = new int[3][] { new int[] { 1, 2, 3 }, new int[] { 4,5,6 }, new int[] { 7,8,0 } };
           
            aux = new int[3][] { new int[] { 1,0,3 }, new int[] { 4,2,5 }, new int[] { 7,8,6 } }; /*Concluido Teste 3*/
            Matriz m = new Matriz(aux, -1, 0);
            do{m.Embaralha(E);} while (m.Distancia() + m.Desordem() == 0);
               
            MatrizToString(m.matriz); 
            Pilha P = ExecutaJogo(m);//Este metodo vai retornar uma Lista contendo todos os passos empilhados
            
        }
        
       
        /* Lados Mapeados 4↑ 2↓ 3→ 1← */

        public void copyMatriz(int[][] a, int[][] b)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    a[i][j] = b[i][j];
                }
            }
        }
        public Pilha ExecutaJogo(Matriz matriz)
        {
            Pilha p = new Pilha();
            Matriz Aux = new Matriz();
            List<string> ListaDir = new List<string>();
            Matriz matrizRel = new Matriz();
            int DesordemRelatorio = 0; ;
            int[] vDir = new int[4] { -1, -1, -1, -1 };
            int[] vLocZero = new int[2] { 0, 0 };
            int[][] FINAL = new int[3][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 0 } };
            p.push(matriz);
            int dir = -10;
            int AtualDir = 0;
            int vel = 1;/* Velocidade de atualização, 50 = 20FPS */
            CarregaQuadro(matriz.matriz);
            bool Informada = true;
            bool passos = false;

            /* Força Bruta
            if (MessageBox.Show("Deseja usar a busca por força bruta?", "Mensage do sistema ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Informada = false;
            }*/
            if (MessageBox.Show("Deseja acompanhar o passo-a-passo?", "Mensage do sistema ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                passos = true;
            }
            else
            {
                /*Custom Time here
                Velocidade z = new Velocidade();
                z.ShowDialog();
                vel = (int)z.n;
                */
            }

            MessageBox.Show(MatrizToString(matriz.matriz) + "\nFator: " + (Aux.Desordem() + Aux.Distancia()));
            int ContPassos = 0; int fact = 50;

            var time = new Stopwatch();
            time.Start();
            while (matriz.Final() != true)
            {
                //matriz = p.pop();
                Passos.Text = "Passos:" + ContPassos + " - Tempo Decorrido:" + time.Elapsed.ToString(@"hh\:mm\:ss");
                if (Informada)
                {
                    copyMatriz(Aux.matriz, matriz.matriz);
                    if (dir != 3 && Aux.Move(1, dir))
                        vDir[0] = Aux.Desordem() + Aux.Distancia() + Visitado(ArrayToString(Aux.buscarNum(0)), ListaDir);// Esquerda

                    copyMatriz(Aux.matriz, matriz.matriz);
                    if (dir != 4 && Aux.Move(2, dir))
                        vDir[1] = Aux.Desordem() + Aux.Distancia() + Visitado(ArrayToString(Aux.buscarNum(0)), ListaDir); //Baixo

                    copyMatriz(Aux.matriz, matriz.matriz);
                    if (dir != 1 && Aux.Move(3, dir))
                        vDir[2] = Aux.Desordem() + Aux.Distancia() + Visitado(ArrayToString(Aux.buscarNum(0)), ListaDir);//Direita

                    copyMatriz(Aux.matriz, matriz.matriz);
                    if (dir != 2 && Aux.Move(4, dir))
                        vDir[3] = Aux.Desordem() + Aux.Distancia() + Visitado(ArrayToString(Aux.buscarNum(0)), ListaDir);//Cima

                    AtualDir = Menor(vDir);

                    DesordemRelatorio = matriz.Desordem() + Aux.Distancia() + Visitado(ArrayToString(Aux.buscarNum(0)), ListaDir);
                    copyMatriz(Aux.matriz, matriz.matriz);
                    matriz.Move(AtualDir, dir);

                    dir = AtualDir;

                    p.push(matriz);
                }
                else
                {
                    Random rdn = new Random();
                    AtualDir = rdn.Next(1, 5);
                    matriz.Move(AtualDir, dir);
                }
               
                CarregaQuadro(matriz.matriz);

                string str = (MatrizToString(Aux.matriz) + "\nFator: " + DesordemRelatorio + "\nDireção:_" + dir + "\n" + ArrayToString(vDir)).Replace("_2", "Baixo").Replace("_1", "Esquerda").Replace("_3", "Direita").Replace("_4", "Cima") + "\n[E][B][D][C]\n\n" + MatrizToString(matriz.matriz)+"\n\nDeseja Continuar?";
                
                if (passos)
                {
                    if (MessageBox.Show(str, "Mensage do sistema ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return null;
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(vel);
                    this.Refresh();

                    Application.DoEvents();
                }

                if (Informada)
                {
                    vDir = new int[4] { -1, -1, -1, -1 };
                    vLocZero = matriz.buscarNum(0);
                    ListaDir.Add(ArrayToString(vLocZero));
                }
                ContPassos++;
                if (ContPassos % fact == 0)
                {
                    matriz.Embaralha(10);
                    fact = fact * 2;
                }
               
            }
            Passos.Text = "Passos:" + ContPassos + " - Tempo Decorrido:" + time.Elapsed.ToString(@"hh\:mm\:ss");
            time.Stop();
            MessageBox.Show("FIM");
            
            return p;
        }
        public void CarregaQuadro(int[][]m)
        {
            pic1.ImageLocation = Application.StartupPath + "/Imgs/" + m[0][0] + ".jpg";
            pic2.ImageLocation = Application.StartupPath + "/Imgs/" + m[0][1] + ".jpg";
            pic3.ImageLocation = Application.StartupPath + "/Imgs/" + m[0][2] + ".jpg";
            pic4.ImageLocation = Application.StartupPath + "/Imgs/" + m[1][0] + ".jpg";
            pic5.ImageLocation = Application.StartupPath + "/Imgs/" + m[1][1] + ".jpg";
            pic6.ImageLocation = Application.StartupPath + "/Imgs/" + m[1][2] + ".jpg";
            pic7.ImageLocation = Application.StartupPath + "/Imgs/" + m[2][0] + ".jpg";
            pic8.ImageLocation = Application.StartupPath + "/Imgs/" + m[2][1] + ".jpg";
            pic9.ImageLocation = Application.StartupPath + "/Imgs/" + m[2][2] + ".jpg";
        }
        public string MatrizToString(int[][] a)
        {
            string Result = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Result += "[" + a[i][j] + "]";
                }
                Result += "\n";
            }
            return Result;
        }
        public string ArrayToString(int[] a)
        {
            string Result = "";
            for (int i = 0; i < a.Length; i++)
            {
                Result += "[" + a[i] + "]";
            }
            return Result;
        }


        public int Menor(int[] v)
        {

            for (int i = 0; i < 4; i++)
            {
                if (v[i] == -1)
                    v[i] = 1000;
            }

            //----------Origin
            int pos = 0;
            int Menor = 1000;
            if(v[0]!=-1)
                Menor = v[0];//Esquerda
            if(v[1]< Menor && v[1] != -1)
            {
                Menor = v[1];//Baixo
                pos = 1;
            }            
            if (v[2] < Menor && v[2] != -1)
            {
                Menor = v[2];//Direita
                pos = 2;
            }
            if (v[3] < Menor && v[3] != -1)
            {
                Menor = v[3];//Cima
                pos = 3;
            }
            //-------------------------F
                      
            for (int i = 0; i < 4; i++)
            {
                if (v[i] > 999)
                    v[i] = -1;
            }
            return pos + 1;

        }

        int Visitado(string xy, List<string> L)
        {
            int r=0;
            for (int i = 0; i < L.Count; i++)
            {
                if (xy == L[i])
                    r++;
            }
            return r;
        }

        private void puzzle_Load(object sender, EventArgs e)
        {
            pictureBox9.ImageLocation = Application.StartupPath+@"/Imgs/Black2.jpg";           
        }
       
    }
}
