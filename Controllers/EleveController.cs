using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using formatif.Models;

namespace formatif.Controllers
{
    public class EleveController : Controller
    {
        SalleRobotDb db = new SalleRobotDb();
        // GET: Eleve
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String Id_user, String Psw)
        {


            var res = (from item in db.Eleves
                       where (item.Eleve_Id == Id_user) && (item.Psw == Psw)
                       select item).FirstOrDefault();


            if (res != null)
            {
                Session["id_user"] = res.Eleve_Id;
                return RedirectToAction("Accueil", "Eleve");
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

            var res1 = (from item in db.Eleves
                       where (item.Eleve_Id == datos.nom_log)
                       select item).FirstOrDefault();

            var res = (from item in db.Reservations
                       select item);

            ViewBag.message_titre = res1.Prenom + " " + res1.Nom + " es " + res1.Status;

            datos.Reservations = res.ToList<Reservation>();
            return View(datos);


        }

        public ActionResult EnregistrerNew(int Jour, int Hour)
        {
            Reservation reser = new Reservation();

            String Id_user = Session["id_user"].ToString();
            var res = (from item in db.Eleves
                       where (item.Eleve_Id == Id_user)
                       select item).FirstOrDefault();

           

            reser.Hour = Hour;
            reser.Jour = Jour;

            if (res != null)
            {
                if (res.Status != "Suspendu")
                {
                    reser.Supervision = res.Supervision;
                    reser.Eleve_Id = res.Eleve_Id;
                    db.Reservations.Add(reser);
                    db.SaveChanges();
                    return RedirectToAction("Accueil", "Eleve");
                }
                else
                {
                   
                    return RedirectToAction("Accueil", "Eleve");
                }

            }
            else
            {


                 return RedirectToAction("Accueil", "Eleve");
            }
           
        }

        public ActionResult DeleteReg(int Reserva_Id)
        {
            var cliente = db.Reservations.Find(Reserva_Id);

            db.Reservations.Remove(cliente);

            db.SaveChanges();

            return RedirectToAction("Accueil", "Eleve");
        }


 public ActionResult Message()
        {
            ModMessage2 data = new ModMessage2();
            String user1 = Session["id_user"].ToString();
            data.nom_log = user1;

            var res2 = from item2 in db.Enseignants
                       select item2;
            data.LstEnseignant = res2.ToList<Enseignant>();

            var res = from item in db.Messages
                      where item.Remite_Id == user1
                      select item;
            data.LstMessage = res.ToList<Message>();



            ViewBag.eleve = "Tous les eleves";
            ViewBag.disable = "disabled";

            return View(data);
        }


        [HttpPost]
        public ActionResult Message(String Msg, String eleve)
        {
            String user1 = Session["id_user"].ToString();
            ModMessage2 data = new ModMessage2();

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

            var res2 = from item2 in db.Enseignants
                       select item2;
            data.LstEnseignant = res2.ToList<Enseignant>();

            return View(data);
        }

        public ActionResult Select_eleve(String eleve)
        {
            String user1 = Session["id_user"].ToString();
            ModMessage2 data = new ModMessage2();

            ViewBag.disable = " ";

            var res = from item in db.Messages
                      where (item.Remite_Id == user1 && item.Destino_Id == eleve) || (item.Remite_Id == eleve && item.Destino_Id == user1)
                      select item;
            data.LstMessage = res.ToList<Message>();

            ViewBag.eleve = eleve;

            data.nom_log = user1;

            var res2 = from item2 in db.Enseignants
                       select item2;
            data.LstEnseignant = res2.ToList<Enseignant>();

            return View("Message", data);

        }


    }
}