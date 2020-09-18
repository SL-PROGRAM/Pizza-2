using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO.Validator;
using System.ComponentModel.DataAnnotations;

namespace TPPizza.Models
{
    public class PizzaVM
    {
        public Pizza Pizza { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        [UniqueListeIngredient]
        [IngredientValidator]
        public List<int> IdsIngredients { get; set; }
        public List<Pate> Pates { get; set; }
        [Required]
        public int IdPate { get; set; }
    }
}