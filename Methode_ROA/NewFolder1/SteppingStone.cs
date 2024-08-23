using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methode_ROA
{
    public class SteppingStone
    {
        public TableauTransport PlanBasique { get; set; }

        public string Trace
        {
            get
            {
                Optimisation();
                return trace;
            }
        }

        public SteppingStone(TableauTransport pb)
        {
            PlanBasique = pb;
        }
        public Cellule CelluleEntrante()
        {
            Cellule cell = new Cellule(0);

            foreach (Cellule c in PlanBasique.Cellules)
            {
                if (c.Delta < cell.Delta)
                    cell = c;
            }

            return cell;
        }

        private Potentiels CalculPotentiels()
        {
            CorrigerLaDegenerescence(PlanBasique);

            Stack<Regret> RegretsLignesTampon = new Stack<Regret>();
            Stack<Regret> RegretsColonnesTampon = new Stack<Regret>();
            List<Regret> RegretsLignes = new List<Regret>();
            List<Regret> RegretsColonnes = new List<Regret>();

            Cellule cellInit = CoutMaximal(1, 0);

            Regret regret = new Regret(cellInit.Cout, cellInit);

            RegretsColonnesTampon.Push(regret);
            RegretsColonnes.Add(regret);

            while (RegretsColonnesTampon.Count > 0)
            {
                Regret reg = RegretsColonnesTampon.Pop();
                Cellule celReg = reg.Cellule;

                foreach (Cellule cell in CellulesDim(1, celReg.Colonne, true))
                {
                    if (!ExistPotentiel(0, cell.Ligne, RegretsLignes))
                    {
                        Regret newReg = new Regret(reg.Valeur - cell.Cout, cell);
                        RegretsLignesTampon.Push(newReg);

                        int index = Methodes.InsertIn(0, newReg, RegretsLignes);

                        if (index < 0) RegretsLignes.Add(newReg);
                        else RegretsLignes.Insert(index, newReg);
                    }
                }

                while (RegretsLignesTampon.Count > 0)
                {
                    Regret reg1 = RegretsLignesTampon.Pop();
                    Cellule celRegLigne = reg1.Cellule;

                   

                    foreach (Cellule cell in CellulesDim(0, celRegLigne.Ligne, true))
                    {
                        if (!ExistPotentiel(1, cell.Colonne, RegretsColonnes))
                        {
                            Regret newReg = new Regret(reg1.Valeur + cell.Cout, cell);
                            RegretsColonnesTampon.Push(newReg);

                            int index = Methodes.InsertIn(1, newReg, RegretsColonnes);

                            if (index < 0) RegretsColonnes.Add(newReg);
                            else RegretsColonnes.Insert(index, newReg);
                        }
                    }

                    foreach (Cellule cell in CellulesDim(0, celRegLigne.Ligne, false)) //Ici, on affecte une quantité nulle à une variable hors-base pour éradiquer la dégénérescence
                    {
                        if (!PlanBasique.LignesSaturees.Contains(cell.Ligne) && !PlanBasique.ColonnesSaturees.Contains(cell.Colonne))
                        {
                            List<Cellule> cellsLigne = CellulesDim(1, cell.Colonne, true);

                            foreach (Cellule c in cellsLigne)
                            {
                                if (CellulesDim(0, c.Ligne, true).Count == 1)
                                {
                                    Regret newReg = new Regret(reg1.Valeur + cell.Cout, cell);
                                    RegretsColonnesTampon.Push(newReg);

                                    //A revoir
                                    int index = Methodes.InsertIn(1, newReg, RegretsColonnes);

                                    if (index < 0) RegretsColonnes.Add(newReg);
                                    else RegretsColonnes.Insert(index, newReg);
                                }
                            }
                        }
                    }
                }
            }

            //if (PlanBasique.LignesSaturees.Count != 0)
            //{
            //    foreach (int i in PlanBasique.LignesSaturees)
            //    {
            //        RegretsLignes.Insert(i, new Regret(0, new Cellule(0)));
            //    }
            //}

            //if (PlanBasique.ColonnesSaturees.Count != 0)
            //{
            //    foreach (int i in PlanBasique.ColonnesSaturees)
            //    {
            //        RegretsColonnes.Insert(i, new Regret(0, new Cellule(0)));
            //    }
            //}

            return new Potentiels(RegretsLignes, RegretsColonnes);
        }

         private Cellule CoutMaximal(int dim, int pos)
        {
            Cellule maxCell = new Cellule(-1);
            int i = 0;

            if (dim == 0)
            {
                for (i = pos * PlanBasique.NombreColonne; i < pos * PlanBasique.NombreColonne + PlanBasique.NombreColonne; i++)
                {
                    if (PlanBasique.Cellules[i].Cout > maxCell.Cout)
                    {
                        maxCell = PlanBasique.Cellules[i];
                    }
                }
            }

            else
            {
                int k = pos;

                while (i < PlanBasique.NombreLigne)
                {
                    if (PlanBasique.Cellules[k].Quantite > 0 && PlanBasique.Cellules[k].Cout > maxCell.Cout)
                    {
                        maxCell = PlanBasique.Cellules[k];
                    }

                    k += PlanBasique.NombreColonne;
                    i++;
                 }  
            }
            return maxCell;
        }
 
        Potentiels pot;
        public void CalculDeltas()
        {
            pot = CalculPotentiels();
            foreach (Cellule cell in PlanBasique.Cellules)
            {
                if (cell.Quantite == 0 && !PlanBasique.LignesSaturees.Contains(cell.Ligne) && !PlanBasique.ColonnesSaturees.Contains(cell.Colonne))
                {
                    cell.Delta = cell.Cout - pot.Arrivees[cell.Colonne].Valeur + pot.Origines[cell.Ligne].Valeur;
                }
            }
        }

        public bool ExistPotentiel(int dim, int pos, List<Regret> list)
        {
            bool exist = false;

            if (dim == 0)
            {
                foreach (Regret reg in list)
                {
                    if (reg.Cellule.Ligne == pos)
                    {
                        exist = true;
                        break;
                    }
                }
            }

            else
            {
                foreach (Regret reg in list)
                {
                    if (reg.Cellule.Colonne == pos)
                    {
                        exist = true;
                        break;
                    }
                }
            }

            return exist;
        }

        public List<Cellule> CellulesDim(int dim, int pos, bool basique)
        {
            List<Cellule> list = new List<Cellule>();

            int i = 0;

            if (dim == 0)
            {
                if (basique)
                {
                    for (i = pos * PlanBasique.NombreColonne; i < pos * PlanBasique.NombreColonne + PlanBasique.NombreColonne; i++)
                    {
                        if (PlanBasique.Cellules[i].Quantite > 0) list.Add(PlanBasique.Cellules[i]);
                    }
                }

                else
                {
                    for (i = pos * PlanBasique.NombreColonne; i < pos * PlanBasique.NombreColonne + PlanBasique.NombreColonne; i++)
                    {
                        if (PlanBasique.Cellules[i].Quantite == 0) list.Add(PlanBasique.Cellules[i]);
                    }
                }
            }

            else
            {
                int k = pos;

                if (basique)
                {
                    while (i < PlanBasique.NombreLigne)
                    {
                        if (PlanBasique.Cellules[k].Quantite > 0) list.Add(PlanBasique.Cellules[k]);
                        k += PlanBasique.NombreColonne;
                        i++;
                    }
                }

                else
                {
                    while (i < PlanBasique.NombreLigne)
                    {
                        if (PlanBasique.Cellules[k].Quantite == 0) list.Add(PlanBasique.Cellules[k]);
                        k += PlanBasique.NombreColonne;
                        i++;
                    }
                }

            }

            return list;

        }

        public Cellule CoutMaximal(int dim, int pos, bool basique)
        {
            Cellule maxCell = new Cellule(-1);
            int i = 0;

            if (basique)
            {
                if (dim == 0)
                {
                    for (i = pos * PlanBasique.NombreColonne; i < pos * PlanBasique.NombreColonne + PlanBasique.NombreColonne; i++)
                    {
                        if (PlanBasique.Cellules[i].Cout > maxCell.Cout && PlanBasique.Cellules[i].Quantite > 0)
                        {
                            maxCell = PlanBasique.Cellules[i];
                        }
                    }
                }

                else
                {
                    int k = pos;

                    while (i < PlanBasique.NombreLigne)
                    {
                        if (PlanBasique.Cellules[k].Cout > maxCell.Cout && PlanBasique.Cellules[k].Quantite > 0)
                        {
                            maxCell = PlanBasique.Cellules[k];
                        }

                        k += PlanBasique.NombreColonne;
                        i++;
                    }

                }
            }

            return maxCell;
        }



        //public Cycle GetCycle(Cellule baseCell)
        //{
        //    Cycle cycle = new Cycle(baseCell);

        //    List<Cellule> CellulesL = CellulesDim(0, baseCell.Ligne, true);
        //    List<Cellule> CellulesC = CellulesDim(1, baseCell.Colonne, true);

        //    bool trouve = false;

        //    foreach (Cellule celL in CellulesL)
        //    {
        //        foreach (Cellule celC in CellulesC)
        //        {
        //            if (PlanBasique.Cellules[celC.Ligne * PlanBasique.NombreColonne + celL.Colonne].Quantite > 0)
        //            {
        //                cycle.CelluleLR = celL;
        //                cycle.CelluleUD = celC;
        //                cycle.CelluleDiag = PlanBasique.Cellules[celC.Ligne * PlanBasique.NombreColonne + celL.Colonne];

        //                trouve = true;
        //                break;
        //            }
        //        }

        //        if (trouve) break;
        //    }

        //    return cycle;
        //}

        string trace = " <br/><p id=\"MTitleVe\"> Optimisation par la méthode de pontentiel </p>";
        string ElementCycle = ""; 

        public TableauTransport Optimisation()
        {

            CalculDeltas();
            Cycle2 cycle;
            float q = 0;

            while (!PlanBasique.EstOptimal())
            {
                 cycle = GetCycle(CelluleEntrante());

                 q = cycle.GetMin();

                 trace += TableauItere(cycle, q);

                  trace += "<p id=\"MTitleVe\">∆<sub>ij</sub> = Max{ | ∆<sub>ij</sub> | } = " + CelluleEntrante().Delta + "</p>";

                  ElementCycle = ElementCycle.Substring(0, ElementCycle.Length - 2);

                   trace += "<p id=\"MTitleVe\"> θ = Min{ " + ElementCycle + " } = " + q + "</p>";

                cycle.Ajuster(q);

                PlanBasique.InitialisationDeltas();

                CalculDeltas();
            }

            trace += TableauItere(new Cycle2(new Cellule()), q) + TracePlanOptimalBasique(PlanBasique);

            trace += "</body></html>";
           
            return PlanBasique;
        }

        public string TableauItere(Cycle2 cycle, float q)
        {
            string str = "<table><tr><td></td>";

            for (int i = 1; i < PlanBasique.NombreColonne + 1; i++)
            {
                str += "<td id=\"MTitle\">M<sub>" + i + "</sub></td>";
            }

            str += "<td id=\"MTitle\"><p id=\"MTi\">P<sub>o</sub></p></td></tr>";

            int  l = 0;

            for (int i = 0; i < PlanBasique.NombreLigne; i++)
            {
                str += "<tr><td id=\"MTitless\"><p id=\"MTi\">P<sub>" + (i + 1) + "</sub></p></td>";

                for (int j = 0; j < PlanBasique.NombreColonne + 1; j++)
                {
                    if (j == PlanBasique.NombreColonne) str += "<td id=\"MTits\">" + pot.Origines[i].Valeur + "</td>";
                    else
                    {
                        string classe = (cycle.CellulesRetrait.Contains(PlanBasique.Cellules[l]) || cycle.CellulesAjout.Contains(PlanBasique.Cellules[l])) ? "cycle" : "MTit";
                        if (PlanBasique.Cellules[i * PlanBasique.NombreColonne + j].Quantite == 0)
                        {
                            
                            str += "<td class=\""+classe+"\"><span id=\"trues\">" + PlanBasique.Cellules[l].Cout + "</span><br /><span id=\"trueOp\" >" + PlanBasique.Cellules[l].Delta + "</span></td>";
                        }
                        else
                        {
                            str += "<td class=\"" + classe + "\"><span id=\"trues\">" + PlanBasique.Cellules[l].Cout + "</span><br /><span id=\"true\">(" + PlanBasique.Cellules[l].Quantite + ")</span></td>";                       
                        }
                        l++;
                    }
                }
            }

            str += "<tr><td id=\"MTitle\"><p id=\"MTi\">P<sub>d</sub></p></td>";

            foreach (Regret reg in pot.Arrivees)
            {
                str += "<td id=\"MTits\">" + reg.Valeur + "</td>";
            }

            str += "</tr></table>";

            return str;
        }

        public Cycle2 GetCycle(Cellule baseCell)
        {
            //PlanBasique.ColonnesSaturees.Clear();
            //PlanBasique.LignesSaturees.Clear();
            ElementCycle = "";

            Cycle2 cycle = new Cycle2(baseCell);
            int actualLine = -1;
            int stopLine = baseCell.Ligne;
            bool ajoutInLine = false;

            Cellule cellTampon;

            cycle.CellulesAjout.Add(baseCell);
            cellTampon = baseCell;

            while (actualLine != stopLine)
            {
                if (!ajoutInLine)
                {
                    List<Cellule> list = CellulesDim(1, cellTampon.Colonne, true);

                    foreach (Cellule cell in list)
                    {
                        if (cell != cellTampon && (CellulesDim(0, cell.Ligne, true).Count > 1 || cell.Ligne == stopLine))
                        {               
                                cellTampon = cell;
                                cycle.CellulesRetrait.Add(cell);
                                actualLine = cell.Ligne;
                                ajoutInLine = true;
                                ElementCycle += cell.Quantite + " , "; 
                               break;                                               
                        }
                    }
                }
                else 
                {
                    List<Cellule> list = CellulesDim(0, cellTampon.Ligne, true);
                    bool f = false;
                    foreach (Cellule cell in list)
                    {
                        if (cell != cellTampon && CellulesDim(1, cell.Colonne, true).Count > 1 )
                        {
                        
                            for (int i = 0; i < CellulesDim(1, cell.Colonne, true).Count; i++)
                            {
                                if (CellulesDim(0, CellulesDim(1, cell.Colonne, true)[i].Ligne, true).Count > 1)
                                {
                                    f = true;
                                }
                                else
                                    f = false;
                            }
                            if (f)
                            {
                                cellTampon = cell;
                                cycle.CellulesAjout.Add(cell);
                                actualLine = cell.Ligne;
                                ajoutInLine = false;
                                break;  
                            }
                        }
                    }
                }
            }
            return cycle;
        }

       public string TracePlanOptimalBasique(TableauTransport tabTrans)
        {
            string chaine = "<p id=\"MTitleVe\">Comme toutes les ∆ij ≥ 0 alors le plan basique Optimal est : </p><table><tr><td></td>";

            for (int i = 1; i < tabTrans.NombreColonne + 1; i++)
            {
                chaine += "<td id=\"MTitle\">M<sub>" + i + "</sub></td>";
            }

            chaine += "<td id=\"MTitle\"><p id=\"MTi\">a<sub>i</sub></p></td></tr>";

            int l = 0;

            float Zmin = 0;
            string ZminChaine = "";
            for (int i = 0; i < tabTrans.NombreLigne; i++)
            {
                chaine += "<tr><td id=\"MTitless\"><p id=\"MTi\">P<sub>" + (i + 1) + "</sub></p></td>";
                for (int j = 0; j < tabTrans.NombreColonne + 1; j++)
                {
                    if (j == tabTrans.NombreColonne) chaine += "<td id=\"MTits\">" + tabTrans.Offres[i] + "</td>";
                    else
                    {
                        if (tabTrans.Cellules[l].Quantite == 0)
                        {
                            chaine += "<td class=\"MTit\"><span id=\"trues\">" + tabTrans.Cellules[l].Cout + "</span><br /><span id=\"truess\">(" + tabTrans.Cellules[l].Quantite + ")</span></td>";
                        }
                        else
                        {
                            chaine += "<td class=\"MTit\"><span id=\"trues\">" + tabTrans.Cellules[l].Cout + "</span><br /><span id=\"true\">(" + tabTrans.Cellules[l].Quantite + ")</span></td>";
                            
                            ZminChaine +=tabTrans.Cellules[l].Cout + " * " + tabTrans.Cellules[l].Quantite +" + ";

                            Zmin += tabTrans.Cellules[l].Cout * tabTrans.Cellules[l].Quantite;

                        }
                        l++;
                    }
                }
                chaine += "</tr>";
            }

            ZminChaine = ZminChaine.Substring(0, ZminChaine.Length - 2) ;

            chaine += "<tr><td id=\"MTitle\"><p id=\"MTi\">b<sub>j</sub></p></td>";

            for (int i = 0; i < tabTrans.NombreColonne + 1; i++)
            {
                if (i < tabTrans.NombreColonne) chaine += "<td id=\"MTits\">" + tabTrans.Demandes[i] + "</td>";

                else chaine += "<td id=\"MTits\">" + SommeQuantites(tabTrans.Demandes) + "</td>";
            }
            chaine += "</tr></table>";

            chaine += "<p id=\"MTitleVe\">Z(x*) = " +( ZminChaine)+" = "+ Zmin +"</p>";

            return chaine;
        }

      public float SommeQuantites(List<float> list)
       {
           float somme = 0;

           foreach (float element in list)
           {
               somme += element;
           }

           return somme;
       }


      public void CorrigerLaDegenerescence(TableauTransport tabTrans)
      {
          foreach (Cellule cell in tabTrans.Cellules)
          {
              if (cell.Quantite > 0 && CellulesDim(0, cell.Ligne, true).Count == 1 && CellulesDim(1, cell.Colonne, true).Count == 1)
              {
                  if (!tabTrans.LignesSaturees.Contains(cell.Ligne))
                  {
                      tabTrans.LignesSaturees.Add(cell.Ligne);
                  }

                  if (!tabTrans.ColonnesSaturees.Contains(cell.Colonne))
                  {
                      tabTrans.ColonnesSaturees.Add(cell.Colonne);
                  }
              }
          }
      }


	 
	}

}

