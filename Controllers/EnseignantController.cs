using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using formatif.Models;

namespace formatif.Controllers
{
    public class EnseignantController : Controller
    {
        // GET: Enseignant

        SalleRobotDb db = new SalleRobotDb();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String Id_user, String Psw)
        {


            var res = (from item in db.Enseignants
                       where (item.Ens_Id == Id_user) && (item.Psw == Psw)
                       select item).FirstOrDefault();


            if (res != null)
            {
                Session["id_user"] = res.Ens_Id;
                return RedirectToAction("Accueil", "Enseignant");
            }
            else
            {
                ViewBag.message = "L'utilisateur ou mot de passe incorrect";
                return View();
            }

        }

        public ActionResult Accueil()
        {
            ModAccueil datos = new ModAccueil();
            datos.nom_log = Session["id_user"].ToString();

            var res = (from item in db.Reservations
                       select item);

            datos.Reservations = res.ToList<Reservation>();
            return View(datos);


        }


        public ActionResult CreerEleve()
        {
            Eleve ele = new Eleve();
            ModCreerEleve DonneELeves = new ModCreerEleve();

            DonneELeves.nom_log = Session["id_user"].ToString();

            ele.Eleve_Id = "";
            ele.Nom = "";
            ele.Prenom = "";
            ele.Email = "";
            ele.Psw = "";
            ele.Photo = "";
            ele.Status = "";
            ele.Supervision = "";

            DonneELeves.DatEleve = ele;

            var res2 = from item in db.Eleves
                       select item;
            DonneELeves.ListEleves = res2.ToList<Eleve>();
            return View(DonneELeves);
        }

        [HttpPost]
        public ActionResult CreerEleve(Eleve eleve)
        {
            Eleve ele = new Eleve();
            ModCreerEleve DonneELeves = new ModCreerEleve();
            DonneELeves.nom_log = Session["id_user"].ToString();

            var res = from ut in db.Eleves
                      where (ut.Email == eleve.Email) || (ut.Eleve_Id == eleve.Eleve_Id)
                      select ut;

            List<Eleve> nbUser = res.ToList<Eleve>();

            if (!(nbUser.Count > 0))
            {
                db.Eleves.Add(eleve);
                db.SaveChanges();
                ViewBag.message = "L'eleve a ete creé.";

                var res1 = from item in db.Eleves
                           select item;
                DonneELeves.DatEleve = ele;
                DonneELeves.ListEleves = res1.ToList<Eleve>();
                return View(DonneELeves);

            }
            else
            {
                ViewBag.message = "L'eleve ou courriel déjà Existe  ";

                var res2 = from item in db.Eleves
                           select item;

                DonneELeves.ListEleves = res2.ToList<Eleve>();
                DonneELeves.DatEleve = eleve;
                return View(DonneELeves);

            }

        }

        public ActionResult EnregistrerNew(int Jour, int Hour)
        {
            ModEnregistrerNew nouveau = new ModEnregistrerNew();
            nouveau.Hour = Hour;
            nouveau.Jour = Jour;

            var res = from ut in db.Eleves
                      select ut;
            nouveau.ListELeves = res.ToList<Eleve>();
            return View(nouveau);
        }

        [HttpPost]
        public ActionResult EnregistrerNew(Reservation reser)
        {
            ModEnregistrerNew nouveau = new ModEnregistrerNew();
            var res = (from ut in db.Eleves
                       where ut.Eleve_Id == reser.Eleve_Id
                       select ut).FirstOrDefault();


            var res2 = from ut2 in db.Eleves
                       select ut2;
            nouveau.ListELeves = res2.ToList<Eleve>();
            nouveau.Hour = reser.Hour;
            nouveau.Jour = reser.Jour;


            if (res != null)
            {
                if (res.Status != "Suspendu")
                {
                    reser.Supervision = res.Supervision;
                    db.Reservations.Add(reser);
                    db.SaveChanges();
                    return RedirectToAction("Accueil", "Enseignant");
                }
                else
                {
                    ViewBag.message = "L'utilisateur est suspendu";
                    return View(nouveau);
                }

            }
            else
            {

                ViewBag.message = "L'utilisateur n'existe pas";
                return View(nouveau);
            }

        }


        public ActionResult ModifierReg(int Reserva_Id)
        {

            return View();
        }

        public ActionResult DeleteReg(int Reserva_Id)
        {
            var cliente = db.Reservations.Find(Reserva_Id);

            db.Reservations.Remove(cliente);

            db.SaveChanges();

            return RedirectToAction("Accueil", "Enseignant");
        }

        public ActionResult Message()
        {
            ModMessage data = new ModMessage();
            String user1 = Session["id_user"].ToString();
            data.nom_log = user1;

            var res2 = from item2 in db.Eleves
                       select item2;
            data.LstELeves = res2.ToList<Eleve>();

            var res = from item in db.Messages
                      where item.Remite_Id == user1
                      select item;
            data.LstMessage = res.ToList<Message>();



            ViewBag.eleve = "Tous les eleves";


            return View(data);
        }


        [HttpPost]
        public ActionResult Message(String Msg, String eleve)
        {
            String user1 = Session["id_user"].ToString();
            ModMessage data = new ModMessage();

            if (eleve != "Tous les eleves")
            {
                Message msg1 = new Message();
                msg1.Destino_Id = eleve;
                msg1.Remite_Id = user1;
                msg1.Msg = Msg;
                msg1.Date = DateTime.Now;
                db.Messages.Add(msg1);
                db.SaveChanges();
                ViewBag.eleve = eleve;

                var res = from item in db.Messages
                          where (item.Remite_Id == user1 && item.Destino_Id == eleve) || (item.Remite_Id == eleve && item.Destino_Id == user1)
                          select item;
                data.LstMessage = res.ToList<Message>();
            }
            else
            {
                ViewBag.eleve = eleve;
            }

            data.nom_log = user1;

            var res2 = from item2 in db.Eleves
                       select item2;
            data.LstELeves = res2.ToList<Eleve>();

            return View(data);
        }

        public ActionResult Select_eleve(String eleve)
        {
            String user1 = Session["id_user"].ToString();
            ModMessage data = new ModMessage();



            var res = from item in db.Messages
                      where (item.Remite_Id == user1 && item.Destino_Id == eleve) || (item.Remite_Id == eleve && item.Destino_Id == user1)
                      select item;
            data.LstMessage = res.ToList<Message>();

            ViewBag.eleve = eleve;

            data.nom_log = user1;

            var res2 = from item2 in db.Eleves
                       select item2;
            data.LstELeves = res2.ToList<Eleve>();

            return View("Message", data);

        }
    }
}