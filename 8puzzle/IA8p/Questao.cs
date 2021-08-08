using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA8p
{
    public partial class Questao : Form
    {
        public int qnt;
        public Questao()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                qnt = Convert.ToInt32(txtEmbaralha.Text);
                if (qnt > 30)
                    qnt = 30;
                if (qnt < 1)
                    qnt = 1;
                Close();
            }
            catch(Exception D)
            {

            }
            
        }

        private void Questao_Load(object sender, EventArgs e)
        {

        }
    }
}
