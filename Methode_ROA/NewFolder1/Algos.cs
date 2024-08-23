using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace Methode_ROA
{
    static public class Algos
    {
        static public TableauTransport CoutMinimal(TableauTransport tabTrans)
        {
            tabTrans = Methodes.AjusterOffresDemandes(tabTrans);

            List<float> qteOffertes = new List<float>();
            List<float> qteDemandees = new List<float>();

            foreach (float o in tabTrans.Offres)
            {
                qteOffertes.Add(o);
            }

            foreach (float d in tabTrans.Demandes)
            {
                qteDemandees.Add(d);
            }

            while (tabTrans.LignesSaturees.Count < tabTrans.NombreLigne)
            {
                Cellule minCell = tabTrans.CoutMinimal();
                minCell.Quantite = Math.Min(qteOffertes[minCell.Ligne], qteDemandees[minCell.Colonne]);

                if (minCell.Quantite == qteOffertes[minCell.Ligne])
                {
                    tabTrans.LignesSaturees.Add(minCell.Ligne);
                    qteDemandees[minCell.Colonne] -= minCell.Quantite;
                }

                else
                {
                    tabTrans.ColonnesSaturees.Add(minCell.Colonne);
                    qteOffertes[minCell.Ligne] -= minCell.Quantite;
                }
            }

            tabTrans.CoutOptimal = Methodes.CoutOptimal(tabTrans);

            return tabTrans;
        }

        static public TableauTransport CoinNordOuest(TableauTransport tabTrans)
        {
            tabTrans = Methodes.AjusterOffresDemandes(tabTrans);

            List<float> qteOffertes = new List<float>();
            List<float> qteDemandees = new List<float>();

            foreach (float o in tabTrans.Offres)
            {
                qteOffertes.Add(o);
            }

            foreach (float d in tabTrans.Demandes)
            {
                qteDemandees.Add(d);
            }

            Cellule cell = tabTrans.Cellules[0];

            while (tabTrans.LignesSaturees.Count < tabTrans.NombreLigne)
            {
                cell.Quantite = Math.Min(qteDemandees[cell.Colonne], qteOffertes[cell.Ligne]);

                if (cell.Quantite == qteOffertes[cell.Ligne])
                {
                    qteDemandees[cell.Colonne] -= cell.Quantite;
                    tabTrans.LignesSaturees.Add(cell.Ligne);

                    int newIndice = (tabTrans.Cellules.IndexOf(cell) + tabTrans.NombreColonne) < tabTrans.Cellules.Count ? (tabTrans.Cellules.IndexOf(cell) + tabTrans.NombreColonne) : 0;
                    cell = tabTrans.Cellules[newIndice];
                }

                else
                {
                    tabTrans.ColonnesSaturees.Add(cell.Colonne);
                    qteOffertes[cell.Ligne] -= cell.Quantite;
                    cell = tabTrans.Cellules[tabTrans.Cellules.IndexOf(cell) + 1];
                }
            }

            tabTrans.CoutOptimal = Methodes.CoutOptimal(tabTrans);

            return tabTrans;
        }

        static public TableauTransport BalasHammmer(TableauTransport tabTrans)
        {
            tabTrans = Methodes.AjusterOffresDemandes(tabTrans);

            while (tabTrans.ColonnesSaturees.Count < tabTrans.NombreColonne || tabTrans.LignesSaturees.Count < tabTrans.NombreLigne)
            {
                List<Regret> RegretsLignes = new List<Regret>();
                List<Regret> RegretsColonnes = new List<Regret>();
                List<Regret> Regrets = new List<Regret>();

                for (int i = 0; i < tabTrans.NombreLigne; i++)
                {
                    if (!tabTrans.LignesSaturees.Contains(i))
                    {
                        Cellule cell = tabTrans.CoutMinimal(0, i);
                        Regrets.Add(new Regret(tabTrans.CoutSuperieurImmediat(0, cell).Cout - cell.Cout, cell));
                    }
                }

                for (int j = 0; j < tabTrans.NombreColonne; j++)
                {
                    if (!tabTrans.ColonnesSaturees.Contains(j))
                    {
                        Cellule cell = tabTrans.CoutMinimal(1, j);
                        Regrets.Add(new Regret(tabTrans.CoutSuperieurImmediat(0, cell).Cout - cell.Cout, cell));

                    }
                }

                Cellule affectCell = Methodes.RegretMaximum(Regrets).Cellule;

                affectCell.Quantite = Math.Min(tabTrans.Demandes[affectCell.Colonne], tabTrans.Offres[affectCell.Ligne]);

                if (affectCell.Quantite == tabTrans.Demandes[affectCell.Colonne])
                {
                    tabTrans.Offres[affectCell.Ligne] -= affectCell.Quantite;
                    tabTrans.ColonnesSaturees.Add(affectCell.Colonne);
                }

                else
                {
                    tabTrans.Demandes[affectCell.Colonne] -= affectCell.Quantite;
                    tabTrans.LignesSaturees.Add(affectCell.Ligne);
                }

            }

            tabTrans.CoutOptimal = Methodes.CoutOptimal(tabTrans);

            return tabTrans;
        }
    }
}
