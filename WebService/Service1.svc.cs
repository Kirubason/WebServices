using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WebService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract,WebGet(ResponseFormat =WebMessageFormat.Json)]
        //[WebInvoke(Method = "POST", UriTemplate = "DoWork/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string DoWork()
        {
            // Add your operation implementation here
            return "Welcome to Web Services";
        }

        //For returning all the data from the DB
        [OperationContract, WebGet(ResponseFormat = WebMessageFormat.Json)]
        public List<SuperHero> GetAllHeroes()
        {
            return Data.SuperHeroes;
        }
        //For returning particular ID alone
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate= "GetHero/{id}")]

        
        public SuperHero GetHero(string id)
        {
            return Data.SuperHeroes.Find(sh => sh.Id == int.Parse(id));
        }
        // Add the uses to the list

        [OperationContract,WebInvoke(Method ="POST" , ResponseFormat = WebMessageFormat.Json , BodyStyle = WebMessageBodyStyle.Bare , UriTemplate = "AddHero")]
        public SuperHero AddHero(SuperHero New)
        {
            New.Id = Data.SuperHeroes.Max(sh => sh.Id) + 1;
            Data.SuperHeroes.Add(New);
            return New;
        }

        [OperationContract, WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateHero/{id}")]
        public SuperHero UpdateHero(SuperHero update, string id)
        {
            SuperHero Hero = Data.SuperHeroes.Where(sh => sh.Id == int.Parse(id)).FirstOrDefault();
            Hero.FirstName = update.FirstName;
            Hero.LastName = update.LastName;
            Hero.HeroName = update.HeroName;
            Hero.PlaceOfBirth = update.PlaceOfBirth;
            Hero.Combat = update.Combat;
            return Hero;
        }

        [OperationContract, WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteHero/{id}")]
        public List<SuperHero> DeleteHero(string id)
        {
            Data.SuperHeroes = Data.SuperHeroes.Where(sh => sh.Id != int.Parse(id)).ToList();
            
            return Data.SuperHeroes;
        }

        [OperationContract, WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SearchWord/{word}")]
        public List<SuperHero> SearchWord(string word)
        {
            List<SuperHero> result = Data.SuperHeroes.Where<SuperHero>(sh => sh.FirstName.ToLower().Contains(word) 
            || sh.LastName.ToLower().Contains(word)
            || sh.HeroName.ToLower().Contains(word)
            || sh.PlaceOfBirth.ToLower().Contains(word))
            .ToList<SuperHero>();

            return result;
        }
    }
}
