using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPruebaMasiv.Class;
using WebPruebaMasiv.Models;

namespace WebPruebaMasiv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasinoController : ControllerBase
    {

        
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            RouletteModelClass roulette = new RouletteModelClass();
            int result = roulette.Create();
            if (result > 0) return Ok(result);

            return BadRequest(result);


        }


        [HttpGet]
        [Route("active/{rouletteId}")]
        public IActionResult Active(int rouletteId)
        {
            RouletteModelClass roulette = new RouletteModelClass();
            int result = roulette.GetRoulette(rouletteId).Active();
            if (result > 0)
            {
                return Ok("Exitoso");
            }
            else {
                return BadRequest("Denegado");
            }

        }

        [HttpPost]
        [Route("createbet")]
        public IActionResult CreateBet(Bet bet)
        {
            if(bet.validate() != "Ok") return BadRequest(bet.validate());
            BetModelClass betModel = new BetModelClass();
            int result = betModel.Create(bet);
            if (result > 0) return Ok(result);

            return BadRequest(result);


        }

        [HttpGet]
        [Route("closeroulette/{rouletteId}")]
        public IActionResult CloseRoulette(int rouletteId)
        {
            RouletteModelClass rouletteModel = new RouletteModelClass();
            BetModelClass betModel = new BetModelClass();
            Roulette roulette = rouletteModel.GetRoulette(rouletteId);
            List<Bet> bets = betModel.AllRouletteClose((int)roulette.id);
            roulette.Inactive();
            Random random = new Random();
            int numero = random.Next(0, 36);
            foreach (Bet bet in bets) {
                bet.BetResult(numero);
            }
        
            return Ok(bets);


        }

        [HttpGet]
        [Route("allroulette")]
        public IActionResult AllRoulette()
        {
            RouletteModelClass roulette = new RouletteModelClass();

            return Ok(roulette.All());

        }


    }
}
