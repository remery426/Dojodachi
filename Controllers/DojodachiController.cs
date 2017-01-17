using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
namespace Dojodachi.Controllers
{
 public class DojodachiController : Controller
 {
  [HttpGet]
  [Route("Dojodachi")]
public IActionResult Index(){
    if(!HttpContext.Session.GetInt32("fullness").HasValue && !HttpContext.Session.GetInt32("happiness").HasValue)
    {
        HttpContext.Session.SetInt32("fullness", 20);
        HttpContext.Session.SetInt32("happiness", 20);
        HttpContext.Session.SetInt32("meals", 3);
        HttpContext.Session.SetInt32("energy", 50);
    }
      ViewBag.fullness = (HttpContext.Session.GetInt32("fullness"));
      ViewBag.happiness = (HttpContext.Session.GetInt32("happiness"));
      ViewBag.meals = (HttpContext.Session.GetInt32("meals"));
      ViewBag.energy = (HttpContext.Session.GetInt32("energy"));
      ViewBag.response = ( HttpContext.Session.GetString("response"));
      
   return View();
  
  }
  [HttpGet]
  [RouteAttribute("feed")]
  public IActionResult feed()
  {
      Console.WriteLine(HttpContext.Session.GetInt32("fullness"));
      if (HttpContext.Session.GetInt32("meals")>0){
          var random = new Random();
          if(random.Next(0,3)==0){
              HttpContext.Session.SetInt32("meals",HttpContext.Session.GetInt32("meals").Value - 1);
              HttpContext.Session.SetString("response","Your Dojodachi didn't like his food. You Lost 1 meal.");
              return RedirectToAction("Index");

          }
          var full_increase = random.Next(5,11);
          HttpContext.Session.SetString("response","Your Dojodachi liked his food. Gained " +full_increase+ " fullness. Lost 1 meal.");
          HttpContext.Session.SetInt32("fullness",HttpContext.Session.GetInt32("fullness").Value + full_increase);
          HttpContext.Session.SetInt32("meals",HttpContext.Session.GetInt32("meals").Value - 1);
      }
      else{
          Console.WriteLine("No meals available");
      }
      Console.WriteLine(HttpContext.Session.GetInt32("fullness"));
      return RedirectToAction("Index");
  }
  [HttpGet]
  [RouteAttribute("sleep")]
  public IActionResult sleep(){
      HttpContext.Session.SetInt32("fullness",HttpContext.Session.GetInt32("fullness").Value -5);
      HttpContext.Session.SetInt32("happiness",HttpContext.Session.GetInt32("happiness").Value -5);
      HttpContext.Session.SetInt32("energy",HttpContext.Session.GetInt32("energy").Value +15);
      HttpContext.Session.SetString("response","Your Dojodachi gained 15 energy, lost 5 happiness and lost 5 fullness");
      return RedirectToAction("Index");
  }

  [HttpGet]
  [RouteAttribute("play")]

  public IActionResult play(){

      var random = new Random();
      if(random.Next(0,4)==0){
          HttpContext.Session.SetInt32("energy",HttpContext.Session.GetInt32("energy").Value -5);
          HttpContext.Session.SetString("response","Your Dojodachi didn't like the game. Lost 5 energy");
          return RedirectToAction("Index");
      }
      HttpContext.Session.SetInt32("energy",HttpContext.Session.GetInt32("energy").Value -5);
      var gain = random.Next(5,11);
      HttpContext.Session.SetInt32("happiness",HttpContext.Session.GetInt32("happiness").Value + gain);
    HttpContext.Session.SetString("response","Your Dojodachi liked the game.  Gained " + gain + " happiness. Lost 5 energy");
      return RedirectToAction("Index");
  }

  [HttpGet]
  [RouteAttribute("work")]

  public IActionResult work(){
      if(HttpContext.Session.GetInt32("energy")>0){
      HttpContext.Session.SetInt32("energy",HttpContext.Session.GetInt32("energy").Value -5);
      var random = new Random();
      var meal_add = random.Next(1,4);
      HttpContext.Session.SetInt32("meals",HttpContext.Session.GetInt32("meals").Value + meal_add);
      HttpContext.Session.SetString("response","Move that work. Gained " + meal_add + " meals.");
      }
      else{
          Console.WriteLine("You too tired boiii");
      }

      return RedirectToAction("Index");
  }
 
  [HttpGet]
  [RouteAttribute("reset")]
  public IActionResult reset(){
    HttpContext.Session.SetInt32("fullness", 20);
    HttpContext.Session.SetInt32("happiness",20);
    HttpContext.Session.SetInt32("meals", 3);
    HttpContext.Session.SetInt32("energy",50);
    return RedirectToAction("Index");
  }
 }
}
