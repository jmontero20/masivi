using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPruebaMasiv.Models;

namespace WebPruebaMasiv.Class
{
    public class Bet
    {
        public int? number { get ; set; }
        public int? id { get; set; }

        public string color { get; set; }

        public double? value { get; set; }

        public double? winner { get; set; }

        public bool result { get; set; }

        public int id_roulette { get; set; }

        public string validate() {

            if (this.number == null && this.color == null) return "error no se registro apuesta";
            if (this.number != null && this.number < 0 && this.number > 36) return "numero apostado no es valido";
            if(this.color!= null && this.color != "negro" && this.color != "rojo") return "color apostado no es valido";
            if (this.value == null || this.value < 0 || this.value > 10000) return "valor de la apuesta no es valido";
            RouletteModelClass rouletteModel = new RouletteModelClass();
            Roulette roulette = rouletteModel.GetRoulette(this.id_roulette);
            if (!roulette.estado) return "La rouleta esta cerrada";
            return "Ok";

        }

        public void BetResult(int result) {
            BetModelClass betModel = new BetModelClass();
            if (this.number != null) {
                if (this.number == result)
                {
                    this.result = true;
                    this.winner = this.value * 5;
                    betModel.Update(this);
                }
                else {
                    this.result = false;
                    this.winner = 0;
                    betModel.Update(this);
                }

            }
            if (this.color != null) {
                if ((this.color == "negro" && result%2 == 1) || (this.color == "rojo" && result % 2 == 1))
                {
                    this.result = true;
                    this.winner = (double)this.value * 1.8;
                    betModel.Update(this);
                }
                else
                {
                    this.result = false;
                    this.winner = 0;
                    betModel.Update(this);
                }
            }
        }




    }
}
