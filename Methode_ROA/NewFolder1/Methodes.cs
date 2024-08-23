using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Methode_ROA
{
    static public class Methodes
    {
        static public Regret RegretMaximum(List<Regret> regLig)
        {
            Regret regret = new Regret(-1);

            foreach (Regret reg in regLig)
            {
                if (reg.Valeur > regret.Valeur) regret = reg;
            }

            return regret;
        }

        static public float SommeQuantites(List<float> list)
        {
            float somme = 0;

            foreach (float element in list)
            {
                somme += element;
            }

            return somme;
        }

        static public TableauTransport AjusterOffresDemandes(TableauTransport tt)
        {
            float quantiteSupp = SommeQuantites(tt.Offres) - SommeQuantites(tt.Demandes);

            if (quantiteSupp > 0)
            {
                tt.Demandes.Add(quantiteSupp);
                tt.NombreColonne++;

                for (int i = 0; i < tt.NombreLigne; i++)
                {
                    int index = i * tt.NombreColonne + tt.NombreColonne - 1;
                    tt.Cellules.Insert(index, new Cellule(i, tt.NombreColonne - 1 , 0));
                }
            }

            return tt;
        }

        static public double CoutOptimal(TableauTransport tt)
        {
            double coutOpt = 0;

            foreach (Cellule cell in tt.Cellules)
            {
                coutOpt += cell.Quantite * cell.Cout;
            }

            return coutOpt;
        }


        static public int InsertInIndex(Regret reg, List<Regret> list, int dim)
        {
            Regret newNext = new Regret(0, new Cellule(1000, 1000, 0));

            int index = 0;

            if (dim == 0)
            {
                foreach (Regret regret in list)
                {
                    if (regret.Cellule.Ligne > reg.Cellule.Ligne)
                        newNext = regret;
                }

                if (newNext.Cellule.Ligne == 1000) index = -1;
                else index = list.IndexOf(newNext);
            }

            else
            {
                foreach (Regret regret in list)
                {
                    if (regret.Cellule.Colonne > reg.Cellule.Colonne)
                        newNext = regret;
                }

                if (newNext.Cellule.Colonne == 1000) index = -1;
                else index = list.IndexOf(newNext); ;
            }

            return index;
        }

        static public int InsertIn(int dim, Regret reg, List<Regret> list)
        {
            Regret regNext = new Regret(0, new Cellule(1000, 1000, 0));

            int index = 0;

            if (dim == 0)
            {
                foreach (Regret r in list)
                {
                    if (r.Cellule.Ligne > reg.Cellule.Ligne)
                    {
                        regNext = r;
                        break;
                    }
                }

                if (regNext.Cellule.Ligne == 1000) index = -1;
                else index = list.IndexOf(regNext);
            }

            else
            {
                foreach (Regret r in list)
                {
                    if (r.Cellule.Colonne > reg.Cellule.Colonne)
                    {
                        regNext = r;
                        break;
                    }
                }

                if (regNext.Cellule.Colonne == 1000) index = -1;
                else index = list.IndexOf(regNext);
            }

            return index;
        }

        static public string TraceAlgo(TableauTransport tabTrans, string enteteMethode)
        {
            DirectoryInfo getFolder = new DirectoryInfo(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "DCss"));

            string chaine = "<html><head><link rel=\"stylesheet\"  href=\"" + getFolder + "\\Styles.css\" /></head><body><div id=\"idEntete\">  "+enteteMethode+" </div>";

            chaine += "<table id=\"IdTable\"><tr><td></td>";

            for (int i = 1; i < tabTrans.NombreColonne+1 ; i++)
            {
                chaine += "<td id=\"MTitle\">M<sub>" + i + "</sub></td>";
            }

            chaine += "<td id=\"MTitle\"><p id=\"MTi\">a<sub>i</sub></p></td></tr>";
            int l = 0;
            for (int i = 0; i < tabTrans.NombreLigne; i++)
            {
                chaine += "<tr><td id=\"MTitless\"><p id=\"MTi\">P<sub>" + (i + 1) + "</sub></p></td>";
                for (int j = 0; j < tabTrans.NombreColonne +1 ; j++)
                {
                    if (j == tabTrans.NombreColonne) chaine += "<td id=\"MTits\">" + tabTrans.Offres[i] + "</td>";
                    else {
                        if (tabTrans.Cellules[l].Quantite==0)
                        {
                            chaine += "<td class=\"MTit\"><span id=\"trues\">" + tabTrans.Cellules[l].Cout + "</span><br /><span id=\"truess\">(" + tabTrans.Cellules[l].Quantite + ")</span></td>"; 
                        }
                        else chaine += "<td class=\"MTit\"><span id=\"trues\">" + tabTrans.Cellules[l].Cout + "</span><br /><span id=\"true\">(" + tabTrans.Cellules[l].Quantite + ")</span></td>";
                        l++;
                    }       
                }
                chaine += "</tr>";
            }

            chaine += "<tr><td id=\"MTitle\"><p id=\"MTi\">b<sub>j</sub></p></td>";

            for (int i = 0; i < tabTrans.NombreColonne+1; i++)
            {

                if (i < tabTrans.NombreColonne) chaine += "<td id=\"MTits\">" + tabTrans.Demandes[i] + "</td>";

                else chaine += "<td id=\"MTits\">" + SommeQuantites(tabTrans.Demandes) + "</td>";           
            }
            chaine += "</tr></table>";

            return chaine;
        }

        static public TableauTransport LireTableau(Panel pan, int nbreCentreProd, int nbreCentreDistr)
        {
         
            TableauTransport tabTrans = new TableauTransport(nbreCentreProd, nbreCentreDistr);
            int Positioncomp = 0;
            foreach (TextBox textbox in pan.Controls.OfType<TextBox>())
            {
                if (Positioncomp % (nbreCentreDistr + 1) == nbreCentreDistr) tabTrans.Offres.Add(float.Parse(textbox.Text));

                else if (Positioncomp / (nbreCentreDistr + 1) == nbreCentreProd) tabTrans.Demandes.Add(float.Parse(textbox.Text));

                else tabTrans.Cellules.Add(new Cellule(Positioncomp / (nbreCentreDistr + 1), Positioncomp % (nbreCentreDistr + 1), float.Parse(textbox.Text)));
                
                Positioncomp++;
            }

            tabTrans.Offres.RemoveAt(tabTrans.Offres.Count-1);

            return tabTrans;
        }

        static public void DessinerPlan(Panel PanelPlant, int nbreCentreProd, int nbreCentreDist)
        {

           PanelPlant.Controls.Clear();

            TextBox NewTextBox;
            Label label;
            int newAbs = 50, newOrd = 50;
            Size dimTextBox = new Size(70, 30);
            Size dimLabel = new Size(70, 30);

         // Permet de dessiner le centre de distribution B(j) Pour tout j=1,.....,nbreCentreDistribution
            label = new Label();
         // label.BorderStyle = BorderStyle.FixedSingle;
            label.Font = new System.Drawing.Font("Times New Roman", 12);
            label.Text = "";
            label.Size = dimLabel;
            label.Location = new System.Drawing.Point(newAbs, newOrd);
            PanelPlant.Controls.Add(label);

            for (int j = 0; j < nbreCentreDist + 1; j++)
            {
                    label = new Label();
                 // label.BorderStyle = BorderStyle.FixedSingle;
                //label.BackColor = Color.Azure;
                    label.Font = new System.Drawing.Font("Times New Roman", 12);
                    label.Text = j < nbreCentreDist ? "B" + (j+1) : "ai";
                    label.Size = dimLabel;
                    newAbs += dimLabel.Width + 5;
                    label.Location = new System.Drawing.Point(newAbs, newOrd);
                    PanelPlant.Controls.Add(label);
            }

            newAbs = 50;
            for (int i = 0; i < nbreCentreProd + 1; i++)
            {
                // Permet de dessiner le centre de production A(i) Pour tout i=1,.....,nbreCentreProduction et Bj
            
                    label = new Label();
                    // label.BorderStyle = BorderStyle.FixedSingle;
                    //label.BackColor = Color.Azure;
                    label.Font = new System.Drawing.Font("Times New Roman", 12);
                    label.Text = i < nbreCentreProd ? "A" + (i + 1) : "bj";
                    label.Size = dimLabel;
                    newOrd += dimLabel.Height;
                    label.Location = new System.Drawing.Point(newAbs, newOrd);
                    PanelPlant.Controls.Add(label);
       
                for (int j = 0; j < nbreCentreDist + 1; j++)
                {
                  //Dessiner Matrice
                    NewTextBox = new TextBox();
                    NewTextBox.Size = dimTextBox;
                    NewTextBox.BorderStyle = BorderStyle.FixedSingle;
                    newAbs += dimTextBox.Width + 5;
                    NewTextBox.Location = new System.Drawing.Point(newAbs, newOrd);
                  //NewTextBox.Leave += new EventHandler(TxtNbreCP_Leave);
                    NewTextBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                    PanelPlant.Controls.Add(NewTextBox);
                }
                newAbs = 50;
            }
        }

        static void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !IsNumeric(e.KeyChar.ToString());
        }

        static public bool IsNumeric(string s)
        {
            Regex Rg = new Regex("[0-9\\b]");
            return Rg.IsMatch(s.ToString());
        }

        public static int NombreColonne { get; set; }
    }
}
