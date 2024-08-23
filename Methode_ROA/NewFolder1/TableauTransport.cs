using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methode_ROA
{
    public class TableauTransport
    {
        public int NombreLigne { get; set; }
        public int NombreColonne { get; set; }
        public double CoutOptimal { get; set; }
        public List<int> LignesSaturees { get; set; }
        public List<int> ColonnesSaturees { get; set; }
        public List<float> Offres { get; set; }
        public List<float> Demandes { get; set; }
        public List<Cellule> Cellules { get; set; }

        public bool EstOptimal()
        {
           bool ok = true;
           foreach (Cellule cell in Cellules)
           {
             if (cell.Delta < 0)
              {
                ok = false;
                 break;
               }
            }
            
            return ok;   
        }

        public void InitialisationDeltas()
        {
            foreach (Cellule cell in Cellules)
            {
                cell.Delta = 0;
            }
        }

        public TableauTransport(int nbreLig, int nbreCol)
        {
            NombreColonne = nbreCol;
            NombreLigne = nbreLig;
            LignesSaturees = new List<int>();
            ColonnesSaturees = new List<int>();
            Offres = new List<float>();
            Demandes = new List<float>();
            Cellules = new List<Cellule>();
        }

        public Cellule CoutMinimal()
        {
            Cellule minCell = new Cellule(1000000000);

            foreach (Cellule cell in this.Cellules)
            {
                if (!this.LignesSaturees.Contains(cell.Ligne) && !this.ColonnesSaturees.Contains(cell.Colonne) && cell.Cout < minCell.Cout)
                {
                    minCell = cell;
                }
            }

            return minCell;
        }

        public Cellule CoutMinimal(int dim, int pos)
        {
            Cellule minCell = new Cellule(10000000);

            int i = 0;

            if (dim == 0)
            {
                for (i = pos * NombreColonne; i < pos * NombreColonne + NombreColonne; i++)
                {
                    if (!ColonnesSaturees.Contains(Cellules[i].Colonne) & Cellules[i].Cout < minCell.Cout)
                    {
                        minCell = Cellules[i];
                    }
                }
            }

            else
            {
                int k = pos;

                while (i < NombreLigne)
                {
                    if (!LignesSaturees.Contains(Cellules[k].Ligne) & Cellules[k].Cout < minCell.Cout)
                    {
                        minCell = Cellules[k];
                    }

                    k += NombreColonne;
                    i++;
                }
            }
            return minCell;
        }

        public Cellule CoutSuperieurImmediat(int dim, Cellule minCell)
        {
            Cellule cell = new Cellule(10000000);

            int i = 0;

            if (dim == 0)
            {
                for (i = minCell.Ligne * NombreColonne; i < minCell.Ligne * NombreColonne + NombreColonne; i++)
                {
                    if (!ColonnesSaturees.Contains(Cellules[i].Colonne) & Cellules[i] != minCell & Cellules[i].Cout < cell.Cout)
                    {
                        cell = Cellules[i];
                    }
                }           
            }

            else
            {
                int k = minCell.Colonne;

                while (i < NombreLigne)
                {
                    if (!LignesSaturees.Contains(Cellules[k].Ligne) & Cellules[k] != minCell & Cellules[k].Cout < cell.Cout)
                    {
                        cell = Cellules[k];
                    }

                    k += NombreColonne;
                    i++;
                }
            }

            return cell;
        }

        
       
    }
}
