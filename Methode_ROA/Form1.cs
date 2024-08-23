using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Methode_ROA;

namespace Methode_ROA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }     
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Methodes.IsNumeric(e.KeyChar.ToString());
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Methodes.IsNumeric(e.KeyChar.ToString());
        }

        int nbreCentreProd = 0;
        int nbreCentreDist = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            nbreCentreProd = Convert.ToInt16(TxtNbreCP.Text);
            nbreCentreDist = Convert.ToInt16(txtNbreCD.Text);
            Methodes.DessinerPlan(PnlMatrice, nbreCentreProd, nbreCentreDist);

        }
 
        private void RdEltMinimal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TableauTransport tabTranst = new TableauTransport(nbreCentreProd, nbreCentreDist);
                tabTranst = Methodes.LireTableau(PnlMatrice, nbreCentreProd, nbreCentreDist);
        

                    if (Methodes.SommeQuantites(tabTranst.Demandes) > Methodes.SommeQuantites(tabTranst.Offres))
                        MessageBox.Show("Pas de solution optimale", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {      
                        tabTranst = Algos.CoutMinimal(tabTranst);
                        Trace ftrace = new Trace(Methodes.TraceAlgo(tabTranst," METHODE D'ELEMENT MINIMAL ") + new SteppingStone(tabTranst).Trace);
                        ftrace.Show();
                    }
            }
            catch (Exception)
            {
                
            }

        }

        private void RdCoinNordOuest_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TableauTransport tabTranst = new TableauTransport(nbreCentreProd, nbreCentreDist);

                tabTranst = Methodes.LireTableau(PnlMatrice, nbreCentreProd, nbreCentreDist);

                    if (Methodes.SommeQuantites(tabTranst.Demandes) > Methodes.SommeQuantites(tabTranst.Offres))
                        MessageBox.Show("Pas de solution optimale", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        tabTranst = Algos.CoinNordOuest(tabTranst);
                        // tabTrans = new SteppingStone(tabTranst).Optimisation();
                        Trace ftrace = new Trace(Methodes.TraceAlgo(tabTranst, " METHODE DE COIN NORD OUEST ") + new SteppingStone(tabTranst).Trace);
                        ftrace.Show();
                    }
            }
            catch (Exception)
            {
 
            }
          
        }

        private void BtPontentiel_Click(object sender, EventArgs e)
        {
            //TableauTransport tabTrans = new TableauTransport(nbreCentreProd, nbreCentreDist);

            //tabTrans = Methodes.LireTableau(PnlMatrice, nbreCentreProd, nbreCentreDist);

            //tabTrans = new SteppingStone(tabTrans).Optimisation();
            //Trace ftrace = new Trace(Methodes.TraceAlgo(tabTrans, " METHODE DE COIN NORD OUEST ") + new SteppingStone(tabTrans).Trace);
            //ftrace.Show();
        }
    }
}
