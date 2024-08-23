using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methode_ROA
{
    public class Cellule
    {
        public int Ligne { get; set; }
        public int Colonne { get; set; }
        public float Cout { get; set; }
        public float Quantite { get; set; }
        public float Delta { get; set; }

        public Cellule()
        {

        }

        public Cellule(int ligne, int colonne, float cout)
        {
            Ligne = ligne;
            Colonne = colonne;
            Cout = cout;
        }

        public Cellule(float cout)
        {
            Cout = cout;
        }
   
    }

    public class Regret
    {
        public float Valeur { get; set; }
        public Cellule Cellule { get; set; }

        public Regret(float val)
        {
            Valeur = val;
        }


        public Regret(float val, Cellule cell)
        {
            Valeur = val;
            Cellule = cell;
        }

    }

    public class Potentiels
    {
        public List<Regret> Origines { get; set; }
        public List<Regret> Arrivees { get; set; }

        public Potentiels()
        {
            Origines = new List<Regret>();
            Arrivees = new List<Regret>();
        }

        public Potentiels(List<Regret> ligne, List<Regret> colonne)
        {
            Origines = new List<Regret>();
            Origines = ligne;

            Arrivees = new List<Regret>();
            Arrivees = colonne;
        }
    }

    public class Cycle
    {
        public Cellule CelluleBase { get; set; }
        public Cellule CelluleLR { get; set; }
        public Cellule CelluleUD { get; set; }
        public Cellule CelluleDiag { get; set; }

        public Cycle(Cellule cell)
        {
            CelluleBase = cell;
        }

        public void Ajuster(float q)
        {
            CelluleBase.Quantite += q;
            CelluleDiag.Quantite += q;
            CelluleLR.Quantite -= q;
            CelluleUD.Quantite -= q;
        }
    }

    public class Cycle2
    {
        public Cellule CelluleBase { get; set; }
        public List<Cellule> CellulesAjout { get; set; }
        public List<Cellule> CellulesRetrait { get; set; }

        public Cycle2(Cellule cell)
        {
            CellulesAjout = new List<Cellule>();
            CellulesRetrait = new List<Cellule>();
            CelluleBase = cell;
        }

        public void Ajuster(float q)
        {
            foreach (Cellule cell in CellulesAjout)
            {
                cell.Quantite += q;
            }

            foreach (Cellule cell in CellulesRetrait)
            {
                cell.Quantite -= q;
            }
        }

        public float GetMin()
        {
            float min = CellulesRetrait[0].Quantite;

            for (int i = 1; i < CellulesRetrait.Count; i++)
            {
                min = CellulesRetrait[i].Quantite < min ? CellulesRetrait[i].Quantite : min;
            }

            return min;
        }
    }
}
