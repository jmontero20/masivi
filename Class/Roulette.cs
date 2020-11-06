using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using WebPruebaMasiv.Models;

namespace WebPruebaMasiv.Class
{
    public class Roulette
    {
        public int? id { get; set; }

        public string name { get; set; }

        public bool estado { get; set; }


        public int Active() {
            this.estado = true;
            RouletteModelClass roulette = new RouletteModelClass();
            return roulette.Update(this);
        }

        public int Inactive()
        {
            this.estado = false;
            RouletteModelClass roulette = new RouletteModelClass();
            return roulette.Update(this);
        }


    }
}
