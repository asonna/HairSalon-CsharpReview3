using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      // Get["/"] = _ => {
      //   return View["index.cshtml"];
      // };

      Get["/"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Get["/clients"] = _ => {
        List<Client> AllClients = Client.GetAll();
        return View["clients.cshtml", AllClients];
      };

      Get["/clients/new"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["clients_form.cshtml", AllStylists];
      };

      Get["/stylists/new"] = _ => {
        return View["stylists_form.cshtml"];
      };

//
      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        return View["success.cshtml"];
      };

// 
      Post["/clients/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        string clientName = Request.Form["client-name"];
        int clientStylistId = int.Parse(Request.Form["stylistId"]);
        Client newClient = new Client(clientName, clientStylistId);
        newClient.Save();
        return View["success.cshtml"];
      };

      //Routes for our individual view pages
      Get["/stylists/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedStylist = Stylist.Find(parameters.id);
        var StylistClients = SelectedStylist.GetClients();
        model.Add("stylist", SelectedStylist);
        model.Add("clients", StylistClients);
        return View["stylists.cshtml", model];
      };

      Get["stylist/update/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["stylist_update.cshtml", SelectedStylist];
      };

      Patch["stylist/update/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Update(Request.Form["stylist-name"]);
        return View["success.cshtml"];
      };



    }
  }
}
