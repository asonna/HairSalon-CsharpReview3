using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using HairSalon.Objects;
using System;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
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
        return View["success.cshtml", newStylist];
      };

//
      Post["/clients/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        string clientName = Request.Form["client-name"];
        int clientStylistId = int.Parse(Request.Form["stylistId"]);

        Client newClient = new Client(clientName, clientStylistId);
        newClient.Save();

        Stylist assignedStylist = Stylist.Find(clientStylistId);
        model.Add("client", newClient);
        model.Add("stylist", assignedStylist);
        return View["success.cshtml", model];
      };

      //Routes for our individual view pages
      Get["/stylist/{id}"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        return View["stylist.cshtml", selectedStylist];
      };

      Get["/client/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client selectedClient = Client.Find(parameters.id);
        var SelectedStylist = Stylist.Find(selectedClient.GetStylistId());
        model.Add("client", selectedClient);
        model.Add("stylist", SelectedStylist);
        return View["client.cshtml", model];
      };

      Get["stylist/update/{id}"] = parameters => {
        Stylist model = Stylist.Find(parameters.id);
        return View["stylist_update.cshtml", model];
      };

      Patch["stylist/update/{id}"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);

        string stylistName = Request.Form["stylist-name"];
        selectedStylist.Update(stylistName);
        Stylist updatedStylist = Stylist.Find(parameters.id);
        return View["success.cshtml", updatedStylist];
      };

      Get["/stylist/{id}/new_client"] = parameters =>
      {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        return View["client_to_stylist_form.cshtml.cshtml", selectedStylist];
      };

      Post["/stylist/{id}/new_client"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        Stylist selectedStylist = Stylist.Find(parameters.id);

        string clientName = Request.Form["client-name"];
        int clientStylistId = selectedStylist.GetId();

        Client newClient = new Client(clientName, clientStylistId);
        newClient.Save();
        model.Add("client", newClient);
        model.Add("stylist", selectedStylist);
        return View["success.cshtml", model];
      };

      Get["/client/update/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        Client selectedClient = Client.Find(parameters.id);
        Stylist selectedStylist = Stylist.Find(selectedClient.GetStylistId());
        model.Add("client", selectedClient);
        model.Add("all stylists", Stylist.GetAll());
        return View["client_update.cshtml", model];
        };

        Patch["/client/update/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        Client selectedClient = Client.Find(parameters.id);

        string clientName = Request.Form["client-name"];
        int clientStylistId = int.Parse(Request.Form["client-stylist-id"]);

        selectedClient.Update(clientName, clientStylistId);
        Client updatedClient = Client.Find(parameters.id);
        Stylist SelectedStylist = Stylist.Find(updatedClient.GetStylistId());

        model.Add("client", updatedClient);
        model.Add("stylist", SelectedStylist);
        return View["success.cshtml", model];
        };

      Get["/stylist/delete/{id}"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        return View["cleared.cshtml", selectedStylist];
        };

        Get["/client/delete/{id}"] = parameters => {
         Dictionary<string, object> model = new Dictionary<string, object> ();
         Client selectedClient = Client.Find(parameters.id);
         Stylist SelectedStylist = Stylist.Find(selectedClient.GetStylistId());

         model.Add("client", selectedClient);
         model.Add("stylist", SelectedStylist);
         return View["cleared.cshtml", model];
        };
    }
  }
}
