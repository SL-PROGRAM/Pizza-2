using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO.Database;
using TPPizza.Models;

namespace TPPizza.Controllers
{
    public class PizzaController : Controller
    {
     
        // GET: Pizza
        public ActionResult Index()
        {
            return View(FakeDB.Instance.Pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            
            return View(Getpizza(id));
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaVM pizzaVM = new PizzaVM() {   Pizza = new Pizza(), 
                                                Ingredients = FakeDB.Instance.IngredientsDisponibles, 
                                                Pates = FakeDB.Instance.PatesDisponibles}; 

            return View(pizzaVM);
        }

        public PizzaVM Update(PizzaVM pizzaVM)
        {

            pizzaVM.Ingredients = FakeDB.Instance.IngredientsDisponibles;
            pizzaVM.Pates = FakeDB.Instance.PatesDisponibles;

            return pizzaVM;
        }


        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVM pizzaVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pizzaVM.Pizza.Pate = FakeDB.Instance.PatesDisponibles.FirstOrDefault(p => p.Id == pizzaVM.IdPate);

                    foreach (var ingredient in pizzaVM.IdsIngredients)
                    {
                        pizzaVM.Pizza.Ingredients.Add(FakeDB.Instance.IngredientsDisponibles.FirstOrDefault(i => i.Id == ingredient));
                    }
                    FakeDB.Instance.Pizzas.Add(pizzaVM.Pizza);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Update(pizzaVM));
                }
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            Pizza pizza = Getpizza(id);
            PizzaVM pizzaVM = new PizzaVM()
            {
                Pizza = pizza,
                Ingredients = FakeDB.Instance.IngredientsDisponibles,
                Pates = FakeDB.Instance.PatesDisponibles,
                IdsIngredients = pizza.Ingredients.Select(pi => pi.Id).ToList(),
                IdPate = pizza.Pate.Id
            };

            return View(pizzaVM);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaVM pizzaVM)
        {
            try
            {
                Pizza pizza = FakeDB.Instance.Pizzas.FirstOrDefault(p=>p.Id == pizzaVM.Pizza.Id);
                pizza.Nom = pizzaVM.Pizza.Nom;
                pizza.Pate = FakeDB.Instance.PatesDisponibles.FirstOrDefault(p => p.Id == pizzaVM.IdPate);
                pizza.Ingredients = FakeDB.Instance.IngredientsDisponibles
                                            .Where(x=> pizzaVM.IdsIngredients
                                            .Contains(x.Id))
                                            .ToList();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Getpizza(id));
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                FakeDB.Instance.Pizzas.Remove(Getpizza(id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Méthode privée permettant de récupérer la pizza demandée par l'utilisateur
        private Pizza Getpizza(int id)
        {
            return FakeDB.Instance.Pizzas.Where(p => p.Id == id).FirstOrDefault();
        }

    }
}
